using FastEndpoints;
using FluentValidation;

namespace BookStore.Catalog.Endpoints.Author.Delete;

public sealed class DeleteAuthorValidator : Validator<DeleteAuthorRequest>
{
    public DeleteAuthorValidator()
    {
        RuleFor(x => x.AuthorId).NotEmpty();
    }
}
