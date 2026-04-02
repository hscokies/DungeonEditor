namespace Infrastructure.Storage.Exceptions;

public class InvalidObjectNameException(Exception innerException) : ObjectStoreException(innerException);