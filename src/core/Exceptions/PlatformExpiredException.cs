namespace Roblox.Platform.Core;

using System;

/// <summary>
/// An exception thrown when something is expired at the platform level.
/// </summary>
public class PlatformExpiredException : PlatformException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlatformExpiredException" /> class.
    /// </summary>
    public PlatformExpiredException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PlatformExpiredException" /> class with the given error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public PlatformExpiredException(string message) 
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PlatformExpiredException" /> class with
    /// the given error message and inner exception.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="exception">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
    public PlatformExpiredException(string message, Exception exception) 
        : base(message, exception)
    {
    }
}
