using BookStore.Core.SharedKernel;

namespace BookStore.Catalog.Domain.AuthorAggregate.Events;

public sealed class AuthorLinkedToBookEvent(
    Guid bookId,
    List<Guid> authorIds,
    string? notes
) : DomainEventBase
{
    public Guid BookId { get; set; } = bookId;
    public List<Guid> AuthorIds { get; set; } = authorIds;
    public string? Notes { get; set; } = notes;
}
