using Ardalis.Result;

using BookStore.Catalog.Domain.AuthorAggregate;
using BookStore.Core.Abstractions;

namespace BookStore.Catalog.UseCases.Author.Commands.CreateAuthorCommand;

public sealed record CreateAuthorCommand(string Name, string? ShortBio, AuthorContact? AuthorContact) 
    : ICommand<Result<Guid>>;
