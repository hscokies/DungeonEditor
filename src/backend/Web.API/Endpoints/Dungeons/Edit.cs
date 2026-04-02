using Application.Dungeons;
using Application.Dungeons.Edit;
using Microsoft.AspNetCore.Mvc;
using Web.API.Infrastructure;

namespace Web.API.Endpoints.Dungeons;

internal sealed class Edit : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var mapper = new DungeonsMapper();

        app.MapPut(EditDungeonCommand.Path, async (
                HttpContext httpContext,
                Guid id,
                [FromBody] DungeonDto request,
                [FromServices] EditDungeonHandler handler,
                CancellationToken cancellationToken) =>
            {
                var command = mapper.ToCommand(request, id, httpContext.GetUserId());
                var result = await handler.Handle(command, cancellationToken);

                return result.Match(Results.NoContent, CustomResults.Problem);
            })
            .RequireAuthorization()
            .WithTags(Tags.Dungeon)
            .Produces(StatusCodes.Status200OK)
            .ProducesValidationProblem();
    }
}
