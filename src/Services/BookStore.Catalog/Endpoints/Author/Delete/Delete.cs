using Ardalis.Result;
using BookStore.Catalog.UseCases.Author.Commands.DeleteAuthorCommand;
using FastEndpoints;
using MediatR;

namespace BookStore.Catalog.Endpoints.Author.Delete;

public sealed class Delete(ISender sender) : Endpoint<DeleteAuthorRequest>
{
    public override void Configure()
    {
        Delete(DeleteAuthorRequest.Route);
        AllowAnonymous();
    }

    public override async Task HandleAsync(DeleteAuthorRequest request, CancellationToken cancellationToken)
    {
        var command = new DeleteAuthorCommand(request.AuthorId);
        var result = await sender.Send(command, cancellationToken);

        if (result.Status.Equals(ResultStatus.NotFound))
        {
            await SendNotFoundAsync(cancellationToken);
            return;
        }

        if (result.IsSuccess)
            await SendNoContentAsync(cancellationToken);
    }
}
