using Domain.Common;

namespace Application.Common.Errors;

internal static class AuthorizationErrors
{
    public static Error InvalidAuthorization => Error.Validation("Authorization.Invalid", "Invalid username or password");

    public static Error UserLockedOut => Error.Validation("Authorization.Lockout", "Too many invalid authorization attempts");

    public static Error NotAllowed => Error.Validation("Authorization.NotAllowed", "The user is not authorized to access this account");
}
