namespace Roblox.Platform.Core;

using System;

/// <summary>
/// Exception thrown when a method argument is that of an invalid enum value is invalid.
/// </summary>
public class PlatformInvalidEnumArgumentException : PlatformArgumentException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlatformInvalidEnumArgumentException" /> class.
    /// </summary>
    public PlatformInvalidEnumArgumentException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PlatformInvalidEnumArgumentException" /> class with the given error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public PlatformInvalidEnumArgumentException(string message) 
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PlatformInvalidEnumArgumentException" /> class with
    /// the given error message and inner exception.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
    public PlatformInvalidEnumArgumentException(string message, Exception innerException) 
        : base(message, innerException)
    {
    }
}
