using BookStore.Catalog.Domain.AuthorAggregate;

namespace BookStore.Catalog.Endpoints.Author.Create;

public class CreateAuthorRequest
{
    public const string Route = "/Authors";

    public required string Name { get; set; }
    public string? ShortBio { get; set; }
    public AuthorContact? AuthorContact { get; set; }
}
