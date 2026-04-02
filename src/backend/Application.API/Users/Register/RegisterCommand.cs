using Application.Common;
using Domain.Common;

namespace Application.Users.Register;

public sealed record RegisterCommand(string UserName, string Password) : ICommand
{
    public const string Path = "/users";
}