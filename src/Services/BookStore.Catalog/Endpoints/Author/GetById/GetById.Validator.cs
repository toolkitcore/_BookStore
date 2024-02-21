using FastEndpoints;
using FluentValidation;

namespace BookStore.Catalog.Endpoints.Author.GetById;

public sealed class GetByIdValidator : Validator<GetAuthorByIdRequest>
{
    public GetByIdValidator()
    {
        RuleFor(x => x.AuthorId).NotEmpty();
    }
}
