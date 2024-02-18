namespace BookStore.Infrastructure.EventBus.Events;

public record IntegrationEvent
{
    public Guid Id { get; } = Guid.NewGuid();
    public DateTime CreationDate { get; } = DateTime.UtcNow;
}