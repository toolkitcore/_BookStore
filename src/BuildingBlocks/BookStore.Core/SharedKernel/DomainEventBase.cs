using MediatR;

namespace BookStore.Core.SharedKernel;

public abstract class DomainEventBase : INotification
{
    public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;
    public IDictionary<string, object>? Metadata { get; set; } = new Dictionary<string, object>();
}