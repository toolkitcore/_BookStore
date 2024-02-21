namespace BookStore.Catalog.Endpoints.Author.Create;

public class CreateAuthorResponse(Guid authorId)
{
    public Guid AuthorId { get; set; } = authorId;
}
