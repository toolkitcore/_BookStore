using Ardalis.Result;
using BookStore.Catalog.Infrastructure.Data;
using BookStore.Core.Abstractions;
using BookStore.Persistence;
using Mapster;

namespace BookStore.Catalog.Features.Author.Commands.CreateAuthorCommand;

public sealed class CreateAuthorCommandHandler(
    ILogger<CreateAuthorCommandHandler> logger,
    Repository<CatalogDbContext, Domain.AuthorAggregate.Author> repository)
    : ICommandHandler<CreateAuthorCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating author with name {Name}", request.Name);
        var entity = request.Adapt<Domain.AuthorAggregate.Author>();
        await repository.AddAsync(entity, cancellationToken);
        logger.LogInformation("Author {Name} created", request.Name);
        return Result<Guid>.Success(entity.Id);
    }
}
