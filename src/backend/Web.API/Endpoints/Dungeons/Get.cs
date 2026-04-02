using Application.Dungeons.Get;
using Microsoft.AspNetCore.Mvc;
using Web.API.Infrastructure;

namespace Web.API.Endpoints.Dungeons;

internal sealed class Get : IEndpoint
{

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(GetDungeonQuery.Path, async (
                HttpContext httpContext,
                Guid id,
                [FromServices] GetDungeonHandler handler,
                CancellationToken ct) =>
            {
                var query = new GetDungeonQuery(httpContext.GetUserId(), id);
                var result = await handler.Handle(query, ct);

                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .RequireAuthorization()
            .WithTags(Tags.Dungeon)
            .Produces(StatusCodes.Status200OK)
            .ProducesValidationProblem();
    }
}
