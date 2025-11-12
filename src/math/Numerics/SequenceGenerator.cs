namespace Roblox.Platform.Math.Numerics;

using System.Collections.Generic;

/// <summary>
/// Utility class for generating sequences.
/// </summary>
public static class SequenceGenerator
{
    /// <summary>
    /// Generates a sequence of integers.
    /// </summary>
    /// <param name="start">The start value.</param>
    /// <param name="end">The end value.</param>
    /// <param name="step">The step value.</param>
    /// <returns>The sequence of integers.</returns>
    public static int[] GenerateSequence(int start, int end, int step = 1)
    {
        var sequence = new List<int>();
        for (int value = start; value <= end; value += step)
            sequence.Add(value);

        return sequence.ToArray();
    }

    /// <summary>
    /// Generates a sequence of doubles.
    /// </summary>
    /// <param name="start">The start value.</param>
    /// <param name="end">The end value.</param>
    /// <param name="step">The step value.</param>
    /// <returns>The sequence of doubles.</returns>
    public static double[] GenerateSequence(double start, double end, double step = 1)
    {
        var sequence = new List<double>();
        for (double value = start; value <= end; value += step)
            sequence.Add(value);

        return sequence.ToArray();
    }
}
