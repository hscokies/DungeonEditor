using Infrastructure.AppSettings.Models;
using Infrastructure.Storage.Exceptions;
using Microsoft.Extensions.Options;
using MimeMapping;
using Minio;
using Minio.DataModel.Args;
using NLog;
using BucketNotFoundException = Minio.Exceptions.BucketNotFoundException;
using InvalidBucketNameException = Minio.Exceptions.InvalidBucketNameException;
using InvalidObjectNameException = Minio.Exceptions.InvalidObjectNameException;
using ObjectNotFoundException = Minio.Exceptions.ObjectNotFoundException;

namespace Infrastructure.Storage.Providers;

internal sealed class MinioBlobStorage : IBlobStorage
{
    private readonly IMinioClient _client;
    private readonly Logger _logger = LogManager.GetCurrentClassLogger();

    private readonly MinioSettings _settings;

    public MinioBlobStorage(IOptions<MinioSettings> options)
    {
        var settings = _settings = options.Value;
        _client = new MinioClient()
            .WithCredentials(settings.AccessKey, settings.SecretKey)
            .WithEndpoint(settings.Endpoint)
            // .WithSSL()
            .Build();
    }


    public async Task<IReadOnlyCollection<Blob>> List(
        string path,
        bool recursive = false,
        CancellationToken cancellationToken = default)
    {
        var args = new ListObjectsArgs()
            .WithBucket(_settings.Bucket)
            .WithPrefix(path)
            .WithRecursive(recursive);

        try
        {
            var collection = new List<Blob>();
            await foreach (var item in _client.ListObjectsEnumAsync(args, cancellationToken).ConfigureAwait(false))
            {
                var blob = new Blob
                {
                    Type = item.IsDir ? BlobType.Directory : BlobType.File,
                    ETag = item.ETag,
                    Path = item.Key,
                    Size = item.Size,
                    ContentType = item.ContentType,
                    LastModified = item.LastModifiedDateTime,
                };

                collection.Add(blob);
            }

            return collection;
        }
        catch (Exception e)
        {
            _logger.Error(e, "Failed to get objects list: {Message}", e.Message);
            throw e switch
            {
                InvalidBucketNameException => new Exceptions.InvalidBucketNameException(e),
                BucketNotFoundException => new Exceptions.BucketNotFoundException(e),
                _ => throw new ObjectStoreException(e),
            };
        }
    }

    public async Task<Stream> OpenRead(string path, CancellationToken cancellationToken = default)
    {
        var localPath = Path.Combine(
            Path.GetTempPath(),
            Path.GetRandomFileName());

        var args = new GetObjectArgs()
            .WithBucket(_settings.Bucket)
            .WithFile(localPath)
            .WithObject(path);

        try
        {
            await _client.GetObjectAsync(args, cancellationToken);
            return new FileStream(
                localPath, FileMode.Open,
                FileAccess.Read, FileShare.Read,
                4096, FileOptions.DeleteOnClose);
        }
        catch (Exception e)
        {
            _logger.Error(e, "Failed to open object for read: {Message}", e.Message);
            throw e switch
            {
                InvalidBucketNameException => new Exceptions.InvalidBucketNameException(e),
                InvalidObjectNameException => new Exceptions.InvalidObjectNameException(e),
                BucketNotFoundException => new Exceptions.BucketNotFoundException(e),
                ObjectNotFoundException => new Exceptions.ObjectNotFoundException(e),
                _ => throw new ObjectStoreException(e),
            };
        }
    }

    public async Task<Blob> Write(
        Stream stream,
        string contentType,
        string? path,
        CancellationToken cancellationToken = default)
    {
        var args = new PutObjectArgs()
            .WithBucket(_settings.Bucket)
            .WithObject(path)
            .WithStreamData(stream)
            .WithObjectSize(stream.Length)
            .WithContentType(contentType);

        try
        {
            var result = await _client.PutObjectAsync(args, cancellationToken);
            return new Blob
            {
                ContentType = contentType,
                ETag = result.Etag,
                Path = result.ObjectName,
                Size = (ulong?)result.Size,
                Type = BlobType.File,
            };
        }
        catch (Exception e)
        {
            _logger.Error(e, "Failed to write object from stream: {Message}", e.Message);
            throw e switch
            {
                InvalidBucketNameException => new Exceptions.InvalidBucketNameException(e),
                InvalidObjectNameException => new Exceptions.InvalidObjectNameException(e),
                BucketNotFoundException => new Exceptions.BucketNotFoundException(e),
                ObjectNotFoundException => new Exceptions.ObjectNotFoundException(e),
                _ => throw new ObjectStoreException(e),
            };
        }
    }

