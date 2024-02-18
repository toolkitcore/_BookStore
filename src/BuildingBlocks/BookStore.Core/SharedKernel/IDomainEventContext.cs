namespace BookStore.Core.SharedKernel;

public interface IDomainEventContext
{
    IEnumerable<DomainEventBase> GetDomainEvents();
}