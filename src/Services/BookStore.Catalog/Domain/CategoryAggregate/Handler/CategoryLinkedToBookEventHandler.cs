using Ardalis.GuardClauses;
using BookStore.Catalog.Domain.BookAggregate;
using BookStore.Catalog.Domain.CategoryAggregate.Events;
using BookStore.Catalog.Domain.CategoryAggregate.Specifications;
using BookStore.Catalog.Infrastructure.Data;
using BookStore.Core.Exceptions;
using BookStore.Persistence;
using MediatR;

namespace BookStore.Catalog.Domain.CategoryAggregate.Handler;

public sealed class CategoryLinkedToBookEventHandler(
    ILogger<CategoryLinkedToBookEventHandler> logger,
    Repository<CatalogDbContext, Category> repository) : INotificationHandler<CategoryLinkedToBookEvent>
{
    public async Task Handle(CategoryLinkedToBookEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling {Event} with book id {BookId} and category ids {CategoryIds}",
            nameof(CategoryLinkedToBookEvent), notification.BookId, notification.CategoryIds);
        
        var categories = await Task.WhenAll(notification.CategoryIds
            .Select(async categoryId =>
            {
                var category =
                    await repository.FirstOrDefaultAsync(new CategoryByIdSpec(categoryId), cancellationToken);

                Guard.Against.Null(category, nameof(category), $"Category with id {categoryId} was not found");

                if (category.BookCategories.Any(bc => bc.BookId == notification.BookId))
                {
                    logger.LogWarning("Category with id {CategoryId} is already linked to book with id {BookId}",
                        categoryId, notification.BookId);
                    throw CoreDomainException.DuplicateEntity(nameof(BookCategory), $"{categoryId}");
                }

                category.BookCategories.Add(new(notification.BookId, categoryId));
                return category;
            }));

        await repository.UpdateRangeAsync(categories, cancellationToken);
    }
}
