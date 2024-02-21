using System.Collections.Immutable;
using BookStore.Core.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Persistence;

public class ApplicationDbContextBase : DbContext, IDatabaseFacade, IDomainEventContext
{
    protected ApplicationDbContextBase(DbContextOptions options) : base(options)
    {
    }

    public IEnumerable<DomainEventBase> GetDomainEvents()
    {
        var domainEntities = ChangeTracker
            .Entries<EntityBase>()
            .Where(x => x.Entity.DomainEvents.Count != 0)
            .ToImmutableList();

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.DomainEvents)
            .ToImmutableList();

        domainEntities.ForEach(entity => entity.Entity.ClearDomainEvents());

        return domainEvents;
    }
}
