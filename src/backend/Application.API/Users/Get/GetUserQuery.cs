using Application.Common;

namespace Application.Users.Get;

public sealed record GetUserQuery(Guid Id) : IQuery<GetUserResult>
{
    public const string Path = "/users/me";
}
