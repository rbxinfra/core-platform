namespace Roblox.Platform.Core;

using System;

/// <summary>
/// Exception thrown when a platform request gets throttled, but may be available later.
/// </summary>
public class PlatformFloodedException : PlatformException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlatformFloodedException" /> class.
    /// </summary>
    public PlatformFloodedException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PlatformFloodedException" /> class with the given error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public PlatformFloodedException(string message) 
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PlatformFloodedException" /> class with
    /// the given error message and inner exception.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
    public PlatformFloodedException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
