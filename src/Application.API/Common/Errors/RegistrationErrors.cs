using Domain.Common;

namespace Application.Common.Errors;

internal static class RegistrationErrors
{
    internal static readonly Error DuplicateUserName = Error.Problem("Registration.DuplicateUserName", "A user with the specified username already exists");
    internal static readonly Error InvalidUserName = Error.Problem("Registration.InvalidUserName", "The username may contain only letters and digits");
    internal static readonly Error InvalidPasswordFormat = Error.Problem("Registration.InvalidPasswordFormat", "The password must be at least 12 characters long and include at least one lowercase letter, one uppercase letter, one digit, and one special character.");
}
