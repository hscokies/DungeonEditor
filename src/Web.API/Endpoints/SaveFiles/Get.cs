using Application.SaveFiles.Get;
using Microsoft.AspNetCore.Mvc;
using Web.API.Infrastructure;

namespace Web.API.Endpoints.SaveFiles;

internal sealed class Get : IEndpoint
{

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(GetSaveFileQuery.Path, async (
                HttpContext httpContext,
                Guid id,
                [FromServices] GetSaveFileHandler handler,
                CancellationToken cancellationToken) =>
            {
                var query = new GetSaveFileQuery(httpContext.GetUserId(), id);
                var result = await handler.Handle(query, cancellationToken);
                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .RequireAuthorization()
            .WithTags(Tags.SaveFiles)
            .Produces(StatusCodes.Status200OK)
            .ProducesValidationProblem()
            .DisableAntiforgery();
    }
}
