using Ardalis.GuardClauses;
using BookStore.Catalog.Domain.BookAggregate;
using System.Text.Json.Serialization;

namespace BookStore.Catalog.Domain.AuthorAggregate;

public sealed class BookAuthor(Guid bookId, Guid authorId, string? notes)
{
    public Guid BookId { get; set; } = Guard.Against.NullOrEmpty(bookId, nameof(bookId));
    public Book? Book { get; set; }
    public Guid AuthorId { get; set; } = Guard.Against.NullOrEmpty(authorId, nameof(authorId));
    [JsonIgnore] public Author? Author { get; set; }
    public string? Notes { get; set; } = notes;
}
