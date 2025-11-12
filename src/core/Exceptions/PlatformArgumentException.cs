namespace Roblox.Platform.Core;

using System;

/// <summary>
/// Exception thrown when a method argument is invalid.
/// </summary>
public class PlatformArgumentException : PlatformException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlatformArgumentException" /> class.
    /// </summary>
    public PlatformArgumentException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PlatformArgumentException" /> class with the given error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public PlatformArgumentException(string message) 
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PlatformArgumentException" /> class with
    /// the given error message and inner exception.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
    public PlatformArgumentException(string message, Exception innerException) 
        : base(message, innerException)
    {
    }
}
