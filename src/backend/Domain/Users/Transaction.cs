namespace Domain.Users;

public abstract class Transaction
{
    public Guid Id { get; init; }

    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;

    public TransactionType Type { get; init; }

    public required string Description { get; init; }

    public required Guid UserId { get; init; }

    public required short Amount { get; init; }
}

public sealed class InboundTransaction : Transaction;

public sealed class OutboundTransaction : Transaction;

public enum TransactionType
{
    Inbound,
    Outbound,
}
