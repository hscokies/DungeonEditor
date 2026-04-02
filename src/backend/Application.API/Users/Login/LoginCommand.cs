using Application.Common;
using Domain.Common;

namespace Application.Users.Login;

public sealed record LoginCommand(string UserName, string Password) : ICommand
{
    public const string Path = "/session";
}
