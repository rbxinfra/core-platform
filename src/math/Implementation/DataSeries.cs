namespace Roblox.Platform.Math;

using System;
using System.Collections.Generic;

/// <summary>
/// Default implementation of <see cref="IDataSeries{TKey, TVal}"/>.
/// </summary>
internal abstract class DataSeries<TKey, TVal> : IDataSeries<TKey, TVal> 
    where TKey : IComparable<TKey>
{
    private readonly SortedDictionary<TKey, TVal> _Datapoints = new();

    /// <inheritdoc cref="IDataSeries{TKey, TVal}.AddDataPoint(TKey, TVal)"/>
    public void AddDataPoint(TKey key, TVal val)
    {
        lock (_Datapoints)
            DoAddDataPoint(key, val);
    }

    /// <inheritdoc cref="IDataSeries{TKey, TVal}.AddDataPoints(IDictionary{TKey, TVal})"/>
    public void AddDataPoints(IDictionary<TKey, TVal> dataPoints)
    {
        if (dataPoints == null) return;

        lock (_Datapoints)
            foreach (var key in dataPoints.Keys)
                DoAddDataPoint(key, dataPoints[key]);
    }

    /// <inheritdoc cref="IDataSeries{TKey, TVal}.AddDataPoints(IEnumerable{KeyValuePair{TKey, TVal}})"/>
    public void AddDataPoints(IEnumerable<KeyValuePair<TKey, TVal>> dataPoints)
    {
        if (dataPoints == null) return;

        lock (_Datapoints)
            foreach (KeyValuePair<TKey, TVal> pair in dataPoints)
                DoAddDataPoint(pair.Key, pair.Value);
    }

    /// <inheritdoc cref="IDataSeries{TKey, TVal}.DataPoints"/>
    public SortedDictionary<TKey, TVal> DataPoints
    {
        get
        {
            lock (_Datapoints)
                return new SortedDictionary<TKey, TVal>(_Datapoints);
        }
    }

    /// <inheritdoc cref="IDataSeries{TKey, TVal}.KeyValuePairs"/>
    public IEnumerable<KeyValuePair<TKey, TVal>> KeyValuePairs => DataPoints;

    /// <summary>
    /// Locks and operates on the data points.
    /// </summary>
    /// <param name="operation">The operation to perform.</param>
    protected void LockAndOperateOnDataPoints(Action<SortedDictionary<TKey, TVal>> operation)
    {
        lock (_Datapoints)
            operation(_Datapoints);
    }

    /// <summary>
    /// Pre-processes the value to add.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="val">The value.</param>
    /// <param name="keyExists">Whether the key exists.</param>
    /// <param name="oldVal">The old value.</param>
    /// <returns>The processed value.</returns>
    protected abstract TVal PreProcessValueToAdd(TKey key, TVal val, bool keyExists, TVal oldVal);

    /// <summary>
    /// Post-processes the added data point.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="val">The value.</param>
    protected abstract void PostProcessAddedDataPoint(TKey key, TVal val);

    private void DoAddDataPoint(TKey key, TVal val)
    {
        bool keyExists = false;
        var oldVal = default(TVal);

        if (_Datapoints.ContainsKey(key))
        {
            keyExists = true;
            oldVal = _Datapoints[key];
        }

        val = PreProcessValueToAdd(key, val, keyExists, oldVal);
        _Datapoints[key] = val;

        PostProcessAddedDataPoint(key, val);
    }
}
