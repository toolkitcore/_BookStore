using Ardalis.Specification;

namespace BookStore.Catalog.Domain.BookAggregate.Specifications;

public sealed class BookByIdSpec : Specification<Book>
{
    public BookByIdSpec(Guid bookId)
    {
        Query
            .Where(b => b.Id == bookId)
            .Include(b => b.Publisher)
            .Include(b => b.BookAuthors.Select(ba => ba.Author))
            .Include(b => b.BookCategories.Select(bc => bc.Category));
    }
}