    public async Task<Blob> Copy(string sourcePath, string remotePath, CancellationToken cancellationToken = default)
    {
        var contentType = MimeUtility.GetMimeMapping(sourcePath);
        var args = new PutObjectArgs()
            .WithBucket(_settings.Bucket)
            .WithObject(remotePath)
            .WithFileName(sourcePath)
            .WithContentType(contentType);

        try
        {
            var result = await _client.PutObjectAsync(args, cancellationToken);
            return new Blob
            {
                ETag = result.Etag,
                Path = result.ObjectName,
                Size = (ulong?)result.Size,
                Type = BlobType.File,
                ContentType = contentType,
            };
        }
        catch (Exception e)
        {
            _logger.Error(e, "Failed to copy object: {Message}", e.Message);
            throw e switch
            {
                InvalidBucketNameException => new Exceptions.InvalidBucketNameException(e),
                InvalidObjectNameException => new Exceptions.InvalidObjectNameException(e),
                BucketNotFoundException => new Exceptions.BucketNotFoundException(e),
                ObjectNotFoundException => new Exceptions.ObjectNotFoundException(e),
                _ => throw new ObjectStoreException(e),
            };
        }
    }

    public async Task Delete(string path, CancellationToken cancellationToken = default)
    {
        var args = new RemoveObjectArgs()
            .WithBucket(_settings.Bucket)
            .WithObject(path);

        try
        {
            await _client.RemoveObjectAsync(args, cancellationToken);
        }
        catch (Exception e)
        {
            _logger.Error(e, "Failed to delete object: {Message}", e.Message);
            throw e switch
            {
                InvalidBucketNameException => new Exceptions.InvalidBucketNameException(e),
                InvalidObjectNameException => new Exceptions.InvalidObjectNameException(e),
                BucketNotFoundException => new Exceptions.BucketNotFoundException(e),
                ObjectNotFoundException => new Exceptions.ObjectNotFoundException(e),
                _ => throw new ObjectStoreException(e),
            };
        }
    }

    public async Task Delete(IEnumerable<string> paths, CancellationToken cancellationToken = default)
    {
        var args = new RemoveObjectsArgs()
            .WithBucket(_settings.Bucket)
            .WithObjects(paths.ToArray());

        try
        {
            await _client.RemoveObjectsAsync(args, cancellationToken);
        }
        catch (Exception e)
        {
            _logger.Error(e, "Failed to delete object: {Message}", e.Message);
            throw e switch
            {
                InvalidBucketNameException => new Exceptions.InvalidBucketNameException(e),
                InvalidObjectNameException => new Exceptions.InvalidObjectNameException(e),
                BucketNotFoundException => new Exceptions.BucketNotFoundException(e),
                ObjectNotFoundException => new Exceptions.ObjectNotFoundException(e),
                _ => throw new ObjectStoreException(e),
            };
        }
    }

    public async Task<Blob> Get(string path, CancellationToken cancellationToken = default)
    {
        var args = new StatObjectArgs()
            .WithBucket(_settings.Bucket)
            .WithObject(path);

        try
        {
            var result = await _client.StatObjectAsync(args, cancellationToken);
            return new Blob
            {
                ContentType = result.ContentType,
                ETag = result.ETag,
                LastModified = result.LastModified,
                Path = result.ObjectName,
                Size = (ulong?)result.Size,
            };
        }
        catch (Exception e)
        {
            _logger.Error(e, "Failed to get object stats: {Message}", e.Message);
            throw e switch
            {
                InvalidBucketNameException => new Exceptions.InvalidBucketNameException(e),
                InvalidObjectNameException => new Exceptions.InvalidObjectNameException(e),
                BucketNotFoundException => new Exceptions.BucketNotFoundException(e),
                ObjectNotFoundException => new Exceptions.ObjectNotFoundException(e),
                _ => throw new ObjectStoreException(e),
            };
        }
    }

    public async Task<Uri> GetUri(string path, TimeSpan lifeTime, CancellationToken cancellationToken = default)
    {
        var args = new PresignedGetObjectArgs()
            .WithBucket(_settings.Bucket)
            .WithObject(path)
            .WithExpiry((int)lifeTime.TotalSeconds);

        try
        {
            var url = await _client.PresignedGetObjectAsync(args);
            return new Uri(url);
        }
        catch (Exception e)
        {
            _logger.Error(e, "Failed to generate object url: {Message}", e.Message);
            throw e switch
            {
                InvalidBucketNameException => new Exceptions.InvalidBucketNameException(e),
                InvalidObjectNameException => new Exceptions.InvalidObjectNameException(e),
                BucketNotFoundException => new Exceptions.BucketNotFoundException(e),
                ObjectNotFoundException => new Exceptions.ObjectNotFoundException(e),
                _ => throw new ObjectStoreException(e),
            };
        }
    }
}
