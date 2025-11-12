namespace Roblox.Platform.Math.Functions;

using System;

/// <summary>
/// Represents a logistic function utility.
/// </summary>
public static class LogisticFunctions
{
    /// <summary>
    /// Computes a logistic curve.
    /// </summary>
    /// <remarks>
    /// https://en.wikipedia.org/wiki/Logistic_function
    /// </remarks>
    /// <param name="m">Slope of the line at inflection point.</param>
    /// <param name="k">Vertical size of curve, positive numbers for up, negative for down.</param>
    /// <param name="b">y-intercept (Moves the line vertically).</param>
    /// <param name="c">x-value of inflection point (Moves the line horizontally).</param>
    /// <param name="x">The input value.</param>
    /// <returns>f(x)</returns>
    public static float Compute(float m, float k, float b, float c, float x) 
        => b + k * (1 / (1 + (float)Math.Pow(2718.2818284590453 * (double)m, (double)(c - x))));
}
