using System.Collections.Concurrent;
using Domain.Common;
using Domain.Users;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.Users;

public sealed class BalanceService(IDbContextFactory<DataContext> dbContextFactory) : IBalanceService
{
    private static readonly ConcurrentDictionary<Guid, SemaphoreSlim> Locks = new();


    public async Task<Result> Charge(OutboundTransaction transaction, CancellationToken cancellationToken)
    {
        var userId = transaction.UserId;
        var amount = transaction.Amount;

        var semaphore = Locks.GetOrAdd(userId, _ => new SemaphoreSlim(1, 1));
        await semaphore.WaitAsync(TimeSpan.FromSeconds(30), cancellationToken);

        var dataContext = await dbContextFactory.CreateDbContextAsync(cancellationToken);

        var user = await dataContext.Users.FindAsync([userId], cancellationToken);
        if (user is null)
        {
            return UserErrors.NotFound;
        }

        if (user.Balance < amount)
        {
            return UserErrors.NotEnoughFunds;
        }

        user.Balance -= amount;
        dataContext.Transactions.Add(transaction);

        await dataContext.SaveChangesAsync(cancellationToken);
        semaphore.Release();

        return Result.Success();
    }

    public async Task<Result> Deposit(InboundTransaction transaction, CancellationToken cancellationToken)
    {
        var userId = transaction.UserId;
        var semaphore = Locks.GetOrAdd(userId, _ => new SemaphoreSlim(1, 1));
        await semaphore.WaitAsync(TimeSpan.FromSeconds(30), cancellationToken);

        var dataContext = await dbContextFactory.CreateDbContextAsync(cancellationToken);

        var user = await dataContext.Users.FindAsync([userId], cancellationToken);
        if (user is null)
        {
            return UserErrors.NotFound;
        }

        user.Balance += transaction.Amount;
        dataContext.Transactions.Add(transaction);

        await dataContext.SaveChangesAsync(cancellationToken);
        semaphore.Release();

        return Result.Success();
    }
}
