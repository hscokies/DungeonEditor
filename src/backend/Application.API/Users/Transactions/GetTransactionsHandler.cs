using Application.Common;
using Domain.Common;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Transactions;

public sealed class GetTransactionsHandler(IReadOnlyDataContext dataContext) : IQueryHandler<GetTransactionsQuery, GetTransactionsResult>
{

    public async Task<Result<GetTransactionsResult>> Handle(GetTransactionsQuery query, CancellationToken cancellationToken)
    {
        var transactions = await dataContext.Transactions.Where(x => x.UserId == query.UserId)
            .OrderBy(x => x.CreatedAt)
            .Skip(query.Offset)
            .Take(query.Limit)
            .Select(x => new TransactionDto(x.Id, x.Type, x.CreatedAt, x.Amount, x.Description))
            .ToArrayAsync(cancellationToken);

        return new GetTransactionsResult(transactions);
    }
}
