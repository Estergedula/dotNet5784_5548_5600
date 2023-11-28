namespace DO;
/// <summary>
/// "The object does not exist" exception
/// </summary>
[Serializable]
public class DalDoesNotExistException : Exception
{
    public DalDoesNotExistException(string? message) : base(message) { }
}

/// <summary>
/// Exception of type "object already exists"
/// </summary>
[Serializable]
public class DalAlreadyExistsException : Exception
{
    public DalAlreadyExistsException(string? message) : base(message) { }
}
/// <summary>
/// Exception of type "The requested object cannot be deleted"
/// </summary>
[Serializable]
public class DalDeletionImpossible : Exception
{
    public DalDeletionImpossible(string? message) : base(message) { }
}
public class DalXMLFileLoadCreateException:Exception
{
    public DalXMLFileLoadCreateException(string? message) : base(message) { }
}