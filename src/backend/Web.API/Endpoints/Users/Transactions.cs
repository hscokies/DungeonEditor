using Application.Users.Transactions;
using Microsoft.AspNetCore.Mvc;
using Web.API.Infrastructure;

namespace Web.API.Endpoints.Users;

internal sealed class Transactions : IEndpoint
{

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(GetTransactionsQuery.Path, async (
                HttpContext httpContext,
                ushort offset,
                ushort limit,
                [FromServices] GetTransactionsHandler handler,
                CancellationToken cancellationToken) =>
            {
                var query = new GetTransactionsQuery(httpContext.GetUserId(), offset, limit);
                var result = await handler.Handle(query, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .WithTags(Tags.Account)
            .Produces(StatusCodes.Status200OK);
    }
}
