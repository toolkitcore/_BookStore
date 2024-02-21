using Ardalis.GuardClauses;
using BookStore.Catalog.Domain.BookAggregate;
using BookStore.Catalog.Domain.CategoryAggregate.Events;
using BookStore.Core.SharedKernel;
using System.Text.Json.Serialization;

namespace BookStore.Catalog.Domain.CategoryAggregate;

public sealed class Category(string name, string? description, Guid? parentCategoryId)
    : AuditableEntityBase, IAggregateRoot
{
    public string Name { get; set; } = Guard.Against.NullOrEmpty(name, nameof(name));
    public string? Description { get; set; } = description;
    public Guid? ParentCategoryId { get; set; } = parentCategoryId;
    [JsonIgnore] public Category? ParentCategory { get; set; }
    public ICollection<Category> ChildrenCategories { get; set; } = [];
    public ICollection<Book> Books { get; set; } = [];
    public ICollection<BookCategory> BookCategories { get; set; } = [];

    public void LinkCategoryToBook(Guid bookId, List<Guid> categoryId)
        => RegisterDomainEvent(new CategoryLinkedToBookEvent(bookId, categoryId));
}
