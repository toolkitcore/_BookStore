using Ardalis.GuardClauses;
using Ardalis.Result;
using BookStore.Catalog.Infrastructure.Data;
using BookStore.Catalog.ViewModels;
using BookStore.Core.Abstractions;
using BookStore.Persistence;
using Mapster;

namespace BookStore.Catalog.Features.Author.Queries.GetAuthorByIdQuery;

public sealed class GetAuthorByIdQueryHandler(
    ILogger<GetAuthorByIdQueryHandler> logger,
    Repository<CatalogDbContext, Domain.AuthorAggregate.Author> repository)
    : IQueryHandler<GetAuthorByIdQuery, Result<AuthorViewModel>>
{
    public async Task<Result<AuthorViewModel>> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
    {
        Guard.Against.NullOrEmpty(request.AuthorId, nameof(request));
        var author = await repository.GetByIdAsync(request.AuthorId, cancellationToken);
        Guard.Against.NotFound(request.AuthorId, author);
        logger.LogInformation("Returning author {AuthorId}", request.AuthorId);
        return Result<AuthorViewModel>.Success(author.Adapt<AuthorViewModel>());
    }
}
