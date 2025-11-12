namespace Roblox.Platform.Math.Statistics;

/// <summary>
/// Descriptive statistics for a set of values.
/// </summary>
/// <typeparam name="TVal">The type of the values.</typeparam>
public sealed class DescriptiveStatistics<TVal>
{
    /// <summary>
    /// The maximum value.
    /// </summary>
    public TVal Maximum { get; }

    /// <summary>
    /// The minimum value.
    /// </summary>
    public TVal Minimum { get; }

    /// <summary>
    /// The sum of the values.
    /// </summary>
    public TVal Sum { get; }

    /// <summary>
    /// The number of values.
    /// </summary>
    public int Count { get; }

    /// <summary>
    /// The average value.
    /// </summary>
    public double Average { get; }

    /// <summary>
    /// The L2 norm.
    /// </summary>
    public double L2Norm { get; }

    /// <summary>
    /// The standard deviation.
    /// </summary>
    public double StandardDeviation { get; }

    /// <summary>
    /// The mean deviation.
    /// </summary>
    public double MeanDeviation { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DescriptiveStatistics{TVal}"/> class.
    /// </summary>
    /// <param name="max">The maximum value.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="sum">The sum of the values.</param>
    /// <param name="count">The number of values.</param>
    /// <param name="avg">The average value.</param>
    /// <param name="l2Norm">The L2 norm.</param>
    /// <param name="standardDeviation">The standard deviation.</param>
    /// <param name="meanDeviation">The mean deviation.</param>
    public DescriptiveStatistics(TVal max, TVal min, TVal sum, int count, double avg, double l2Norm, double standardDeviation, double meanDeviation)
    {
        Maximum = max;
        Minimum = min;
        Sum = sum;
        Count = count;
        Average = avg;
        L2Norm = l2Norm;
        StandardDeviation = standardDeviation;
        MeanDeviation = meanDeviation;
    }
}
