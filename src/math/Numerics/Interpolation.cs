namespace Roblox.Platform.Math.Numerics;

using System;

/// <summary>
/// NOTE: This class will extrapolate if the x value is outside of the range provided for the slope; buyer beware.
/// </summary>
public static class Interpolation
{
    /// <summary>
    /// Interpolates the specified y_i linearly.
    /// </summary>
    /// <param name="y_i">The y value at the current index.</param>
    /// <param name="y_im1">The y value at the previous index.</param>
    /// <param name="x_i">The x value at the current index.</param>
    /// <param name="x_im1">The x value at the previous index.</param>
    /// <param name="x">The x value to interpolate.</param>
    /// <returns>The interpolated value.</returns>
    public static double LinearlyInterpolate(double y_i, double y_im1, DateTime x_i, DateTime x_im1, DateTime x) 
        => y_im1 + (y_i - y_im1) / (x_i - x_im1).TotalDays * (x - x_im1).TotalDays;

    /// <summary>
    /// Interpolates the specified y_i linearly.
    /// </summary>
    /// <param name="y_i">The y value at the current index.</param>
    /// <param name="y_im1">The y value at the previous index.</param>
    /// <param name="x_i">The x value at the current index.</param>
    /// <param name="x_im1">The x value at the previous index.</param>
    /// <param name="x">The x value to interpolate.</param>
    /// <returns>The interpolated value.</returns>
    public static double? LinearlyInterpolate(double? y_i, double? y_im1, DateTime? x_i, DateTime? x_im1, DateTime x)
    {
        if (y_i == null || y_im1 == null || x_i == null || x_im1 == null)
            return default;

        return LinearlyInterpolate(y_i.Value, y_im1.Value, x_i.Value, x_im1.Value, x);
    }

    /// <summary>
    /// Interpolates the specified y_i linearly.
    /// </summary>
    /// <param name="y_i">The y value at the current index.</param>
    /// <param name="y_im1">The y value at the previous index.</param>
    /// <param name="x_i">The x value at the current index.</param>
    /// <param name="x_im1">The x value at the previous index.</param>
    /// <param name="x">The x value to interpolate.</param>
    /// <returns>The interpolated value.</returns>
    public static double LinearlyInterpolate(double y_i, double y_im1, double x_i, double x_im1, double x) 
        => y_im1 + (y_i - y_im1) / (x_i - x_im1) * (x - x_im1);
}
