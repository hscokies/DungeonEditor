namespace Application.Users.List;

public sealed record ListUsersResult(ICollection<UserDto> Users);

public sealed record UserDto(Guid Id, string Username, short Balance);

