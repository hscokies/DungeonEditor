using Application.Common;
using Domain.Common;
using Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace Application.Users.Logout;

public sealed class LogoutHandler(SignInManager<User> signInManager) : ICommandHandler
{
    public async Task<Result> Handle(CancellationToken cancellationToken)
    {
        await signInManager.SignOutAsync();
        return Result.Success();
    }
}
