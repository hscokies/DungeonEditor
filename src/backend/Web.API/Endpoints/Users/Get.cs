using Application.Users.Get;
using Web.API.Infrastructure;

namespace Web.API.Endpoints.Users;

internal sealed class Get : IEndpoint
{

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(GetUserQuery.Path, async (
                HttpContext httpContext,
                GetUserHandler handler,
                CancellationToken cancellationToken) =>
            {
                var query = new GetUserQuery(httpContext.GetUserId());
                var result = await handler.Handle(query, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .RequireAuthorization()
            .WithTags(Tags.Account)
            .Produces(StatusCodes.Status200OK);
    }
}
