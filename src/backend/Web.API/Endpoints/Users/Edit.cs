using Application.Users.Edit;
using Domain.Users;
using Web.API.Infrastructure;
using Web.API.Requests;

namespace Web.API.Endpoints.Users;

internal sealed class Edit : IEndpoint
{

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch(EditUserCommand.Path, async (
                Guid id,
                EditUserRequest request,
                EditUserHandler handler,
                CancellationToken cancellationToken) =>
            {
                var command = new EditUserCommand(id, request.Balance);
                var result = await handler.Handle(command, cancellationToken);
                return result.Match(Results.NoContent, CustomResults.Problem);
            })
            .RequireAuthorization(x => x.RequireRole(RoleName.Admin))
            .Produces(StatusCodes.Status204NoContent)
            .ProducesValidationProblem();
    }
}
