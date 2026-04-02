using Application.SaveFiles.Get;
using Application.SaveFiles.Upload;
using Microsoft.AspNetCore.Mvc;
using Web.API.Infrastructure;

namespace Web.API.Endpoints.SaveFiles;

internal sealed class Upload : IEndpoint
{

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(UploadSaveCommand.Path, async (
                HttpContext httpContext,
                [FromForm] IFormFile file,
                [FromServices] UploadSaveHandler handler,
                CancellationToken cancellationToken
            ) =>
            {
                var command = new UploadSaveCommand(httpContext.GetUserId(), file);
                var result = await handler.Handle(command, cancellationToken);
                return result.Match((id) => Results.Accepted(GetSaveFileQuery.Path, id), CustomResults.Problem);
            })
            .RequireAuthorization()
            .WithTags(Tags.SaveFile)
            .Produces(StatusCodes.Status202Accepted)
            .ProducesValidationProblem()
            .DisableAntiforgery();
    }
}
