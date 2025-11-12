namespace Roblox.Platform.Math.Statistics;

using System;

/// <summary>
/// This implementation was transcribed from:
/// http://www.fourmilab.ch/rpkp/experiments/analysis/zCalc.html
/// </summary>
public static class ZScore
{
    private const double _ZMax = 6;

    /// <summary>
    /// Calculates the z score
    /// </summary>
    /// <param name="confidence">Confidence percentile, must be between 0 and 1.</param>
    /// <returns>The normal z score</returns>
    /// <exception cref="ArgumentException">Throws if <paramref name="confidence" /> is not between 0 and 1.</exception>
    public static double Calculate(double confidence)
    {
        if (confidence < 0 || confidence > 1)
            throw new ArgumentException(string.Format("Probability {0} must be between 0 and 1.", confidence));

        return Math.Abs(CalculateCriticalNormalZ(CalculateRightSideQuantile(confidence)));
    }

    /// <summary>
    /// Converts a confidence to the right side quantile when calculating the Z Score.
    /// e.g .95 -&gt; .975
    /// </summary>
    /// <param name="confidence">The confidence </param>
    /// <returns>The right side quantile confidence.</returns>
    private static double CalculateRightSideQuantile(double confidence) => (1 + confidence) / 2;

    /// <summary>
    /// Compute critical normal z value to produce given p.
    /// Bisection search for a value within CHI_EPSILON, relying on the monotonicity of pochisq.
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    private static double CalculateCriticalNormalZ(double p)
    {
        const double Z_EPSILON = 0.000001;

        var minz = -_ZMax;
        var maxz = _ZMax;
        double zval = 0;
        double pval;

        if (p < 0 || p > 1) return -1;

        while (maxz - minz > Z_EPSILON)
        {
            pval = ProbabilityOfNormalZ(zval);

            if (pval > p)
                maxz = zval;
            else
                minz = zval;

            zval = (maxz + minz) * 0.5;
        }

        return zval;
    }

    /// <summary>
    /// Calculates the probability of normal z value.
    /// Adapted from a polynomial approximation in: Ibbetson D, Algorithm 209 Collected Algorithms of the CACM 1963 p. 616
    /// This routine has six digit accuracy, so it is only useful for absolute z values &lt;= 6.
    /// </summary>
    /// <param name="z"></param>
    /// <returns>The probability of normal Z. For z values &gt; to 6.0, returns 0.0.</returns>
    private static double ProbabilityOfNormalZ(double z)
    {
        double y, x, w;

        if (z == 0) x = 0;
        else
        {
            y = 0.5 * Math.Abs(z);
            if (y > (_ZMax * 0.5)) x = 1;
            else if (y < 1)
            {
                w = y * y;

                x = ((((((((0.000124818987 * w 
                        - 0.001075204047) * w + 0.005198775019) * w 
                        - 0.019198292004) * w + 0.059054035642) * w 
                        - 0.151968751364) * w + 0.319152932694) * w 
                        - 0.5319230073) * w + 0.797884560593) * y * 2.0;
            }
            else
            {
                y -= 2.0;

                x = (((((((((((((-0.000045255659 * y 
                                + 0.00015252929) * y - 1.9538132E-05) * y 
                                - 0.000676904986) * y + 0.001390604284) * y 
                                - 0.00079462082) * y - 0.002034254874) * y 
                                + 0.006549791214) * y - 0.010557625006) * y
                                + 0.011630447319) * y - 0.009279453341) * y 
                                + 0.005353579108) * y - 0.002141268741) * y 
                                + 0.000535310849) * y + 0.999936657524;
            }
        }

        return z > 0.0 ? ((x + 1) * 0.5) : ((1 - x) * 0.5);
    }
}
