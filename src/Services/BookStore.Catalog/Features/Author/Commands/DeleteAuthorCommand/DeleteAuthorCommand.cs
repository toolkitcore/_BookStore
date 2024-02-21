using Ardalis.Result;
using BookStore.Core.Abstractions;

namespace BookStore.Catalog.Features.Author.Commands.DeleteAuthorCommand;

public sealed record DeleteAuthorCommand(Guid Id) : ICommand<Result>;
