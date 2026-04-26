using Application.SaveFiles.Download;
using Web.API.Infrastructure;

namespace Web.API.Endpoints.SaveFiles;

internal sealed class Download : IEndpoint
{

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(DownloadSaveFileQuery.Path, async (
                HttpContext httpContext,
                Guid id,
                DownloadSaveFileHandler handler,
                CancellationToken cancellationToken) =>
            {
                var userId = httpContext.GetUserId();
                var query = new DownloadSaveFileQuery(id, userId);

                var result = await handler.Handle(query, cancellationToken);
                return result.Match(
                    sf => Results.File(sf.Stream, sf.ContentType, sf.Filename),
                    CustomResults.Problem);
            })
            .WithTags(Tags.SaveFile)
            .RequireAuthorization()
            .Produces(StatusCodes.Status200OK)
            .ProducesValidationProblem();
    }
}
