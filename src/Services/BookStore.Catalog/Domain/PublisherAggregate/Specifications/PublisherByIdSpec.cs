using Ardalis.Specification;

namespace BookStore.Catalog.Domain.PublisherAggregate.Specifications;

public sealed class PublisherByIdSpec : Specification<Publisher>
{
    public PublisherByIdSpec(Guid publisherId)
    {
        Query
            .Where(p => p.Id == publisherId);
    }
}
