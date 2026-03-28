using Domain.Common;

namespace Application.Common.Errors;

internal static class AuthorizationErrors
{
    public static Error InvalidAuthorization => Error.Problem("Authorization.Invalid", "Invalid username or password");

    public static Error UserLockedOut => Error.Problem("Authorization.Lockout ", "Too many invalid authorization attempts");

    public static Error NotAllowed => Error.Problem("Authorization.NotAllowed ", "The user is not authorized to access this account");
}
