using Ardalis.Result;
using BookStore.Catalog.Features.Author.Queries.GetAuthorBooksQuery;
using BookStore.Catalog.ViewModels;
using FastEndpoints;
using MediatR;

namespace BookStore.Catalog.Endpoints.Author.GetBooks;

public class GetBooks(ISender sender) : Endpoint<GetBooksRequest, GetBooksResponse>
{
    public override void Configure()
    {
        Get(GetBooksRequest.Route);
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetBooksRequest request, CancellationToken cancellationToken)
    {
        var query = new GetAuthorBooksQuery(request.AuthorId, request.PageIndex, request.PageSize);
        var result = await sender.Send(query, cancellationToken);

        if (result.Status.Equals(ResultStatus.NotFound))
        {
            await SendNotFoundAsync(cancellationToken);
            return;
        }

        if (result.IsSuccess)
        {
            Response = new(
                result.Value.Select(c => new AuthorViewModel(c.Id, c.Name, c.ShortBio, c.AuthorContact)).ToList(),
                new(result.PagedInfo.PageNumber, result.PagedInfo.PageSize, result.PagedInfo.TotalRecords,
                    result.PagedInfo.TotalPages)
            );
        }
    }
}
