namespace Application.Users.Get;

public sealed record GetUserResult(Guid Id, string Username, short Balance, bool Admin);
