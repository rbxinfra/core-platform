namespace Roblox.Platform.Math;

using System;
using System.Collections.Generic;

/// <summary>
/// Represents a series of data.
/// </summary>
internal interface IDataSeries<TKey, TVal> 
    where TKey : IComparable<TKey>
{
    /// <summary>
    /// Adds a data point to the series.
    /// </summary>
    /// <param name="key">The key of the data point.</param>
    /// <param name="val">The value of the data point.</param>
    void AddDataPoint(TKey key, TVal val);

    /// <summary>
    /// Adds a collection of data points to the series.
    /// </summary>
    /// <param name="dataPoints">The data points to add.</param>
    void AddDataPoints(IDictionary<TKey, TVal> dataPoints);

    /// <summary>
    /// Adds a collection of data points to the series.
    /// </summary>
    /// <param name="dataPoints">The data points to add.</param>
    void AddDataPoints(IEnumerable<KeyValuePair<TKey, TVal>> dataPoints);

    /// <summary>
    /// Gets the data points.
    /// </summary>
    SortedDictionary<TKey, TVal> DataPoints { get; }

    /// <summary>
    /// Gets the key value pairs.
    /// </summary>
    IEnumerable<KeyValuePair<TKey, TVal>> KeyValuePairs { get; }
}
