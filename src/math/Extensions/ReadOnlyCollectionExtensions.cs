namespace Roblox.Platform.Math;

using System;
using System.Linq;
using System.Collections.Generic;

/// <summary>
/// Extension methods for <see cref="IReadOnlyCollection{T}"/>.
/// </summary>
public static class ReadOnlyCollectionExtensions
{
    /// <summary>
    /// Normalizes the specified weight values such that the lowest weight is normalized to 0 and the highest weight is normalized to 1.
    /// </summary>
    /// <remarks>
    /// https://en.wikipedia.org/wiki/Standard_score
    /// </remarks>
    /// <typeparam name="T">The type of values that are weighted.</typeparam>
    /// <param name="values">The values.</param>
    /// <returns>
    /// The normalized weighted values.
    /// </returns>
    /// <exception cref="ArgumentNullException">values</exception>
    public static IEnumerable<Tuple<float, T>> Normalize<T>(this IReadOnlyCollection<Tuple<long, T>> values)
    {
        if (values == null) throw new ArgumentNullException(nameof(values));

        var currentMin = long.MaxValue;
        var currentMax = long.MinValue;

        foreach (var kvp in values)
        {
            if (kvp.Item1 > currentMax)
                currentMax = kvp.Item1;
            if (kvp.Item1 < currentMin)
                currentMin = kvp.Item1;
        }

        var delta = currentMax - currentMin;
        if (delta == 0)
            return values.Select(kvp => new Tuple<float, T>(1, kvp.Item2));

        return values.Select(kvp => new Tuple<float, T>((kvp.Item1 - currentMin) / delta, kvp.Item2));
    }

    /// <summary>
    /// Gets the index of a weighted random element.
    /// </summary>
    /// <param name="weights">The weights.</param>
    /// <param name="seed">The seed.</param>
    /// <returns>
    /// The index of the chosen element from the collection.
    /// </returns>
    /// <exception cref="ArgumentException">Thrown if <paramref name="weights" /> is empty or contains a negative weight.</exception>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="weights" /> is null.</exception>
    public static int GetWeightedRandomElementIndex(this IReadOnlyCollection<float> weights, int seed)
    {
        if (weights == null) throw new ArgumentNullException(nameof(weights));
        if (!weights.Any()) throw new ArgumentException(nameof(weights));

        var total = weights.Sum(weight =>
        {
            if (weight < 0) throw new ArgumentException("negative weight");

            return weight;
        });

        var randomOffset = new Random(seed).NextDouble() * (double)total;
        float currentOffset = 0;
        int index = 0;

        foreach (float weight in weights)
        {
            currentOffset += weight;

            if (randomOffset <= (double)currentOffset)
                return index;

            index++;
        }

        throw new ArithmeticException("Mathematical impossibility");
    }
}
