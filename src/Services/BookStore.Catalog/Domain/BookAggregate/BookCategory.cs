using Ardalis.GuardClauses;
using BookStore.Catalog.Domain.CategoryAggregate;
using System.Text.Json.Serialization;

namespace BookStore.Catalog.Domain.BookAggregate;

public sealed class BookCategory(Guid bookId, Guid categoryId)
{
    public Guid BookId { get; set; } = Guard.Against.NullOrEmpty(bookId, nameof(bookId));
    [JsonIgnore] public Book? Book { get; set; }
    public Guid CategoryId { get; set; } = Guard.Against.NullOrEmpty(categoryId, nameof(categoryId));
    [JsonIgnore] public Category? Category { get; set; }
}
