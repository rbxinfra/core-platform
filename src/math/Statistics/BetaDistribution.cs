namespace Roblox.Platform.Math.Statistics;

using MathNet.Numerics.Distributions;

/// <summary>
/// Class contains methods related to beta distribution. 
/// </summary>
public static class BetaDistribution
{
    /// <summary>
    /// Returns a beta distribution sample.
    /// </summary>
    /// <param name="alpha"> The α shape parameter of the Beta distribution. Range: α ≥ 0.</param>
    /// <param name="beta">The β shape parameter of the Beta distribution. Range: β ≥ 0.</param>
    /// <returns>A sample of type double.</returns>
    public static double GetBetaDistributionSample(double alpha, double beta)
    {
        if (alpha < 0 || beta < 0)
            throw new MathException("Input parameters can not be negetaive.");

        return Beta.Sample(alpha, beta);
    }
}
