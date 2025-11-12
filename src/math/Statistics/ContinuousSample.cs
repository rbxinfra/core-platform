namespace Roblox.Platform.Math.Statistics;

using System;
using System.Linq;
using System.Collections.Generic;

/// <summary>
/// Represents a continuous sample of data.
/// </summary>
public sealed class ContinuousSample
{
    private readonly IReadOnlyCollection<double> _Values;

    /// <summary>
    /// Initializes a new instance of the <see cref="ContinuousSample"/> class.
    /// </summary>
    /// <param name="values">The values.</param>
    /// <exception cref="ArgumentNullException">Thrown if the values are null.</exception>
    public ContinuousSample(IEnumerable<double> values)
    {
        if (values == null)
            throw new ArgumentNullException(nameof(values), "ContinuousSample constructor requires a non-null list of values");

        _Values = new List<double>(values);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ContinuousSample"/> class.
    /// </summary>
    /// <param name="values">The values.</param>
    /// <exception cref="ArgumentNullException">Thrown if the values are null.</exception>
    public ContinuousSample(IReadOnlyCollection<double> values)
    {
        _Values = values ?? throw new ArgumentNullException(nameof(values), "ContinuousSample constructor requires a non-null list of values");
    }

    /// <summary>
    /// Calculates the statistics.
    /// </summary>
    /// <returns>The statistics.</returns>
    public DescriptiveStatistics<double> CalculateStatistics() => DoCalculateStatistics(_Values);

    /// <summary>
    /// Calculates the statistics without outliers.
    /// </summary>
    /// <param name="outlierThreshold">The outlier threshold.</param>
    /// <returns>The statistics without outliers.</returns>
    public DescriptiveStatistics<double> CalculateStatisticsWithoutOutliers(Confidence outlierThreshold) => CalculateStatisticsWithoutOutliers(outlierThreshold, out _);

    /// <summary>
    /// Calculates the statistics without outliers.
    /// </summary>
    /// <param name="outlierThreshold">The outlier threshold.</param>
    /// <param name="statisticsWithOutliers">The statistics with outliers.</param>
    /// <returns>The statistics without outliers.</returns>
    public DescriptiveStatistics<double> CalculateStatisticsWithoutOutliers(Confidence outlierThreshold, out DescriptiveStatistics<double> statisticsWithOutliers)
    {
        var sanitizedValues = new List<double>();
        var stats = DoCalculateStatistics(_Values);

        foreach (var val in _Values)
            if (!TTest.SampleMeanIsStatisticallyDifferent(val, stats.Average, stats.Count, stats.StandardDeviation, outlierThreshold))
                sanitizedValues.Add(val);

        statisticsWithOutliers = stats;

        return DoCalculateStatistics(sanitizedValues);
    }

    /// <summary>
    /// Calculates the standard deviation.
    /// </summary>
    /// <param name="values">The values.</param>
    /// <returns>The standard deviation.</returns>
    public static double StandardDeviation(IReadOnlyCollection<double> values)
    {
        int count = values.Count;
        if (count > 2)
        {
            var average = values.Average();
            double varianceAggregator = 0;
            foreach (double val in values)
                varianceAggregator += Math.Pow(val - average, 2);

            return Math.Sqrt(varianceAggregator / (count - 1));
        }

        return 0;
    }

    private DescriptiveStatistics<double> DoCalculateStatistics(IReadOnlyCollection<double> data)
    {
        try
        {
            int count = 0;
            double max = double.MinValue;
            double min = double.MaxValue;
            double average = 0;
            double l2Norm = 0;

            foreach (var val in data)
            {
                count++;
                max = max < val ? val : max;
                min = min > val ? val : min;
                average += val;
                l2Norm += val * val;
            }

            max = count > 0 ? max : 0.0;
            min = count > 0 ? min : 0.0;

            var sum = average;
            average = count > 0 ? average / count : 0;
            l2Norm = Math.Sqrt(l2Norm);

            double standardDeviation = 0;
            double meanDeviation = 0;
            foreach (var val in data)
            {
                standardDeviation += Math.Pow(val - average, 2);
                meanDeviation += Math.Abs(val - average);
            }

            standardDeviation = count > 2 ? Math.Sqrt(standardDeviation / (count - 1)) : 0;
            meanDeviation = count > 0 ? meanDeviation / count : 0;

            return new DescriptiveStatistics<double>(max, min, sum, count, average, l2Norm, standardDeviation, meanDeviation);
        }
        catch (Exception ex)
        {
            throw new MathException("CalculateStatistics Exception: ", ex);
        }
    }
}
