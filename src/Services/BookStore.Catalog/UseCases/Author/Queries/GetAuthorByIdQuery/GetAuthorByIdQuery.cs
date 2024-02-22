using Ardalis.Result;

using BookStore.Catalog.ViewModels;
using BookStore.Core.Abstractions;

namespace BookStore.Catalog.UseCases.Author.Queries.GetAuthorByIdQuery;

public sealed record GetAuthorByIdQuery(Guid AuthorId) : IQuery<Result<AuthorViewModel>>;
