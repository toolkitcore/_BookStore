using BookStore.Catalog.Domain.AuthorAggregate;
using FastEndpoints;
using FluentValidation;

namespace BookStore.Catalog.Endpoints.Author.Update;

public sealed class UpdateBookValidator : AbstractValidator<UpdateBookRequest>
{
    public UpdateBookValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.ShortBio)
            .MaximumLength(200);

        RuleFor(x => x.AuthorContact)
            .SetValidator(new AuthorContactValidator()!);
    }
}

public sealed class AuthorContactValidator : Validator<AuthorContact>
{
    public AuthorContactValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress()
            .MaximumLength(100);

        RuleFor(x => x.WebUrl)
            .MaximumLength(200);

        RuleFor(x => x.Phone)
            .Length(10, 10);
    }
}
