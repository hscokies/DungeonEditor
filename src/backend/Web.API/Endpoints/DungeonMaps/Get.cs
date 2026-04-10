using Application.DungeonMaps.Get;
using Microsoft.AspNetCore.Mvc;
using Web.API.Infrastructure;

namespace Web.API.Endpoints.DungeonMaps;

internal sealed class Get : IEndpoint
{

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(GetDungeonMapsQuery.Path, async (
                string? search,
                [FromServices] GetDungeonMapsHandler handler,
                CancellationToken cancellationToken) =>
            {
                var query = new GetDungeonMapsQuery(search ?? string.Empty);
                var result = await handler.Handle(query, cancellationToken);
                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .RequireAuthorization()
            .WithTags(Tags.DungeonMap)
            .Produces(StatusCodes.Status200OK);
    }
}
