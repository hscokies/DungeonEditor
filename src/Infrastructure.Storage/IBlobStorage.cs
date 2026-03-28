namespace Infrastructure.Storage;

public interface IBlobStorage
{
    public Task<IReadOnlyCollection<Blob>> List(
        string path,
        bool recursive = false,
        CancellationToken cancellationToken = default);

    public Task<Stream> OpenRead(
        string path,
        CancellationToken cancellationToken = default);

    public Task<Blob> Write(
        Stream stream,
        string contentType,
        string path,
        CancellationToken cancellationToken = default);

    public Task<Blob> Copy(
        string sourcePath,
        string remotePath,
        CancellationToken cancellationToken = default);

    public Task Delete(
        string path,
        CancellationToken cancellationToken = default);

    public Task Delete(IEnumerable<string> paths,
        CancellationToken cancellationToken = default);

    public Task<Blob> Get(
        string path,
        CancellationToken cancellationToken = default);

    public Task<Uri> GetUri(string path,
        TimeSpan lifeTime,
        CancellationToken cancellationToken = default);
}
