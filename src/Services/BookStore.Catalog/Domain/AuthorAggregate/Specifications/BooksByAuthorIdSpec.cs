using Ardalis.Specification;

namespace BookStore.Catalog.Domain.AuthorAggregate.Specifications;

public sealed class BooksByAuthorIdSpec : Specification<Author>
{
    public BooksByAuthorIdSpec(Guid authorId)
    {
        Query
            .Where(a => a.Id == authorId)
            .Include(a => a.BookAuthors.Select(ba => ba.Book));
    }
}
