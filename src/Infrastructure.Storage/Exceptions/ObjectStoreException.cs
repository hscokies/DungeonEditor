namespace Infrastructure.Storage.Exceptions;

public class ObjectStoreException(Exception innerException) : Exception(innerException.Message, innerException);