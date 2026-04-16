using Domain.Common;

namespace Infrastructure.Services.Users;

internal static class UserErrors
{
    internal static readonly Error NotFound = Error.NotFound("User.NotFound", "Specified user cannot be found");
    internal static readonly Error NotEnoughFunds = Error.Validation("User.NotEnoughFunds", "Not enough funds");
}
