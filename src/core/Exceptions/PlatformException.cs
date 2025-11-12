namespace Roblox.Platform.Core;

using System;

/// <summary>
/// Represents errors that occur during application execution.
/// </summary>
public class PlatformException : Exception
{
    /// <summary>
    /// Gets the user facing message.
    /// </summary>
    [Obsolete("User facing messaging should be handled by the topmost consumer, not Platform code")]
    public readonly string UserFacingMessage;

    /// <summary>
    /// Initializes a new instance of the <see cref="PlatformException" /> class.
    /// </summary>
    public PlatformException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PlatformException" /> class with the given error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="userFacingMessage">The user facing message.</param>
    public PlatformException(string message, string userFacingMessage = null)
        : base(message)
    {
        UserFacingMessage = userFacingMessage;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PlatformException" /> class with
    /// the given error message and inner exception.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
    public PlatformException(string message, Exception innerException) 
        : base(message, innerException)
    {
    }
}
