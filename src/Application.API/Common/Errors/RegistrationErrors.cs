using Domain.Common;

namespace Application.Common.Errors;

internal static class RegistrationErrors
{
    internal static readonly Error DuplicateUserName = new("Registration.DuplicateUserName", "A user with the specified username already exists", ErrorType.Validation);
    internal static readonly Error InvalidUserName = new("Registration.InvalidUserName", "The username may contain only letters and digits", ErrorType.Validation);
    internal static readonly Error InvalidPasswordFormat = new("Registration.InvalidPasswordFormat", "The password must be at least 12 characters long and include at least one lowercase letter, one uppercase letter, one digit, and one special character.", ErrorType.Validation);
}
