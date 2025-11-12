namespace Roblox.Platform.Math;

/// <summary>
/// The standard percentiles.
/// </summary>
public interface IStandardPercentiles
{
    /// <summary>
    /// The 1st percentile.
    /// </summary>
    double P01 { get; }

    /// <summary>
    /// The 5th percentile.
    /// </summary>
    double P05 { get; }

    /// <summary>
    /// The 10th percentile.
    /// </summary>
    double P10 { get; }

    /// <summary>
    /// The 25th percentile.
    /// </summary>
    double P25 { get; }

    /// <summary>
    /// The 50th percentile.
    /// </summary>
    double P50 { get; }

    /// <summary>
    /// The 75th percentile.
    /// </summary>
    double P75 { get; }

    /// <summary>
    /// The 95th percentile.
    /// </summary>
    double P95 { get; }

    /// <summary>
    /// The 99th percentile.
    /// </summary>
    double P99 { get; }
}
