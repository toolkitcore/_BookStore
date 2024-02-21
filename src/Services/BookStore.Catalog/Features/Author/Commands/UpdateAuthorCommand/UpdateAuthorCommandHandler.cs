using Ardalis.GuardClauses;
using Ardalis.Result;
using BookStore.Catalog.Infrastructure.Data;
using BookStore.Core.Abstractions;
using BookStore.Persistence;
using Mapster;

namespace BookStore.Catalog.Features.Author.Commands.UpdateAuthorCommand;

public sealed class UpdateAuthorCommandHandler(
    ILogger<UpdateAuthorCommandHandler> logger,
    Repository<CatalogDbContext, Domain.AuthorAggregate.Author> repository) 
    : ICommandHandler<UpdateAuthorCommand, Result>
{
    public async Task<Result> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating author with id {Id}", request.Id);
        var entity = request.Adapt<Domain.AuthorAggregate.Author>();
        var author = await repository.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, author);
        await repository.UpdateAsync(entity, cancellationToken);
        logger.LogInformation("Author with id {Id} updated", request.Id);
        return Result.Success();
    }
}
