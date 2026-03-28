namespace Infrastructure.AppSettings.Models;

public sealed class MinioSettings : IAppSettings
{
    public required string Endpoint { get; init; }
    public required string Bucket { get; init; }
    public required string AccessKey { get; init; }
    public required string SecretKey { get; init; }
}
