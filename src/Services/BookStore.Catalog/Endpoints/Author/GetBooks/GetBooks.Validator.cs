using FastEndpoints;
using FluentValidation;

namespace BookStore.Catalog.Endpoints.Author.GetBooks;

public class GetBooksValidator : Validator<GetBooksRequest>
{
    public GetBooksValidator()
    {
        RuleFor(x => x.AuthorId).NotEmpty();
        RuleFor(x => x.PageIndex).GreaterThanOrEqualTo(1);
        RuleFor(x => x.PageSize).InclusiveBetween(1, 100);
    }
}
