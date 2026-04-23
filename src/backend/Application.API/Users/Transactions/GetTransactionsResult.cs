using Domain.Users;

namespace Application.Users.Transactions;

public sealed record GetTransactionsResult(ICollection<TransactionDto> Transactions);

public sealed record TransactionDto(Guid Id, TransactionType Type, DateTime CreatedAt, short Amount, string Description);
