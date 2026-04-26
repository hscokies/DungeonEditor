using Application.SaveFiles.List;
using Microsoft.AspNetCore.Mvc;
using Web.API.Infrastructure;

namespace Web.API.Endpoints.SaveFiles;

internal sealed class List : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(ListSaveFilesQuery.Path, async (
                HttpContext httpContext,
                string? search,
                ushort offset,
                ushort limit,
                [FromServices] ListSaveFilesHandler handler,
                CancellationToken cancellationToken) =>
            {
                var query = new ListSaveFilesQuery(httpContext.GetUserId(), search, offset, limit);
                var result = await handler.Handle(query, cancellationToken);
                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .RequireAuthorization()
            .WithTags(Tags.SaveFile)
            .Produces(StatusCodes.Status200OK);
    }
}
