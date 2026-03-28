namespace Infrastructure.Storage.Exceptions;

public class InvalidBucketNameException(Exception innerException) : ObjectStoreException(innerException);