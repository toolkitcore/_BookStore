using Ardalis.Result;
using BookStore.Catalog.UseCases.Author.Commands.UpdateAuthorCommand;
using FastEndpoints;
using MediatR;

namespace BookStore.Catalog.Endpoints.Author.Update;

public sealed class Update(ISender sender) : Endpoint<UpdateBookRequest, Result>
{
    public override void Configure()
    {
        Put(UpdateBookRequest.Route);
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateBookRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateAuthorCommand(request.Id, request.Name, request.ShortBio, request.AuthorContact);
        var result = await sender.Send(command, cancellationToken);

        if (result.IsSuccess)
            await SendNoContentAsync(cancellationToken);
    }
}
