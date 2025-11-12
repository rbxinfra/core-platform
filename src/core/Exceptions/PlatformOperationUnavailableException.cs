namespace Roblox.Platform.Core;

using System;

/// <summary>
/// Exception thrown when a platform operation is not currently available at no fault of the caller, but may be available later.
/// </summary>
public class PlatformOperationUnavailableException : PlatformException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlatformOperationUnavailableException" /> class.
    /// </summary>
    public PlatformOperationUnavailableException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PlatformOperationUnavailableException" /> class with the given error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public PlatformOperationUnavailableException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PlatformOperationUnavailableException" /> class with
    /// the given error message and inner exception.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
    public PlatformOperationUnavailableException(string message, Exception innerException) 
        : base(message, innerException)
    {
    }
}
