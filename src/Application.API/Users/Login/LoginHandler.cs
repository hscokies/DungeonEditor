using Application.Common;
using Application.Common.Errors;
using Domain.Common;
using Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace Application.Users.Login;

public sealed class LoginHandler(SignInManager<User> signInManager) : ICommandHandler<LoginCommand>
{
    public async Task<Result> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        var result = await signInManager.PasswordSignInAsync(command.UserName, command.Password, true, true);
        if (result.Succeeded)
        {
            return Result.Success();
        }

        if (result.IsLockedOut)
        {
            return AuthorizationErrors.UserLockedOut;
        }

        return result.IsNotAllowed
            ? AuthorizationErrors.NotAllowed
            : AuthorizationErrors.InvalidAuthorization;
    }
}
