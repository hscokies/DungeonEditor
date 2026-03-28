using Application.Common;

namespace Application.Users.Transactions;

public sealed record GetTransactionsQuery(Guid UserId) : IQuery<GetTransactionsResult>
{
    public const string Path = "/users/transactions";
}
