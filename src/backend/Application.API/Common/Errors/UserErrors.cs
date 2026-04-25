using Domain.Common;

namespace Application.Common.Errors;

public static class UserErrors
{
    public static Error NotFound => Error.Validation("User.NotFound", "Specified user cannot be found.");
}
