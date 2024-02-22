using Ardalis.Result;

using BookStore.Catalog.Domain.AuthorAggregate;
using BookStore.Core.Abstractions;

namespace BookStore.Catalog.UseCases.Author.Commands.UpdateAuthorCommand;

public sealed record UpdateAuthorCommand(Guid Id, string Name, string? ShortBio, AuthorContact? AuthorContact)
    : ICommand<Result>;
