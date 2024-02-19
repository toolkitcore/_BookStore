using System.Text.Json;
using BookStore.Infrastructure.EventBus.Abstractions;
using BookStore.Infrastructure.EventBus.Events;
using BookStore.Infrastructure.Outbox.Abstractions;
using BookStore.Infrastructure.Outbox.Message;
using Dapr.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BookStore.Infrastructure.Outbox.Dapr.Internal;

public sealed class DaprOutboxProcessor(
    DaprClient daprClient,
    IEventBus eventBus,
    IOptions<DaprOutboxOptions> options,
    ILogger<DaprOutboxProcessor> logger) : ITransactionOutbox
{
    public async Task HandleAsync(Type integrationEventType, CancellationToken token = default)
    {
        logger.LogTrace("Processing outbox messages for event type {EventType}", integrationEventType.Name);

        var messages = await daprClient.GetStateEntryAsync<List<OutboxMessage>>(options.Value.StateStoreName,
            options.Value.OutboxName, cancellationToken: token);

        if (messages.Value is null || messages.Value.Count == 0)
        {
            logger.LogTrace("No outbox messages found for event type {EventType}", integrationEventType.Name);
            return;
        }

        var deletedEventIds = new List<Guid>();

        foreach (var message in messages.Value)
        {
            if (message.Id.Equals(Guid.Empty) || string.IsNullOrEmpty(message.Type) ||
                string.IsNullOrEmpty(message.Data))
                continue;

            var domainEvent = JsonSerializer.Deserialize(message.Data, integrationEventType);

            if (domainEvent is null)
            {
                logger.LogWarning("Failed to deserialize outbox message {MessageId} for event type {EventType}",
                    message.Id, integrationEventType.Name);
                continue;
            }

            await eventBus.PublishAsync((IntegrationEvent)domainEvent, token);

            deletedEventIds.Add(message.Id);
        }

        if (deletedEventIds.Count > 0)
        {
            messages.Value.RemoveAll(m => deletedEventIds.Contains(m.Id));
            await daprClient.SaveStateAsync(options.Value.StateStoreName, options.Value.OutboxName, messages.Value,
                cancellationToken: token);
        }
    }
}