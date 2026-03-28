namespace Application.Common.Errors;

public static class IdentityErrors
{
    /// <summary>Invalid username</summary>
    public const string InvalidUserName = nameof(InvalidUserName);

    /// <summary>Duplicate username</summary>
    public const string DuplicateUserName = nameof(DuplicateUserName);

    /// <summary>Password too short</summary>
    public const string PasswordTooShort = nameof(PasswordTooShort);

    /// <summary>Password requires unique chars</summary>
    public const string PasswordRequiresUniqueChars = nameof(PasswordRequiresUniqueChars);

    /// <summary>Password requires non-alphanumeric character</summary>
    public const string PasswordRequiresNonAlphanumeric = nameof(PasswordRequiresNonAlphanumeric);

    /// <summary>Password requires digit</summary>
    public const string PasswordRequiresDigit = nameof(PasswordRequiresDigit);

    /// <summary>Password requires lower case</summary>
    public const string PasswordRequiresLower = nameof(PasswordRequiresLower);

    /// <summary>Password requires upper case</summary>
    public const string PasswordRequiresUpper = nameof(PasswordRequiresUpper);
}
