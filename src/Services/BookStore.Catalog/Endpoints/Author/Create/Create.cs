using BookStore.Catalog.UseCases.Author.Commands.CreateAuthorCommand;
using FastEndpoints;
using MediatR;

namespace BookStore.Catalog.Endpoints.Author.Create;

public sealed class Create(ISender sender) : Endpoint<CreateAuthorRequest, CreateAuthorResponse>
{
    public override void Configure()
    {
        Post(CreateAuthorRequest.Route);
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateAuthorRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateAuthorCommand(request.Name, request.ShortBio, request.AuthorContact);
        var result = await sender.Send(command, cancellationToken);

        if (result.IsSuccess)
            Response = new(result.Value);
    }
}
