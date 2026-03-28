namespace Infrastructure.Storage;

public sealed class Blob
{
    public required string Path { get; init; }

    public BlobType Type { get; init; }
    public bool IsDirectory => Type is BlobType.Directory;
    public bool IsFile => Type is BlobType.File;

    public required string ETag { get; init; }
    public required string ContentType { get; set; }
    public ulong? Size { get; init; }

    public DateTime? LastModified { get; init; }
}

public enum BlobType : byte
{
    File = 0,
    Directory = 1,
}