namespace Domain.Users;

public static class CommonTransactions
{
    public static OutboundTransaction SaveFileUpload(Guid userId) => new()
    {
        UserId = userId,
        Amount = Price.SaveFileUpload,
        Description = "Save file upload",
    };

    public static InboundTransaction SaveFileRefund(Guid userId) => new()
    {
        UserId = userId,
        Amount = Price.SaveFileUpload,
        Description = "Refund: Unable to locate dungeons",
    };
}
