using System.Linq.Expressions;
using Ardalis.Specification;

namespace BookStore.Catalog.Domain.BookAggregate.Specifications;

public static class BookSortOptions
{
    public const string Id = "id";
    public const string Name = "name";
    public const string Price = "price";
}

public sealed class BookFilterPaginatedSpec : Specification<Book>
{
    public BookFilterPaginatedSpec(
        int pageSize,
        int pageIndex,
        string orderBy = BookSortOptions.Id,
        bool isAscending = true)
    {
        Query
            .Include(b => b.Publisher)
            .Include(b => b.BookAuthors.Select(ba => ba.Author))
            .Include(b => b.BookCategories.Select(bc => bc.Category))
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize);

        var parameter = Expression.Parameter(typeof(Book), "x");
        var property = Expression.Property(parameter, orderBy);
        var lambda = Expression.Lambda<Func<Book, object>>(
            Expression.Convert(property, typeof(object)), parameter
        );

        if (isAscending)
            Query.OrderBy(lambda!);
        else
            Query.OrderByDescending(lambda!);
    }
}
