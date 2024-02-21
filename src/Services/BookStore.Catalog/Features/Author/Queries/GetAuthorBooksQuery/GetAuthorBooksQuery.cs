using Ardalis.Result;
using BookStore.Catalog.ViewModels;
using BookStore.Core.Abstractions;

namespace BookStore.Catalog.Features.Author.Queries.GetAuthorBooksQuery;

public sealed record GetAuthorBooksQuery(Guid AuthorId, int PageSize, int PageIndex) 
    : IQuery<PagedResult<IEnumerable<AuthorViewModel>>>; 
