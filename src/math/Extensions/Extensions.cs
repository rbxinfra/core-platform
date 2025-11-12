namespace Roblox.Platform.Math;

using System;
using System.Linq;
using System.Collections.Generic;

using Numerics;
using Statistics;

/// <summary>
/// General math extension methods.
/// </summary>
public static class Extensions
{
    /// <summary>
    /// Gets the standard percentiles for a collection of key-value pairs.
    /// </summary>
    /// <param name="pairs">The pairs.</param>
    /// <returns>The standard percentiles.</returns>
    public static IStandardPercentiles GetStandardPercentiles(this IEnumerable<KeyValuePair<DateTime, double>> pairs)
        => new Percentile(from p in pairs select p.Value).GetStandardPercentiles();

    /// <summary>
    /// Gets the statistics for a collection of key-value pairs.
    /// </summary>
    /// <param name="pairs">The pairs.</param>
    /// <returns>The statistics.</returns>
    public static ISeriesStatistics GetStatistics(this IEnumerable<KeyValuePair<DateTime, double>> pairs)
    {
        var s = new TimeSeries(TimeSeries.AddConflictResolution.Average);
        s.AddDataPoints(pairs);

        return new SeriesStatistics(
            s.DateRange,
            s.Count,
            s.Maximum,
            s.Minimum,
            s.Average,
            s.StandardDeviation,
            s.Sum,
            s.LatestDataPoint,
            s.MaximumDataPoint,
            s.MinimumDataPoint
        );
    }

    /// <summary>
    /// Gets the number of times a value is greater than a threshold.
    /// </summary>
    /// <param name="pairs">The pairs.</param>
    /// <param name="threshhold">The threshhold.</param>
    /// <returns>The number of times a value is greater than a threshold.</returns>
    public static int NumberOfTimesGreaterThanThreshhold(this IEnumerable<KeyValuePair<DateTime, double>> pairs, double threshhold)
    {
        var timeSeries = new TimeSeries(TimeSeries.AddConflictResolution.Average);
        timeSeries.AddDataPoints(pairs);

        return timeSeries.NumberOfTimesGreaterThanThreshhold(threshhold);
    }

    /// <summary>
    /// Gets the number of times a value is less than a threshold.
    /// </summary>
    /// <param name="pairs">The pairs.</param>
    /// <param name="threshhold">The threshhold.</param>
    /// <returns>The number of times a value is less than a threshold.</returns>
    public static int NumberOfTimesLessThanThreshhold(this IEnumerable<KeyValuePair<DateTime, double>> pairs, double threshhold)
    {
        var timeSeries = new TimeSeries(TimeSeries.AddConflictResolution.Average);
        timeSeries.AddDataPoints(pairs);

        return timeSeries.NumberOfTimesLessThanThreshhold(threshhold);
    }

    /// <summary>
    /// Time shifts the pairs.
    /// </summary>
    /// <param name="pairs">The pairs.</param>
    /// <param name="shiftAmount">The shift amount.</param>
    /// <returns>The time shifted pairs.</returns>
    public static IEnumerable<KeyValuePair<DateTime, double>> TimeShift(this IEnumerable<KeyValuePair<DateTime, double>> pairs, TimeSpan shiftAmount)
        => from e in pairs select new KeyValuePair<DateTime, double>(e.Key + shiftAmount, e.Value);

    /// <summary>
    /// Computes the first derivative.
    /// </summary>
    /// <param name="pairs">The pairs.</param>
    /// <param name="units">The units.</param>
    /// <returns>The first derivative.</returns>
    public static IEnumerable<KeyValuePair<DateTime, double>> ComputeFirstDerivative(this IEnumerable<KeyValuePair<DateTime, double>> pairs, TimeUnits units)
    {
        var timeSeries = new TimeSeries(TimeSeries.AddConflictResolution.Average);
        timeSeries.AddDataPoints(pairs);

        return timeSeries.ComputeFirstDerivative(units).KeyValuePairs;
    }

    /// <summary>
    /// Computes the second derivative.
    /// </summary>
    /// <param name="pairs">The pairs.</param>
    /// <param name="units">The units.</param>
    /// <returns>The second derivative.</returns>
    public static IEnumerable<KeyValuePair<DateTime, double>> ComputeSecondDerivative(this IEnumerable<KeyValuePair<DateTime, double>> pairs, TimeUnits units)
    {
        var timeSeries = new TimeSeries(TimeSeries.AddConflictResolution.Average);
        timeSeries.AddDataPoints(pairs);

        return timeSeries.ComputeSecondDerivative(units).KeyValuePairs;
    }

