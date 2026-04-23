using Application.Common;

namespace Application.Users.Transactions;

public sealed record GetTransactionsQuery(Guid UserId, ushort Offset, ushort Limit) : IQuery<GetTransactionsResult>
{
    public const string Path = "/users/transactions";
}
