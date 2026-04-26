using Application.Common;
using Domain.Common;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.List;

public sealed class ListUsersHandler(IReadOnlyDataContext dataContext) : IQueryHandler<ListUsersQuery, ListUsersResult>
{
    public async Task<Result<ListUsersResult>> Handle(ListUsersQuery query, CancellationToken cancellationToken)
    {
        var queryable = dataContext.Users.AsQueryable();
        if (!string.IsNullOrWhiteSpace(query.Search))
        {
            var normalizedSearch = query.Search.ToUpper();
            queryable = queryable.Where(x => EF.Functions.Like(x.NormalizedUserName, $"{normalizedSearch}%"));
        }

        var users = await queryable.Skip(query.Offset)
            .Take(query.Limit)
            .Select(x => new UserDto(x.Id, x.UserName!, x.Balance))
            .ToListAsync(cancellationToken);
            
        return new ListUsersResult(users);
    }
}
