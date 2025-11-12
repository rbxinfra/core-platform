namespace Roblox.Platform.Math.Statistics;

using System;

/// <summary>
/// Represents a Wilson score utility.
/// </summary>
public static class WilsonScore
{
    /// <summary>
    /// Calculates the lower bound of Wilson score confidence interval for a Bernoulli parameter
    /// http://www.evanmiller.org/how-not-to-sort-by-average-rating.html
    /// </summary>
    /// <param name="positiveValues">The count of the positive values.</param>
    /// <param name="totalValue">The total count of values.</param>
    /// <param name="confidence">The value of the confidence. This value must be between 0 and 1.</param>
    /// <returns>The value of the wilson score.</returns>
    /// <exception cref="ArgumentException">Throws if <paramref name="confidence" /> is not between 0 and 1.</exception>
    public static double Calculate(double positiveValues, double totalValue, double confidence)
    {
        var negative = totalValue - positiveValues;
        var zScore = ZScore.Calculate(confidence);
        var zSquared = zScore * zScore;
        double voteScore = 0;

        if (totalValue > 0 && positiveValues >= 0 && negative >= 0)
        {
            var num = (positiveValues + zSquared / 2) / totalValue;
            var secondTerm = zScore * Math.Sqrt(positiveValues * negative / totalValue + zSquared / 4) / totalValue;
            var denominator = 1 + zSquared / totalValue;

            voteScore = (num - secondTerm) / denominator;
        }
        
        return voteScore;
    }
}
