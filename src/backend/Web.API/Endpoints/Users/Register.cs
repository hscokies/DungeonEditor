using Application.Users.Register;
using Microsoft.AspNetCore.Mvc;
using Web.API.Infrastructure;

namespace Web.API.Endpoints.Users;

internal sealed class Register : IEndpoint
{

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(RegisterCommand.Path, async (
                RegisterCommand command,
                [FromServices] RegisterHandler handler,
                CancellationToken cancellationToken) =>
        {
            var result = await handler.Handle(command, cancellationToken);
            return result.Match(
                Results.Created,
                CustomResults.Problem);
        })
        .WithTags(Tags.Account)
        .Produces(StatusCodes.Status204NoContent)
        .ProducesValidationProblem();
    }
}
