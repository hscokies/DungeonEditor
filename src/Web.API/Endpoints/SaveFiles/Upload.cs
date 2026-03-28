using System.Security.Claims;
using Application.SaveFiles.Upload;
using Microsoft.AspNetCore.Mvc;
using Web.API.Infrastructure;
using Web.API.Infrastructure.Binders;

namespace Web.API.Endpoints.SaveFiles;

internal sealed class Upload : IEndpoint
{

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(UploadSaveCommand.Path, async (
                [FromClaims(ClaimTypes.NameIdentifier)] Guid userId,
                [FromForm] IFormFile file,
                [FromServices] UploadSaveHandler handler,
                CancellationToken cancellationToken
            ) =>
            {
                var command = new UploadSaveCommand(userId, file);
                var result = await handler.Handle(command, cancellationToken);
                return result.Match(() => Results.Accepted(null), CustomResults.Problem);
            })
            .RequireAuthorization()
            .WithTags(Tags.SaveFiles)
            .Produces(StatusCodes.Status202Accepted)
            .ProducesValidationProblem()
            .DisableAntiforgery();
    }
}
