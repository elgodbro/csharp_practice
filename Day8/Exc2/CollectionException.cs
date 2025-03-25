namespace Exc2;

public class CollectionException(string message, Exception innerException) : Exception(message, innerException);