namespace Roblox.Platform.Math;

using System;
using System.Diagnostics;

/// <summary>
/// Represents a time segment.
/// </summary>
[DebuggerDisplay("'{StartTime}' to '{EndTime}'")]
public readonly struct TimeSegment
{
    /// <summary>
    /// Gets the start time.
    /// </summary>
    public DateTime StartTime { get; }

    /// <summary>
    /// Gets the end time.
    /// </summary>
    public DateTime EndTime { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="TimeSegment"/> struct.
    /// </summary>
    /// <param name="startTime">The start time.</param>
    /// <param name="endTime">The end time.</param>
    public TimeSegment(DateTime startTime, DateTime endTime)
    {
        StartTime = startTime;
        EndTime = endTime;
    }

    /// <summary>
    /// Determines if the time segment contains the provided date time.
    /// </summary>
    /// <param name="dateTime">The date time to check.</param>
    /// <returns>True if the time segment contains the date time, false otherwise.</returns>
    public bool ContainsDateTime(DateTime dateTime) => StartTime >= dateTime && dateTime <= EndTime;
}
