namespace Roblox.Platform.Math;

using System;

/// <summary>
/// Represents an exception that occurs during math operations.
/// </summary>
public sealed class MathException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MathException"/> class.
    /// </summary>
    /// <param name="message">The exception message.</param>
    public MathException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MathException"/> class.
    /// </summary>
    /// <param name="message">The exception message.</param>
    /// <param name="innerEx">The inner exception.</param>
    public MathException(string message, Exception innerEx)
        : base(message, innerEx)
    {
    }
}
