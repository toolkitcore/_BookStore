using Ardalis.GuardClauses;
using Ardalis.Result;
using BookStore.Catalog.Infrastructure.Data;
using BookStore.Core.Abstractions;
using BookStore.Persistence;

namespace BookStore.Catalog.Features.Author.Commands.DeleteAuthorCommand;

public sealed class DeleteAuthorCommandHandler(
    ILogger<DeleteAuthorCommandHandler> logger,
    Repository<CatalogDbContext, Domain.AuthorAggregate.Author> repository)
    : ICommandHandler<DeleteAuthorCommand, Result>
{
    public async Task<Result> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting author with id {Id}", request.Id);
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, entity);
        await repository.DeleteAsync(entity, cancellationToken);
        logger.LogInformation("Author with id {Id} deleted", request.Id);
        return Result.Success();
    }
}
