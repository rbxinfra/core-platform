namespace Roblox.Platform.Math.Statistics;

using System;
using System.Collections.Generic;

/// <summary>
/// Represents a percentile.
/// </summary>
public sealed class Percentile
{
    private readonly List<double> _Values;

    /// <summary>
    /// Initializes a new instance of the <see cref="Percentile"/> class.
    /// </summary>
    /// <param name="values">The values.</param>
    /// <exception cref="ArgumentNullException">Thrown if the values are null.</exception>
    public Percentile(IEnumerable<double> values)
    {
        if (values == null) throw new ArgumentNullException(nameof(values), "Percentile requires non-null values");

        _Values = new List<double>(values);
        _Values.Sort();
    }

    /// <summary>
    /// Gets the percentile.
    /// </summary>
    /// <param name="percentile">The percentile.</param>
    /// <returns>The percentile.</returns>
    public double GetPercentile(int percentile)
    {
        if (percentile < 0 || percentile > 100)
            throw new ArgumentOutOfRangeException(nameof(percentile), "Percentile.GetPercentile argument must be between 0 and 100");

        int count = _Values.Count;
        if (count == 0) return 0;

        int index = 0;
        try
        {
            index = (count - 1) * (percentile / 100);

            if (index < 0 || index >= count)
                throw new ApplicationException($"Attempting to get an endix out of range: {index} while list count was {count}");

            return _Values[index];
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Error getting percentile with index: {index} and count {count} EX: {ex}");
        }
    }

    /// <summary>
    /// Gets the standard percentiles.
    /// </summary>
    /// <returns>The standard percentiles.</returns>
    public IStandardPercentiles GetStandardPercentiles()
    {
        var p1 = GetPercentile(1);
        var p5 = GetPercentile(5);
        var p6 = GetPercentile(10);
        var p7 = GetPercentile(25);
        var p8 = GetPercentile(50);
        var p9 = GetPercentile(75);
        var p10 = GetPercentile(95);
        var p11 = GetPercentile(99);
        
        return new StandardPercentiles(p1, p5, p6, p7, p8, p9, p10, p11);
    }
}
