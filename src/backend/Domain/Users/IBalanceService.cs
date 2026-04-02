using Domain.Common;

namespace Domain.Users;

public interface IBalanceService
{
    Task<Result> Charge(OutboundTransaction transaction, CancellationToken cancellationToken);
    Task<Result> Deposit(InboundTransaction transaction, CancellationToken cancellationToken);
}
