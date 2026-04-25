using Application.Users.List;
using Domain.Users;
using Web.API.Infrastructure;

namespace Web.API.Endpoints.Users;

internal sealed class List : IEndpoint
{

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(ListUsersQuery.Path, async (
                [AsParameters] ListUsersQuery query,
                ListUsersHandler handler,
                CancellationToken cancellationToken) =>
            {
                var result = await handler.Handle(query, cancellationToken);
                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .RequireAuthorization(x => x.RequireRole(RoleName.Admin))
            .WithTags(Tags.Account);
    }
}
