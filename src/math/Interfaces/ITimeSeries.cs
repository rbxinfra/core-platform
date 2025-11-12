namespace Roblox.Platform.Math;

using System;
using System.Collections.Generic;

/// <summary>
/// Represents a time series.
/// </summary>
internal interface ITimeSeries : IDataSeries<DateTime, double>
{
    /// <summary>
    /// Gets the date range for the data.
    /// </summary>
    DateRange DateRange { get; }

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

    /// <summary>
    /// Get the number of times the series crosses the provided threshold.
    /// </summary>
    /// <param name="threshhold">The threshold to check against.</param>
    /// <returns>The number of times the series crosses the threshold.</returns>
    int NumberOfTimesGreaterThanThreshhold(double threshhold);

    /// <summary>
    /// Get the number of times the series is less than the provided threshold.
    /// </summary>
    /// <param name="threshhold">The threshold to check against.</param>
    /// <returns>The number of times the series is less than the threshold.</returns>
    int NumberOfTimesLessThanThreshhold(double threshhold);

    /// <summary>
    /// Gets the count of data points.
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
    /// Compute the first derivative of the time series.
    /// </summary>
    /// <param name="units">The units to use for the derivative.</param>
    /// <returns>The first derivative of the time series.</returns>
    ITimeSeries ComputeFirstDerivative(TimeUnits units);

    /// <summary>
    /// Compute the second derivative of the time series.
    /// </summary>
    /// <param name="units">The units to use for the derivative.</param>
    /// <returns>The second derivative of the time series.</returns>
    ITimeSeries ComputeSecondDerivative(TimeUnits units);

    /// <summary>
    /// Interpolates the value at the provided timestamp.
    /// </summary>
    /// <param name="timestamp">The timestamp to interpolate at.</param>
    /// <returns>The interpolated value.</returns>
    double? InterpolateValueAt(DateTime timestamp);

    /// <summary>
    /// Interpolates onto the provided grid points.
    /// </summary>
    /// <param name="gridPoints">The grid points to interpolate onto.</param>
    /// <returns>The interpolated time series.</returns>
    ITimeSeries InterpolateOntoGrid(ICollection<DateTime> gridPoints);

    /// <summary>
    /// Multiply the time series by a constant.
    /// </summary>
    /// <param name="constant">The constant to multiply by.</param>
    /// <returns>The multiplied time series.</returns>
    ITimeSeries MultiplyBy(double constant);

    /// <summary>
    /// Divide the time series by another time series.
    /// </summary>
    /// <param name="otherSeries">The other time series to divide by.</param>
    /// <returns>The divided time series.</returns>
    ITimeSeries MultiplyBy(ITimeSeries otherSeries);

    /// <summary>
    /// Divide the time series by another time series.
    /// </summary>
    /// <param name="otherSeries">The other time series to divide by.</param>
    /// <returns>The divided time series.</returns>
    ITimeSeries DivideBy(ITimeSeries otherSeries);

    /// <summary>
    /// Add another time series to this time series.
    /// </summary>
    /// <param name="otherSeries">The other time series to add.</param>
    /// <returns>The added time series.</returns>
    ITimeSeries AddTo(ITimeSeries otherSeries);

    /// <summary>
    /// Subtract another time series from this time series.
    /// </summary>
    /// <param name="otherSeries">The other time series to subtract.</param>
    /// <returns>The subtracted time series.</returns>
    ITimeSeries Subtract(ITimeSeries otherSeries);

    /// <summary>
    /// Compute the rolling average.
    /// </summary>
    /// <param name="intervalToAverage">The interval to average.</param>
    /// <returns>The rolling average.</returns>
    ITimeSeries ComputeForwardRollingAverage(int intervalToAverage);
}
