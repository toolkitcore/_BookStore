using Ardalis.Specification;
using BookStore.Catalog.Domain.BookAggregate;

namespace BookStore.Catalog.Domain.AuthorAggregate.Specifications;

public sealed class BooksByAuthorIdPaginatedSpec : Specification<Book>
{
    public BooksByAuthorIdPaginatedSpec(Guid authorId, int pageSize, int pageIndex)
    {
        Query
            .Where(b => b.Id == authorId)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize);
    }
}
