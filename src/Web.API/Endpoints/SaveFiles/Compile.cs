using Application.SaveFiles.Compile;
using Application.SaveFiles.Get;
using Microsoft.AspNetCore.Mvc;
using Web.API.Infrastructure;

namespace Web.API.Endpoints.SaveFiles;

internal sealed class Compile : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch(CompileSaveFileCommand.Path, async (
                HttpContext httpContext,
                Guid id,
                [FromServices] CompileSaveFileHandler handler,
                CancellationToken cancellationToken) =>
            {
                var command = new CompileSaveFileCommand(id, httpContext.GetUserId());
                var result = await handler.Handle(command, cancellationToken);

                return result.Match(() => Results.Accepted(GetSaveFileQuery.Path, id), CustomResults.Problem);
            })
            .RequireAuthorization()
            .WithTags(Tags.SaveFile)
            .Produces(StatusCodes.Status202Accepted)
            .ProducesValidationProblem();
    }
}
