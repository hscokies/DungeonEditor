namespace Infrastructure.AppSettings.Models;

public sealed class MessagingSettings : IAppSettings
{
    public required string Host { get; init; }
    public required string VirtualHost { get; init; }
    public required string Username { get; init; }
    public required string Password { get; init; }
}
