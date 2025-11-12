namespace Roblox.Platform.Math;

/// <summary>
/// The time span for which to aggregate data.
/// </summary>
public enum AggregationTimeSpan
{
    /// <summary>
    /// Aggregates data over a 15 minute time span.
    /// </summary>
    FifteenMinutes,

    /// <summary>
    /// Aggregates data over a 1 hour time span.
    /// </summary>
    Hour,

    /// <summary>
    /// Aggregates data over a 1 day time span.
    /// </summary>
    Day,

    /// <summary>
    /// Aggregates data over a 1 week time span.
    /// </summary>
    Week,

    /// <summary>
    /// Aggregates data over a 1 month time span.
    /// </summary>
    Month
}
