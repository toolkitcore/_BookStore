namespace BookStore.Catalog.Endpoints.Author.GetById;

public sealed class GetAuthorByIdRequest
{
    public const string Route = "/Authors/{AuthorId:guid}";
    public static string BuildRoute(Guid authorId) 
        => Route.Replace("{AuthorId:guid}", authorId.ToString());
    public Guid AuthorId { get; set; }
}
