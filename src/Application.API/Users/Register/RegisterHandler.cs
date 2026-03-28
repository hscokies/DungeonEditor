using Application.Common;
using Application.Common.Errors;
using Domain.Common;
using Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace Application.Users.Register;

public class RegisterHandler(UserManager<User> userManager, SignInManager<User> signInManager) : ICommandHandler<RegisterCommand>
{
    public async Task<Result> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        const short defaultBalance = 100;

        var user = new User()
        {
            Id = Guid.CreateVersion7(),
            UserName = command.UserName,
            Balance = defaultBalance,
        };

        var result = await userManager.CreateAsync(user, command.Password);

        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, RoleName.User);
            await signInManager.SignInAsync(user, false);
            
            return Result.Success();
        }

        var validationErrors = new Dictionary<string, IEnumerable<Error>>();
        var errorCodes = result.Errors.Select(x => x.Code).ToHashSet();

        var usernameErrors = GetUserNameErrors(errorCodes).ToArray();
        if (usernameErrors.Length > 0)
        {
            validationErrors[nameof(command.UserName)] = usernameErrors;
        }
        
        var passwordErrors = GetPasswordErrors(errorCodes).ToArray();
        if (passwordErrors.Length > 0)
        {
            validationErrors[nameof(command.Password)] = passwordErrors;
        }

        return new ValidationError(validationErrors);
    }

    private static IEnumerable<Error> GetUserNameErrors(HashSet<string> errors)
    {
        if (errors.Contains(IdentityErrors.DuplicateUserName))
        {
            yield return RegistrationErrors.DuplicateUserName;
        }

        if (errors.Contains(IdentityErrors.InvalidUserName))
        {
            yield return RegistrationErrors.InvalidUserName;
        }
    }

    private static IEnumerable<Error> GetPasswordErrors(HashSet<string> errors)
    {
        ICollection<string> passwordErrors =
        [
            IdentityErrors.PasswordRequiresDigit,
            IdentityErrors.PasswordRequiresLower,
            IdentityErrors.PasswordRequiresNonAlphanumeric,
            IdentityErrors.PasswordRequiresUpper,
            IdentityErrors.PasswordRequiresUniqueChars,
            IdentityErrors.PasswordTooShort,
        ];

        if (passwordErrors.Any(errors.Contains))
        {
            return [RegistrationErrors.InvalidPasswordFormat];
        }

        return [];
    }
}
