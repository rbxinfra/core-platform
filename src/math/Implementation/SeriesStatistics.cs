namespace Roblox.Platform.Math;

using System;
using System.Collections.Generic;

/// <summary>
/// Default implementation of <see cref="ISeriesStatistics"/>.
/// </summary>
internal sealed class SeriesStatistics : ISeriesStatistics
{
    /// <inheritdoc cref="ISeriesStatistics.DateRange"/>
    public DateRange DateRange { get; }

    /// <inheritdoc cref="ISeriesStatistics.Count"/>
    public int Count { get; }

    /// <inheritdoc cref="ISeriesStatistics.Maximum"/>
    public double Maximum { get; }

    /// <inheritdoc cref="ISeriesStatistics.Minimum"/>
    public double Minimum { get; }

    /// <inheritdoc cref="ISeriesStatistics.Average"/>
    public double Average { get; }

    /// <inheritdoc cref="ISeriesStatistics.StandardDeviation"/>
    public double StandardDeviation { get; }

    /// <inheritdoc cref="ISeriesStatistics.Sum"/>
    public double Sum { get; }

    /// <inheritdoc cref="ISeriesStatistics.LatestDataPoint"/>
    public KeyValuePair<DateTime, double>? LatestDataPoint { get; }

    /// <inheritdoc cref="ISeriesStatistics.MaximumDataPoint"/> 
    public KeyValuePair<DateTime, double>? MaximumDataPoint { get; }

    /// <inheritdoc cref="ISeriesStatistics.MinimumDataPoint"/>
    public KeyValuePair<DateTime, double>? MinimumDataPoint { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="SeriesStatistics"/> class.
    /// </summary>
    /// <param name="range">The date range.</param>
    /// <param name="count">The number of data points.</param>
    /// <param name="max">The maximum value.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="avg">The average value.</param>
    /// <param name="stdev">The standard deviation.</param>
    /// <param name="sum">The sum of the data.</param>
    /// <param name="latestPoint">The latest data point.</param>
    /// <param name="maxPoint">The maximum data point.</param>
    /// <param name="minPoint">The minimum data point.</param>
    public SeriesStatistics(
        DateRange range,
        int count,
        double max,
        double min,
        double avg,
        double stdev,
        double sum,
        KeyValuePair<DateTime, double>? latestPoint,
        KeyValuePair<DateTime, double>? maxPoint,
        KeyValuePair<DateTime, double>? minPoint
    )
    {
        DateRange = range;
        Count = count;
        Maximum = max;
        Minimum = min;
        Average = avg;
        StandardDeviation = stdev;
        Sum = sum;
        LatestDataPoint = latestPoint;
        MaximumDataPoint = maxPoint;
        MinimumDataPoint = minPoint;
    }
}
