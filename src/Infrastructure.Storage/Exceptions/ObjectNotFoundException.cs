namespace Infrastructure.Storage.Exceptions;

public class ObjectNotFoundException(Exception innerException) : ObjectStoreException(innerException);