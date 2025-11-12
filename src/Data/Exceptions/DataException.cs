namespace Lary.Laboratory.Data.Exceptions;

/// <summary>
/// Base exception for data layer errors.
/// </summary>
public class DataException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DataException"/> class.
    /// </summary>
    public DataException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DataException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The error message.</param>
    public DataException(string message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DataException"/> class with a specified error message and inner exception.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="innerException">The inner exception.</param>
    public DataException(string message, Exception innerException) : base(message, innerException)
    {
    }
}

