using Application.Users.Login;
using Microsoft.AspNetCore.Mvc;
using Web.API.Infrastructure;

namespace Web.API.Endpoints.Users;

internal sealed class Login : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(LoginCommand.Path, async (
                LoginCommand command,
                [FromServices] LoginHandler handler,
                CancellationToken cancellationToken) =>
        {
            var result = await handler.Handle(command, cancellationToken);
            return result.Match(Results.NoContent, CustomResults.Problem);
        })
        .WithTags(Tags.Account)
        .Produces(StatusCodes.Status204NoContent)
        .ProducesValidationProblem();
    }
}
