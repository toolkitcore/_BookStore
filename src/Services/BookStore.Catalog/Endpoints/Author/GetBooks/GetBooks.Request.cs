using FastEndpoints;

namespace BookStore.Catalog.Endpoints.Author.GetBooks;

public class GetBooksRequest
{
    public const string Route = "/Authors/{AuthorId:guid}/Books";
    public static string BuildRoute(Guid authorId)
        => Route.Replace("{AuthorId:guid}", authorId.ToString());
    public Guid AuthorId { get; set; }

    [BindFrom(nameof(PageIndex))]
    public int PageIndex { get; set; } = 1;

    [BindFrom(nameof(PageSize))]
    public int PageSize { get; set; } = 10;
}
