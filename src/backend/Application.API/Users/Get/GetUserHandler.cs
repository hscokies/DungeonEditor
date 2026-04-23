using Application.Common;
using Domain.Common;
using Domain.Users;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Get;

public sealed class GetUserHandler(IReadOnlyDataContext dataContext) : IQueryHandler<GetUserQuery, GetUserResult>
{
    public async Task<Result<GetUserResult>> Handle(GetUserQuery query, CancellationToken cancellationToken)
    {
        return await dataContext.Users.Where(x => x.Id == query.Id)
            .Select(x => new GetUserResult(x.Id, x.UserName, x.Balance, x.Roles.Any(r => r.Name == RoleName.Admin)))
            .FirstOrDefaultAsync(cancellationToken);
    }
}
