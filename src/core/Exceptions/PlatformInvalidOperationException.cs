namespace Roblox.Platform.Core;

using System;

/// <summary>
/// Exception thrown when an invalid operation is performed.
/// </summary>
public class PlatformInvalidOperationException : PlatformException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlatformInvalidOperationException" /> class.
    /// </summary>
    public PlatformInvalidOperationException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PlatformInvalidOperationException" /> class with the given error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public PlatformInvalidOperationException(string message) 
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PlatformInvalidOperationException" /> class with
    /// the given error message and inner exception.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
    public PlatformInvalidOperationException(string message, Exception innerException) 
        : base(message, innerException)
    {
    }
}
