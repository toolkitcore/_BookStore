using BookStore.Core.SharedKernel;

namespace BookStore.Catalog.Domain.CategoryAggregate.Events;

public sealed class CategoryLinkedToBookEvent(Guid bookId, List<Guid> categoryIds) : DomainEventBase
{
    public Guid BookId { get; set; } = bookId;
    public List<Guid> CategoryIds { get; set; } = categoryIds;
}
