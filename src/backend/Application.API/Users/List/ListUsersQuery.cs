using Application.Common;

namespace Application.Users.List;

public sealed record ListUsersQuery(string? Search, ushort Offset, ushort Limit) : IQuery<ListUsersResult>
{
    public const string Path = "/users";
}
