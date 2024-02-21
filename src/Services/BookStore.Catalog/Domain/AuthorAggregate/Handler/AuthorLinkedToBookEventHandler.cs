using Ardalis.GuardClauses;
using BookStore.Catalog.Domain.AuthorAggregate.Events;
using BookStore.Catalog.Domain.AuthorAggregate.Specifications;
using BookStore.Catalog.Infrastructure.Data;
using BookStore.Core.Exceptions;
using BookStore.Persistence;
using MediatR;

namespace BookStore.Catalog.Domain.AuthorAggregate.Handler;

public sealed class AuthorLinkedToBookEventHandler(
    ILogger<AuthorLinkedToBookEventHandler> logger,
    Repository<CatalogDbContext, Author> repository) : INotificationHandler<AuthorLinkedToBookEvent>
{
    public async Task Handle(AuthorLinkedToBookEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling {Event} with book id {BookId} and author ids {AuthorIds}",
            nameof(AuthorLinkedToBookEvent), notification.BookId, notification.AuthorIds);

        var authors = await Task.WhenAll(notification.AuthorIds
            .Select(async authorId =>
            {
                var author = await repository.FirstOrDefaultAsync(new AuthorByIdSpec(authorId), cancellationToken);

                Guard.Against.Null(author, nameof(author), $"Author with id {authorId} was not found");

                if (author.BookAuthors.Any(ba => ba.BookId == notification.BookId))
                {
                    logger.LogWarning("Author with id {AuthorId} is already linked to book with id {BookId}",
                        authorId, notification.BookId);
                    throw CoreDomainException.DuplicateEntity(nameof(BookAuthor), $"{authorId}");
                }

                author.BookAuthors.Add(new(notification.BookId, authorId, notification.Notes));
                return author;
            }));

        await repository.UpdateRangeAsync( authors, cancellationToken);
    }
}
