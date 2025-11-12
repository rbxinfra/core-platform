namespace Roblox.Platform.Math.Numerics;

using System;

/// <summary>
/// Represents a differentiation utility.
/// </summary>
public static class Differentiation
{
    /// <summary>
    /// Computes the first derivative.
    /// </summary>
    /// <param name="y_i">The y value at the current index.</param>
    /// <param name="y_im1">The y value at the previous index.</param>
    /// <param name="x_i">The x value at the current index.</param>
    /// <param name="x_im1">The x value at the previous index.</param>
    /// <param name="units">The time units.</param>
    /// <returns>The first derivative.</returns>
    public static double ComputeFirstDerivative(double y_i, double y_im1, DateTime x_i, DateTime x_im1, TimeUnits units)
    {
        var timeSpan = ComputeTimeSpan(x_i, x_im1, units);
        if (timeSpan != 0) return (y_i - y_im1) / timeSpan;

        return 0;
    }

    /// <summary>
    /// Computes the second derivative.
    /// </summary>
    /// <param name="y_i">The y value at the current index.</param>
    /// <param name="y_im1">The y value at the previous index.</param>
    /// <param name="y_im2">The y value at the second previous index.</param>
    /// <param name="x_i">The x value at the current index.</param>
    /// <param name="x_im1">The x value at the previous index.</param>
    /// <param name="x_im2">The x value at the second previous index.</param>
    /// <param name="units">The time units.</param>
    /// <returns>The second derivative.</returns>
    public static double ComputeSecondDerivative(double y_i, double y_im1, double y_im2, DateTime x_i, DateTime x_im1, DateTime x_im2, TimeUnits units)
    {
        var timeSpan = ComputeTimeSpan(x_i, x_im2, units);

        return (y_i - 2.0 * y_im1 + y_im2) / Math.Pow(0.5 * timeSpan, 2);
    }

    /// <summary>
    /// Differentiates a series.
    /// </summary>
    /// <param name="input">The input series.</param>
    /// <returns>The differentiated series.</returns>
    public static double[] DifferentiateSeries(double[] input)
    {
        var diff = new double[input.Length - 1];
        for (int i = 0; i < diff.Length; i++)
            diff[i] = input[i + 1] - input[i];

        return diff;
    }

    private static double ComputeTimeSpan(DateTime x_i, DateTime x_im1, TimeUnits units)
    {
        var timeSpan = x_i - x_im1;

        return units switch
        {
            TimeUnits.Seconds => timeSpan.TotalSeconds,
            TimeUnits.Minutes => timeSpan.TotalMinutes,
            TimeUnits.Hours => timeSpan.TotalHours,
            _ => timeSpan.TotalMilliseconds,
        };
    }
}
