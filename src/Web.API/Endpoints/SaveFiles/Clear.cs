using Application.SaveFiles.Clear;
using Microsoft.AspNetCore.Mvc;
using Web.API.Infrastructure;

namespace Web.API.Endpoints.SaveFiles;

internal sealed class Clear : IEndpoint
{

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete(ClearSaveFilesCommand.Path, async (
                HttpContext httpContext,
                [FromServices] ClearSaveFilesHandler handler,
                CancellationToken cancellationToken) =>
            {
                var command = new ClearSaveFilesCommand(httpContext.GetUserId());
                var result = await handler.Handle(command, cancellationToken);

                return result.Match(Results.NoContent, CustomResults.Problem);
            })
            .RequireAuthorization()
            .WithTags(Tags.SaveFiles)
            .Produces(StatusCodes.Status204NoContent);
    }
}
