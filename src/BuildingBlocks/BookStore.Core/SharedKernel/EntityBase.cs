namespace BookStore.Core.SharedKernel;

public abstract class EntityBase : HasDomainEventsBase
{
    public Guid Id { get; set; }
}