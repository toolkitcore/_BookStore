using Ardalis.Result;
using BookStore.Catalog.Features.Author.Queries.GetAuthorByIdQuery;
using FastEndpoints;
using MediatR;

namespace BookStore.Catalog.Endpoints.Author.GetById;

public sealed class GetById(ISender sender) : Endpoint<GetAuthorByIdRequest, GetAuthorByIdResponse>
{
    public override void Configure()
    {
        Get(GetAuthorByIdRequest.Route);
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetAuthorByIdRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetAuthorByIdQuery(request.AuthorId);
        var result = await sender.Send(query, cancellationToken);

        if (result.Status.Equals(ResultStatus.NotFound))
        {
            await SendNotFoundAsync(cancellationToken);
            return;
        }

        if (result.IsSuccess)
        {
            Response = new(
                result.Value.Id,
                result.Value.Name,
                result.Value.ShortBio,
                result.Value.AuthorContact);
        }
    }
}
