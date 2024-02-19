using BookStore.Core.SharedKernel;
using BookStore.Infrastructure.Outbox.Message;
using Dapr.Client;
using MediatR;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace BookStore.Infrastructure.Outbox.Dapr.Internal;

public sealed class DaprDispatchedHandler(
    DaprClient daprClient,
    IOptions<DaprOutboxOptions> options) : INotificationHandler<DomainEventBase>
{
    public async Task Handle(DomainEventBase notification, CancellationToken cancellationToken)
    {
        var events = await daprClient.GetStateEntryAsync<List<OutboxMessage>>(options.Value.StateStoreName,
                       options.Value.OutboxName, cancellationToken: cancellationToken);
        events.Value ??= [];

        events.Value.Add(new()
        {
            Id = Guid.NewGuid(),
            Data = JsonSerializer.Serialize(notification),
            Type = notification.GetType().AssemblyQualifiedName ?? "Undefined",
            OccurredOn = DateTime.UtcNow
        });

        await events.SaveAsync(cancellationToken: cancellationToken);
    }
}