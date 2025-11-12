namespace Roblox.Platform.Math.Statistics;

using System;

/// <summary>
/// Provides methods for performing a t-test.
/// </summary>
public static class TTest
{
    /// <summary>
    /// Returns true if the sample mean (or guessed mean) is statistically different from the population mean, given the
    /// population size, the population standard deviation, and the statistical confidence.
    /// </summary>
    /// <param name="sampleMean">Mean of the sample being evaluated</param>
    /// <param name="populationMean">Mean of the population under consideration</param>
    /// <param name="populationSize">Size of the population under consideration</param>
    /// <param name="populationStdev">Standard deviation of the population under consideration</param>
    /// <param name="conf">Confidence of the t-test to determine statistical significace</param>
    /// <returns>True if the sample mean is statistically different from the population mean, false otherwise</returns>
    public static bool SampleMeanIsStatisticallyDifferent(double sampleMean, double populationMean, int populationSize, double populationStdev, Confidence conf)
    {
        try
        {
            var tCritical = TDistribution.TCritical(populationSize - 1, conf);

            return Math.Abs((sampleMean - populationMean) / populationStdev) > tCritical;
        }
        catch (Exception ex)
        {
            throw new MathException("TTest.ValueIsAnOutlier Exception", ex);
        }
    }
}
