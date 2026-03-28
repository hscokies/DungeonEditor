namespace Infrastructure.Storage.Exceptions;

public class BucketNotFoundException(Exception innerException) : ObjectStoreException(innerException);