using Application.Common;

namespace Application.Users.Edit;

public sealed record EditUserCommand(Guid Id, short  Balance) : ICommand
{
    public const string Path = "/users/{id:guid}";
}
