using Application.Users.Login;
using Application.Users.Logout;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Endpoints.Users;

internal sealed class Logout : IEndpoint
{

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete(LoginCommand.Path, async ([FromServices] LogoutHandler handler, CancellationToken cancellationToken) =>
            {
                await handler.Handle(cancellationToken);
                return Results.NoContent();
            })
            .WithTags(Tags.Account);
    }
}
