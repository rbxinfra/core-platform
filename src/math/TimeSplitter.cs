namespace Roblox.Platform.Math;

using System;
using System.Linq;
using System.Collections.Generic;

/// <summary>
/// Represents an aggregation time span.
/// </summary>
public static class TimeSplitter
{
    /// <summary>
    /// Gets the complete parts.
    /// </summary>
    /// <param name="segments">The segments.</param>
    /// <param name="rangeStartTime">The range start time.</param>
    /// <param name="rangeEndTime">The range end time.</param>
    /// <returns>The complete parts.</returns>
    public static IEnumerable<TimeSegment> GetCompleteParts(IEnumerable<TimeSegment> segments, DateTime rangeStartTime, DateTime rangeEndTime) 
        => segments.Where(s => s.StartTime >= rangeStartTime && s.EndTime <= rangeEndTime);

    /// <summary>
    /// Splits into parts.
    /// </summary>
    /// <param name="rangeStartTime">The range start time.</param>
    /// <param name="rangeEndTime">The range end time.</param>
    /// <param name="timeSpan">The time span.</param>
    /// <param name="timeZoneOffset">The time zone offset.</param>
    /// <returns>The parts.</returns>
    public static IEnumerable<TimeSegment> SplitIntoParts(DateTime rangeStartTime, DateTime rangeEndTime, AggregationTimeSpan timeSpan, TimeSpan timeZoneOffset)
    {
        var startTime = GetPartStartTime(rangeStartTime, timeSpan, timeZoneOffset);
        var partTimeSpan = GetPartTimeSpan(timeSpan);

        DateTime chunkEnd;
        do
        {
            chunkEnd = timeSpan == AggregationTimeSpan.Month 
                ? startTime.AddMonths(1) 
                : startTime.Add(partTimeSpan);

            yield return new TimeSegment(startTime, chunkEnd);

            startTime = chunkEnd;
        }
        while (chunkEnd < rangeEndTime);
    }

    /// <summary>
    /// Splits into complete parts.
    /// </summary>
    /// <param name="rangeStartTime">The range start time.</param>
    /// <param name="rangeEndTime">The range end time.</param>
    /// <param name="timeSpan">The time span.</param>
    /// <param name="timeZoneOffset">The time zone offset.</param>
    /// <returns>The complete parts.</returns>
    public static IEnumerable<TimeSegment> SplitIntoCompleteParts(DateTime rangeStartTime, DateTime rangeEndTime, AggregationTimeSpan timeSpan, TimeSpan timeZoneOffset) 
        => GetCompleteParts(SplitIntoParts(rangeStartTime, rangeEndTime, timeSpan, timeZoneOffset), rangeStartTime, rangeEndTime);

    private static DateTime GetPartStartTime(DateTime rangeStartTime, AggregationTimeSpan timeSpan, TimeSpan timeZoneOffset)
    {
        return timeSpan switch
        {
            AggregationTimeSpan.FifteenMinutes => rangeStartTime.Date.AddHours(rangeStartTime.Hour).AddMinutes(rangeStartTime.Minute - rangeStartTime.Minute % 15),
            AggregationTimeSpan.Hour => rangeStartTime.Date.AddHours(rangeStartTime.Hour),
            AggregationTimeSpan.Day => rangeStartTime.Date.Add(-timeZoneOffset),
            AggregationTimeSpan.Week => rangeStartTime.Date.AddDays(-(double)rangeStartTime.DayOfWeek + 1).Add(-timeZoneOffset),
            AggregationTimeSpan.Month => rangeStartTime.Date.AddDays((double)(-(double)rangeStartTime.Day + 1)).Add(-timeZoneOffset),
            _ => rangeStartTime,
        };
    }

    private static TimeSpan GetPartTimeSpan(AggregationTimeSpan timeSpan)
    {
        return timeSpan switch
        {
            AggregationTimeSpan.FifteenMinutes => TimeSpan.FromMinutes(15),
            AggregationTimeSpan.Hour => TimeSpan.FromHours(1),
            AggregationTimeSpan.Day => TimeSpan.FromDays(1),
            AggregationTimeSpan.Week => TimeSpan.FromDays(7),
            AggregationTimeSpan.Month => TimeSpan.FromDays(30),
            _ => TimeSpan.FromHours(1),
        };
    }
}
