using Ardalis.GuardClauses;
using BookStore.Catalog.Domain.BookAggregate;
using BookStore.Core.SharedKernel;

namespace BookStore.Catalog.Domain.PublisherAggregate;

public sealed class Publisher(string name, string? webUrl) : AuditableEntityBase, IAggregateRoot
{
    public required string Name { get; set; } = Guard.Against.NullOrEmpty(name, nameof(name));
    public string? WebUrl { get; set; } = webUrl;
    public ICollection<Book> Books { get; set; } = [];
}
