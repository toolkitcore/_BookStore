using BookStore.Catalog.Domain.AuthorAggregate;

namespace BookStore.Catalog.Endpoints.Author.GetById;

public sealed class GetAuthorByIdResponse(
    Guid id,
    string name,
    string? shortBio,
    AuthorContact? authorContact)
{
    public Guid Id { get; set; } = id;
    public string Name { get; set; } = name;
    public string? ShortBio { get; set; } = shortBio;
    public AuthorContact? AuthorContact { get; set; } = authorContact;
}
