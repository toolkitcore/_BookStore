using BookStore.Infrastructure.EventBus.Events;

namespace BookStore.Infrastructure.EventBus.Abstractions;

public interface IEventBus
{
    Task PublishAsync(IntegrationEvent integrationEvent, CancellationToken token = default);
}