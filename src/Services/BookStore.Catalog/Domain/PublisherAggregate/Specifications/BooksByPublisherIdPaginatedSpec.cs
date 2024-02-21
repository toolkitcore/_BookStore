using Ardalis.Specification;

namespace BookStore.Catalog.Domain.PublisherAggregate.Specifications;

public sealed class BooksByPublisherIdPaginatedSpec : Specification<Publisher>
{
    public BooksByPublisherIdPaginatedSpec(Guid publisherId, int pageSize, int pageIndex)
    {
        Query
            .Where(p => p.Id == publisherId)
            .Include(p => p.Books)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize);
    }
}