    /// <summary>
    /// Computes the relative increase in percent.
    /// </summary>
    /// <param name="pairs">The pairs.</param>
    /// <returns>The relative increase in percent.</returns>
    public static IEnumerable<KeyValuePair<DateTime, double>> ComputeRelativeIncreaseInPercent(this IEnumerable<KeyValuePair<DateTime, double>> pairs)
    {
        var timeSeries = new TimeSeries(TimeSeries.AddConflictResolution.Average);
        timeSeries.AddDataPoints(pairs);

        return timeSeries.ComputeRelativeIncreaseInPercent().KeyValuePairs;
    }

    /// <summary>
    /// Computes the relative decrease in percent.
    /// </summary>
    /// <param name="pairs">The pairs.</param>
    /// <returns>The relative decrease in percent.</returns>
    public static IEnumerable<KeyValuePair<DateTime, double>> ComputeRelativeDecreaseInPercent(this IEnumerable<KeyValuePair<DateTime, double>> pairs)
    {
        var timeSeries = new TimeSeries(TimeSeries.AddConflictResolution.Average);
        timeSeries.AddDataPoints(pairs);

        return timeSeries.ComputeRelativeDecreaseInPercent().KeyValuePairs;
    }

    /// <summary>
    /// Interpolates the value at.
    /// </summary>
    /// <param name="pairs">The pairs.</param>
    /// <param name="timestamp">The timestamp.</param>
    /// <returns>The interpolated value at.</returns>
    public static double? InterpolateValueAt(this IEnumerable<KeyValuePair<DateTime, double>> pairs, DateTime timestamp)
    {
        var timeSeries = new TimeSeries(TimeSeries.AddConflictResolution.Average);
        timeSeries.AddDataPoints(pairs);

        return timeSeries.InterpolateValueAt(timestamp);
    }

    /// <summary>
    /// Interpolates onto grid.
    /// </summary>
    /// <param name="pairs">The pairs.</param>
    /// <param name="gridPoints">The grid points.</param>
    /// <returns>The interpolated onto grid.</returns>
    public static IEnumerable<KeyValuePair<DateTime, double>> InterpolateOntoGrid(this IEnumerable<KeyValuePair<DateTime, double>> pairs, ICollection<DateTime> gridPoints)
    {
        var timeSeries = new TimeSeries(TimeSeries.AddConflictResolution.Average);
        timeSeries.AddDataPoints(pairs);

        return timeSeries.InterpolateOntoGrid(gridPoints).KeyValuePairs;
    }

    /// <summary>
    /// Interpolates ordered onto ordered grid.
    /// </summary>
    /// <param name="pairsOrdered">The pairs ordered.</param>
    /// <param name="gridPointsOrdered">The grid points ordered.</param>
    /// <returns>The interpolated ordered onto ordered grid.</returns>
    public static IEnumerable<KeyValuePair<DateTime, double>> InterpolateOrderedOntoOrderedGrid(this IEnumerable<KeyValuePair<DateTime, double>> pairsOrdered, IList<DateTime> gridPointsOrdered)
    {
        if (pairsOrdered is not IList<KeyValuePair<DateTime, double>> entries)
            entries = pairsOrdered.ToList();

        int countOfEntries = entries.Count;
        int gridCount = gridPointsOrdered.Count;
        var interpolatedTimeSeries = new List<KeyValuePair<DateTime, double>>(gridCount);

        if (gridCount == 0 || countOfEntries < 2)
            return interpolatedTimeSeries;

        int startIndex = 1;
        for (int gridPosition = 0; gridPosition < gridCount; gridPosition++)
        {
            var gridPoint = gridPointsOrdered[gridPosition];
            for (int i = startIndex; i < countOfEntries; i++)
            {
                var previousTimeSeriesEntry = entries[i - 1];
                var xIm = previousTimeSeriesEntry.Key;
                var yIm = previousTimeSeriesEntry.Value;
                var currentTimeSeriesEntry = entries[i];
                var xI = currentTimeSeriesEntry.Key;
                var yI = currentTimeSeriesEntry.Value;

                if (gridPoint.CompareTo(xIm) > 0 && gridPoint.CompareTo(xI) <= 0)
                {
                    var yInterpolated = Interpolation.LinearlyInterpolate(yI, yIm, xI, xIm, gridPoint);
                    interpolatedTimeSeries.Add(new KeyValuePair<DateTime, double>(gridPoint, yInterpolated));
                    startIndex = i == 1 ? 1 : i - 1;

                    break;
                }
            }
        }

        return interpolatedTimeSeries;
    }

