using BookStore.Infrastructure.EventBus.Abstractions;
using BookStore.Infrastructure.EventBus.Events;
using Dapr.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BookStore.Infrastructure.EventBus.Dapr.Internal;

public class DaprEventBus(
    DaprClient daprClient, 
    IOptions<DaprEventBusOptions> options,
    ILogger<DaprEventBus> logger) : IEventBus
{
    public async Task PublishAsync(IntegrationEvent integrationEvent, CancellationToken token = default)
    {
        var topicName = integrationEvent.GetType().Name;
        var pubsubName = options.Value.PubSubName;

        logger.LogInformation("Publishing {IntegrationEvent} to {PubSubName} with topic {TopicName}", integrationEvent, pubsubName, topicName);

        await daprClient.PublishEventAsync(pubsubName, topicName, (object)integrationEvent, token);
    }
}