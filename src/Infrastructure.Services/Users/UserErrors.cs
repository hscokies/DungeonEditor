using Domain.Common;

namespace Infrastructure.Services.Users;

internal static class UserErrors
{
    internal static Error NotFound(string description = "Specified user cannot be found.") => Error.NotFound("User.NotFound", description);
    internal static Error NotEnoughFunds(string description) => new Error("User.NotEnoughFunds", description, ErrorType.Validation);
}
