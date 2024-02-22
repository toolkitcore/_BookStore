using Ardalis.Result;

using BookStore.Core.Abstractions;

namespace BookStore.Catalog.UseCases.Author.Commands.DeleteAuthorCommand;

public sealed record DeleteAuthorCommand(Guid Id) : ICommand<Result>;
