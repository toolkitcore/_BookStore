using BookStore.Catalog.Domain.AuthorAggregate;

namespace BookStore.Catalog.Endpoints.Author.Update;

public sealed class UpdateBookRequest
{
    public const string Route = "/Authors";

    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? ShortBio { get; set; }
    public AuthorContact? AuthorContact { get; set; }
}
