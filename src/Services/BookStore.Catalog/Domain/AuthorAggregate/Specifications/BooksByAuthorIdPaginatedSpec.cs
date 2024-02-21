using Ardalis.Specification;

namespace BookStore.Catalog.Domain.AuthorAggregate.Specifications;

public sealed class BooksByAuthorIdPaginatedSpec : Specification<Author>
{
    public BooksByAuthorIdPaginatedSpec(Guid authorId, int pageSize, int pageIndex)
    {
        Query
            .Where(b => b.Id == authorId)
            .Include(b => b.BookAuthors.Select(ba => ba.Book))
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize);
    }
}
