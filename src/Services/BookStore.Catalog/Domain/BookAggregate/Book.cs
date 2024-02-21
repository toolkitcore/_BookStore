using Ardalis.GuardClauses;
using BookStore.Catalog.Domain.AuthorAggregate;
using BookStore.Catalog.Domain.PublisherAggregate;
using BookStore.Core.SharedKernel;
using System.Text.Json.Serialization;

namespace BookStore.Catalog.Domain.BookAggregate;

public sealed class Book(
    string title,
    string description,
    decimal price,
    string? pictureFileName,
    int availableStock,
    Guid? publisherId) : AuditableEntityBase, IAggregateRoot
{
    public string Title { get; set; } = Guard.Against.NullOrEmpty(title, nameof(title));
    public string? Description { get; set; } = description;
    public decimal Price { get; set; } = Guard.Against.Negative(price, nameof(price));
    public string? PictureFileName { get; set; } = pictureFileName;
    public int AvailableStock { get; set; } = Guard.Against.Negative(availableStock, nameof(availableStock));
    public Guid? PublisherId { get; set; } = publisherId;
    [JsonIgnore] public Publisher? Publisher { get; set; }
    public ICollection<BookAuthor> BookAuthors { get; set; } = [];
    public ICollection<BookCategory> BookCategories { get; set; } = [];

    public int RemoveStock(int quantityDesired)
    {
        AvailableStock -= quantityDesired;
        return quantityDesired;
    }
}
