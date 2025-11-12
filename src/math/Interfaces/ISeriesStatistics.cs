namespace Roblox.Platform.Math;

using System;
using System.Collections.Generic;

/// <summary>
/// Represents statistics for a series of data.
/// </summary>
public interface ISeriesStatistics
{
    /// <summary>
    /// Gets the date range for the data.
    /// </summary>
    DateRange DateRange { get; }

    /// <summary>
    /// Gets the number of data points.
    /// </summary>
    int Count { get; }

    /// <summary>
    /// Gets the maximum value.
    /// </summary>
    double Maximum { get; }

    /// <summary>
    /// Gets the minimum value.
    /// </summary>
    double Minimum { get; }

    /// <summary>
    /// Gets the average value.
    /// </summary>
    double Average { get; }

    /// <summary>
    /// Gets the standard deviation.
    /// </summary>
    double StandardDeviation { get; }

    /// <summary>
    /// Gets the sum of the data.
    /// </summary>
    double Sum { get; }

    /// <summary>
    /// Gets the latest data point.
    /// </summary>
    KeyValuePair<DateTime, double>? LatestDataPoint { get; }

    /// <summary>
    /// Gets the maximum data point.
    /// </summary>
    KeyValuePair<DateTime, double>? MaximumDataPoint { get; }

    /// <summary>
    /// Gets the minimum data point.
    /// </summary>
    KeyValuePair<DateTime, double>? MinimumDataPoint { get; }
}
