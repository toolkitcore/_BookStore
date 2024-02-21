using Ardalis.GuardClauses;
using BookStore.Catalog.Domain.AuthorAggregate.Events;
using BookStore.Core.SharedKernel;

namespace BookStore.Catalog.Domain.AuthorAggregate;

public sealed class Author(string name, AuthorContact? authorContact, string? shortBio) 
    : AuditableEntityBase, IAggregateRoot
{
    public string Name { get; set; } = Guard.Against.NullOrEmpty(name, nameof(name));
    public string? ShortBio { get; set; } = shortBio;
    public AuthorContact? AuthorContact { get; set; } = authorContact;
    public ICollection<BookAuthor> BookAuthors { get; set; } = [];

    public void LinkAuthorToBook(Guid bookId, List<Guid> authorIds, string? notes)
        => RegisterDomainEvent(new AuthorLinkedToBookEvent(bookId, authorIds, notes));
}
