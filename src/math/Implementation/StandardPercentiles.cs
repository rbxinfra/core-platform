namespace Roblox.Platform.Math;

/// <summary>
/// Default implementation of <see cref="IStandardPercentiles"/>.
/// </summary>
internal class StandardPercentiles : IStandardPercentiles
{
    /// <inheritdoc cref="IStandardPercentiles.P01"/>
    public double P01 { get; }

    /// <inheritdoc cref="IStandardPercentiles.P05"/>
    public double P05 { get; }

    /// <inheritdoc cref="IStandardPercentiles.P10"/>
    public double P10 { get; }

    /// <inheritdoc cref="IStandardPercentiles.P25"/>
    public double P25 { get; }

    /// <inheritdoc cref="IStandardPercentiles.P50"/>
    public double P50 { get; }

    /// <inheritdoc cref="IStandardPercentiles.P75"/>
    public double P75 { get; }

    /// <inheritdoc cref="IStandardPercentiles.P95"/>
    public double P95 { get; }

    /// <inheritdoc cref="IStandardPercentiles.P99"/>
    public double P99 { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="StandardPercentiles"/> class.
    /// </summary>
    /// <param name="p01">The 1st percentile.</param>
    /// <param name="p05">The 5th percentile.</param>
    /// <param name="p10">The 10th percentile.</param>
    /// <param name="p25">The 25th percentile.</param>
    /// <param name="p50">The 50th percentile.</param>
    /// <param name="p75">The 75th percentile.</param>
    /// <param name="p95">The 95th percentile.</param>
    /// <param name="p99">The 99th percentile.</param>
    public StandardPercentiles(double p01, double p05, double p10, double p25, double p50, double p75, double p95, double p99)
    {
        P01 = p01;
        P05 = p05;
        P10 = p10;
        P25 = p25;
        P50 = p50;
        P75 = p75;
        P95 = p95;
        P99 = p99;
    }
}
