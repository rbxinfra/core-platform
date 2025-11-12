namespace Roblox.Platform.Core;

using System;

/// <summary>
/// Exception thrown when there is a problem with the data integrity of some platform data.
/// </summary>
public class PlatformDataIntegrityException : PlatformException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlatformDataIntegrityException" /> class.
    /// </summary>
    public PlatformDataIntegrityException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PlatformDataIntegrityException" /> class with the given error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public PlatformDataIntegrityException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PlatformDataIntegrityException" /> class with
    /// the given error message and inner exception.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
    public PlatformDataIntegrityException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
