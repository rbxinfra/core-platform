namespace Roblox.Platform.Core;

using System;

/// <summary>
/// Exception thrown when a method argument is null.
/// </summary>
public class PlatformArgumentNullException : PlatformException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlatformArgumentNullException" /> class.
    /// </summary>
    public PlatformArgumentNullException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PlatformArgumentNullException" /> class with the given error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public PlatformArgumentNullException(string message) 
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PlatformArgumentNullException" /> class with
    /// the given error message and inner exception.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
    public PlatformArgumentNullException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