    /// <summary>
    /// Computes the time range averages.
    /// </summary>
    /// <param name="pairsOrdered">The pairs ordered.</param>
    /// <param name="timeSpan">The time span.</param>
    /// <returns>The time range averages.</returns>
    public static IEnumerable<KeyValuePair<DateTime, double>> ComputeTimeRangeAverages(this IEnumerable<KeyValuePair<DateTime, double>> pairsOrdered, AggregationTimeSpan timeSpan)
    {
        var entries = pairsOrdered.ToList();
        if (!entries.Any())
            return entries;

        var key = entries.First().Key;
        var rangeEndTime = entries.Last().Key;
        var dayStartUtcOffset = TimeSpan.FromHours(-7);
        var parts = TimeSplitter.SplitIntoParts(key, rangeEndTime, timeSpan, dayStartUtcOffset);
        var transformed = new List<KeyValuePair<DateTime, double>>();

        foreach (var segment in parts)
        {
            var entriesInSegmentRange = entries.Where(e => e.Key > segment.StartTime && e.Key <= segment.EndTime).ToArray();
            if (entriesInSegmentRange.Any())
            {
                var averageValue = entriesInSegmentRange.Average(e => e.Value);
                var time = segment.StartTime;

                transformed.Add(new KeyValuePair<DateTime, double>(time, averageValue));
            }
        }

        return transformed;
    }

    /// <summary>
    /// Multiplies the pairs by.
    /// </summary>
    /// <param name="x">The x.</param>
    /// <param name="y">The y.</param>
    /// <returns>The multiplied pairs.</returns>
    public static IEnumerable<KeyValuePair<DateTime, double>> MultiplyBy(this IEnumerable<KeyValuePair<DateTime, double>> x, IEnumerable<KeyValuePair<DateTime, double>> y) 
        => PerformMathOperation(x, y, MathOperation.Multiply);

    /// <summary>
    /// Multiplies the pairs by value.
    /// </summary>
    /// <param name="pairs">The pairs.</param>
    /// <param name="multiplier">The multiplier.</param>
    /// <returns>The multiplied pairs by value.</returns>
    public static IEnumerable<KeyValuePair<DateTime, double>> MultiplyByValue(this IEnumerable<KeyValuePair<DateTime, double>> pairs, double multiplier) 
        => from e in pairs select new KeyValuePair<DateTime, double>(e.Key, e.Value * multiplier);

    /// <summary>
    /// Divides the pairs by.
    /// </summary>
    /// <param name="x">The x.</param>
    /// <param name="y">The y.</param>
    /// <returns>The divided pairs.</returns>
    public static IEnumerable<KeyValuePair<DateTime, double>> DivideBy(this IEnumerable<KeyValuePair<DateTime, double>> x, IEnumerable<KeyValuePair<DateTime, double>> y) 
        => PerformMathOperation(x, y, MathOperation.Divide);

    /// <summary>
    /// Adds to.
    /// </summary>
    /// <param name="x">The x.</param>
    /// <param name="y">The y.</param>
    /// <returns>The added pairs.</returns>
    public static IEnumerable<KeyValuePair<DateTime, double>> AddTo(this IEnumerable<KeyValuePair<DateTime, double>> x, IEnumerable<KeyValuePair<DateTime, double>> y) 
        => PerformMathOperation(x, y, MathOperation.Add);

    /// <summary>
    /// Subtracts the pairs.
    /// </summary>
    /// <param name="x">The x.</param>
    /// <param name="y">The y.</param>
    /// <returns>The subtracted pairs.</returns>
    public static IEnumerable<KeyValuePair<DateTime, double>> Subtract(this IEnumerable<KeyValuePair<DateTime, double>> x, IEnumerable<KeyValuePair<DateTime, double>> y) 
        => PerformMathOperation(x, y, MathOperation.Subtract);

    /// <summary>
    /// Computes the forward rolling average.
    /// </summary>
    /// <param name="pairs">The pairs.</param>
    /// <param name="intervalToAverage">The interval to average.</param>
    /// <returns>The forward rolling average.</returns>
    public static IEnumerable<KeyValuePair<DateTime, double>> ComputeForwardRollingAverage(this IEnumerable<KeyValuePair<DateTime, double>> pairs, int intervalToAverage)
    {
        var timeSeries = new TimeSeries(TimeSeries.AddConflictResolution.Average);
        timeSeries.AddDataPoints(pairs);

        return timeSeries.ComputeForwardRollingAverage(intervalToAverage).KeyValuePairs;
    }

    private static IEnumerable<KeyValuePair<DateTime, double>> PerformMathOperation(IEnumerable<KeyValuePair<DateTime, double>> x, IEnumerable<KeyValuePair<DateTime, double>> y, MathOperation operation)
    {
        var xSeries = new TimeSeries(TimeSeries.AddConflictResolution.Average);
        xSeries.AddDataPoints(x);

        var ySeries = new TimeSeries(TimeSeries.AddConflictResolution.Average);
        ySeries.AddDataPoints(y);
        
        return xSeries.PerformMathOn(ySeries, operation).KeyValuePairs;
    }
}
