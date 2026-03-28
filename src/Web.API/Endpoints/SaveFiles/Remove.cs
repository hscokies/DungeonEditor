using Application.SaveFiles.Remove;
using Microsoft.AspNetCore.Mvc;
using Web.API.Infrastructure;

namespace Web.API.Endpoints.SaveFiles;

internal sealed class Remove : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete(RemoveSaveFileCommand.Path, async (
                HttpContext httpContext,
                Guid id,
                [FromServices] RemoveSaveFileHandler handler,
                CancellationToken cancellationToken) =>
            {
                var command = new RemoveSaveFileCommand(id, httpContext.GetUserId());
                var result = await handler.Handle(command, cancellationToken);

                return result.Match(Results.NoContent, CustomResults.Problem);
            })
            .RequireAuthorization()
            .WithTags(Tags.SaveFiles)
            .Produces(StatusCodes.Status204NoContent)
            .ProducesValidationProblem();
    }
}
