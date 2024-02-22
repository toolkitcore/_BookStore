using Ardalis.GuardClauses;
using Ardalis.Result;

using BookStore.Catalog.Domain.AuthorAggregate.Specifications;
using BookStore.Catalog.Infrastructure.Data;
using BookStore.Catalog.ViewModels;
using BookStore.Core.Abstractions;
using BookStore.Persistence;

using Mapster;

namespace BookStore.Catalog.UseCases.Author.Queries.GetAuthorBooksQuery;

public sealed class GetAuthorBooksQueryHandler(
    ILogger<GetAuthorBooksQueryHandler> logger,
    Repository<CatalogDbContext, Domain.AuthorAggregate.Author> repository)
    : IQueryHandler<GetAuthorBooksQuery, PagedResult<IEnumerable<AuthorViewModel>>>
{
    public async Task<PagedResult<IEnumerable<AuthorViewModel>>> Handle(
        GetAuthorBooksQuery request,
        CancellationToken cancellationToken)
    {
        Guard.Against.NullOrEmpty(request.AuthorId, nameof(request));
        var spec = new BooksByAuthorIdPaginatedSpec(request.AuthorId, request.PageSize, request.PageIndex);
        var authors = await repository.ListAsync(spec, cancellationToken);
        logger.LogInformation("Found {Count} books for author {AuthorId}", authors.Count, request.AuthorId);
        var count = await repository.CountAsync(new BooksByAuthorIdSpec(request.AuthorId), cancellationToken);
        var totalPages = (int)Math.Ceiling(count / (double)request.PageSize);
        var pagedInfo = new PagedInfo(request.PageIndex, request.PageSize, totalPages, count);
        logger.LogInformation("Returning {Count} books for author {AuthorId}", authors.Count, request.AuthorId);
        return new(pagedInfo, authors.Adapt<IEnumerable<AuthorViewModel>>());
    }
}
