namespace BookStore.Catalog.Endpoints.Author.Delete;

public sealed class DeleteAuthorRequest
{
    public const string Route = "/Authors/{AuthorId:guid}";
    public static string BuildRoute(Guid authorId)
        => Route.Replace("{AuthorId:guid}", authorId.ToString());
    public Guid AuthorId { get; set; }
}
