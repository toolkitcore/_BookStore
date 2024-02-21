using FastEndpoints;

using FluentValidation;

namespace BookStore.Catalog.Endpoints.Author.Create;

public class CreateAuthorValidator : Validator<CreateAuthorRequest>
{
    public CreateAuthorValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.ShortBio)
            .MaximumLength(200);
    }
}
