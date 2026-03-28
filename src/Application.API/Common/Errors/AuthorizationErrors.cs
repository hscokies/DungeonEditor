using Domain.Common;

namespace Application.Common.Errors;

internal static class AuthorizationErrors
{
    public static Error InvalidAuthorization => new("Authorization.Invalid", "Invalid username or password", ErrorType.Problem);
    public static Error UserLockedOut => new("Authorization.Lockout ", "Too many invalid authorization attempts", ErrorType.Problem);
    public static Error NotAllowed => new("Authorization.NotAllowed ", "The user is not authorized to access this account", ErrorType.Problem);
}
