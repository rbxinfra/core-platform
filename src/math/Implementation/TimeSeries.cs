namespace Roblox.Platform.Math;

using System;
using System.Linq;
using System.Collections.Generic;

using Numerics;

/// <summary>
/// Default implementation of <see cref="ITimeSeries"/>.
/// </summary>
internal class TimeSeries : DataSeries<DateTime, double>, ITimeSeries
{
    /// <summary>
    /// Represents the conflict resolution strategy.
    /// </summary>
    public enum AddConflictResolution
    {
        /// <summary>
        /// Skip the data point.
        /// </summary>
        Skip,

        /// <summary>
        /// Replace the data point.
        /// </summary>
        Replace,

        /// <summary>
        /// Average the data point.
        /// </summary>
        Average,

        /// <summary>
        /// Use the maximum value.
        /// </summary>
        Maximum,

        /// <summary>
        /// Use the minimum value.
        /// </summary>
        Minimum
    }

    private int _Count;
    private double _Maximum = double.NaN;
    private double _Minimum = double.NaN;
    private double _Sum = double.NaN;
    private double _Average = double.NaN;
    private double _StandardDeviation = double.NaN;
    private bool _StatisticsAreStale;

    private DateRange _DateRange;

    private KeyValuePair<DateTime, double>? _LatestDataPoint;
    private KeyValuePair<DateTime, double>? _MaximumDataPoint;
    private KeyValuePair<DateTime, double>? _MinimumDataPoint;

    private readonly AddConflictResolution _ConflictResolution;
    private readonly Dictionary<DateTime, int> _AverageConflictCount = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="TimeSeries"/> class.
    /// </summary>
    /// <param name="resolution">The conflict resolution strategy.</param>
    public TimeSeries(AddConflictResolution resolution = AddConflictResolution.Average)
    {
        _ConflictResolution = resolution;
    }

    /// <inheritdoc cref="ITimeSeries.DateRange"/>
    public DateRange DateRange => _DateRange;

    /// <inheritdoc cref="ITimeSeries.LatestDataPoint"/>
    public KeyValuePair<DateTime, double>? LatestDataPoint
    {
        get
        {
            UpdateStatistics();

            if (_LatestDataPoint == null) return default;

            var dp = _LatestDataPoint.Value;
            return new KeyValuePair<DateTime, double>(dp.Key, dp.Value);
        }
    }

    /// <inheritdoc cref="ITimeSeries.MaximumDataPoint"/>
    public KeyValuePair<DateTime, double>? MaximumDataPoint
    {
        get
        {
            UpdateStatistics();

            if (_MaximumDataPoint == null) return default;

            var dp = _MaximumDataPoint.Value;
            return new KeyValuePair<DateTime, double>(dp.Key, dp.Value);
        }
    }

    /// <inheritdoc cref="ITimeSeries.MinimumDataPoint"/>
    public KeyValuePair<DateTime, double>? MinimumDataPoint
    {
        get
        {
            UpdateStatistics();

            if (_MinimumDataPoint == null) return default;

            var dp = _MinimumDataPoint.Value;
            return new KeyValuePair<DateTime, double>(dp.Key, dp.Value);
        }
    }

    /// <inheritdoc cref="ITimeSeries.NumberOfTimesGreaterThanThreshhold(double)"/>
    public int NumberOfTimesGreaterThanThreshhold(double threshhold)
    {
        int count = 0;

        foreach (var pair in DataPoints)
            if (pair.Value > threshhold)
                count++;

        return count;
    }

    /// <inheritdoc cref="ITimeSeries.NumberOfTimesLessThanThreshhold(double)"/>
    public int NumberOfTimesLessThanThreshhold(double threshhold)
    {
        int count = 0;

        foreach (KeyValuePair<DateTime, double> pair in DataPoints)
            if (pair.Value < threshhold)
                count++;

        return count;
    }

    /// <inheritdoc cref="ITimeSeries.Count"/>
    public int Count
    {
        get
        {
            UpdateStatistics();

            return _Count;
        }
    }

    /// <inheritdoc cref="ITimeSeries.Maximum"/>
    public double Maximum
    {
        get
        {
            UpdateStatistics();

            return _Maximum;
        }
    }

    /// <inheritdoc cref="ITimeSeries.Minimum"/>
    public double Minimum
    {
        get
        {
            UpdateStatistics();

            return _Minimum;
        }
    }

    /// <inheritdoc cref="ITimeSeries.Average"/>
    public double Average
    {
        get
        {
            UpdateStatistics();

            return _Average;
        }
    }

    /// <inheritdoc cref="ITimeSeries.StandardDeviation"/>
    public double StandardDeviation
    {
        get
        {
            UpdateStatistics();

            return _StandardDeviation;
        }
    }

    /// <inheritdoc cref="ITimeSeries.Sum"/>
    public double Sum
    {
        get
        {
            UpdateStatistics();

            return _Sum;
        }
    }

    public ITimeSeries ComputeFirstDerivative(TimeUnits units)
    {
        try
        {
            var firstDerivative = new TimeSeries(AddConflictResolution.Maximum);

            double? y_i = default;
            DateTime? x_i = default;

            foreach (var point in DataPoints)
            {
                var x_im = x_i;
                var y_im = y_i;

                x_i = point.Key;
                y_i = point.Value;

                if (x_im != null && y_im != null)
                {
                    var firstDeriv = Differentiation.ComputeFirstDerivative(y_i.Value, y_im.Value, x_i.Value, x_im.Value, units);

                    firstDerivative.AddDataPoint(x_i.Value, firstDeriv);
                }
            }

            return firstDerivative;
        }
        catch (Exception ex)
        {
            throw new MathException("Internal Exception", ex);
        }
    }

    /// <inheritdoc cref="ITimeSeries.ComputeSecondDerivative(TimeUnits)"/>
    public ITimeSeries ComputeSecondDerivative(TimeUnits units)
    {
        try
        {
            var secondDerivative = new TimeSeries(AddConflictResolution.Maximum);

            double? y_im = default;
            DateTime? x_im = default;
            double? y_i = default;
            DateTime? x_i = default;

            foreach (var point in DataPoints)
            {
                var x_im2 = x_im;
                var y_im2 = y_im;

                x_im = x_i;
                y_im = y_i;
                x_i = point.Key;
                y_i = point.Value;

                if (x_im != null && y_im != null && x_im2 != null && y_im2 != null)
                {
                    var secondDeriv = Differentiation.ComputeSecondDerivative(y_i.Value, y_im.Value, y_im2.Value, x_i.Value, x_im.Value, x_im2.Value, units);

                    secondDerivative.AddDataPoint(x_i.Value, secondDeriv);
                }
            }

            return secondDerivative;
        }
        catch (Exception ex)
        {
            throw new MathException("Internal Exception", ex);
        }
    }

    /// <summary>
    /// timeseries of absolute increases in percent, or 0 if no increase
    /// </summary>
    public ITimeSeries ComputeRelativeIncreaseInPercent()
    {
        return ComputeRelativeChangeInPercent(v =>
        {
            if (v <= 0) return 0;

            return Math.Abs(v);
        });
    }

    /// <summary>
    /// timeseries of absolute decreases in percent, or 0 if no decrease
    /// </summary>
    public ITimeSeries ComputeRelativeDecreaseInPercent()
    {
        return ComputeRelativeChangeInPercent(v =>
        {
            if (v >= 0) return 0;

            return Math.Abs(v);
        });
    }

    /// <summary>
    /// returns change in percent for each datapoint relative to the previous data point
    /// </summary>
    /// <param name="postEvaluator">final adjustments to the computed value. 0=&gt;0 is considered no change.</param>
    public ITimeSeries ComputeRelativeChangeInPercent(Func<double, double> postEvaluator)
    {
        try
        {
            var relativeChangeInPercent = new TimeSeries(AddConflictResolution.Maximum);
            using var points = DataPoints.GetEnumerator();

            points.MoveNext();
            var previous = points.Current;

            while (points.MoveNext())
            {
                var current = points.Current;
                if (previous.Value == 0)
                {
                    if (current.Value > 0)
                        relativeChangeInPercent.AddDataPoint(current.Key, postEvaluator(double.MaxValue));
                    else if (current.Value < 0)
                        relativeChangeInPercent.AddDataPoint(current.Key, postEvaluator(double.MinValue));
                    else
                        relativeChangeInPercent.AddDataPoint(current.Key, 0);

                    previous = current;
                }
                else
                {
                    var changeInPercent = (current.Value - previous.Value) / Math.Abs(previous.Value) * 100;
                    if (double.IsPositiveInfinity(changeInPercent))
                        changeInPercent = double.MaxValue;
                    else if (double.IsNegativeInfinity(changeInPercent))
                        changeInPercent = double.MinValue;

                    relativeChangeInPercent.AddDataPoint(current.Key, postEvaluator.Invoke(changeInPercent));

                    previous = current;
                }
            }

            return relativeChangeInPercent;
        }
        catch (Exception ex)
        {
            throw new MathException("Internal Exception", ex);
        }
    }

    /// <inheritdoc cref="ITimeSeries.InterpolateValueAt(DateTime)"/>
    public double? InterpolateValueAt(DateTime timestamp)
    {
        try
        {
            if (timestamp < DataPoints.First().Key)
                return default;
            else if (timestamp > DataPoints.Last().Key)
                return default;
            else if (timestamp == DataPoints.First().Key)
                return DataPoints.First().Value;
            else if (timestamp == DataPoints.Last().Key)
                return DataPoints.Last().Value;
            DateTime? x_im = default;
            DateTime? x_i = default;
            double? y_im = default;
            double? y_i = default;

            foreach (var point in DataPoints)
            {
                x_im = x_i;
                y_im = y_i;
                x_i = point.Key;
                y_i = point.Value;

                if (x_i >= timestamp)
                    return Interpolation.LinearlyInterpolate(y_i, y_im, x_i, x_im, timestamp);
            }

            return default;
        }
        catch (Exception ex)
        {
            throw new MathException("Internal Exception", ex);
        }
    }

    /// <inheritdoc cref="ITimeSeries.InterpolateOntoGrid(ICollection{DateTime})"/>
    public ITimeSeries InterpolateOntoGrid(ICollection<DateTime> grid)
    {
        try
        {
            var interpolatedSeries = new TimeSeries(_ConflictResolution);
            if (grid == null || grid.Count == 0 || DataPoints.Count < 2)
                return interpolatedSeries;

            var sortedGrid = new List<DateTime>(grid);
            sortedGrid.Sort();

            var sortedKeys = new List<DateTime>(DataPoints.Keys);
            sortedKeys.Sort();

            int iStart = 1;
            for (int i = 0; i < sortedGrid.Count; i++)
            {
                var gridPoint = sortedGrid[i];
                for (int j = iStart; j < sortedKeys.Count; j++)
                {
                    var x_im = sortedKeys[j - 1];
                    var y_im = DataPoints[x_im];
                    var x_i = sortedKeys[j];
                    var y_i = DataPoints[x_i];

                    if (gridPoint.CompareTo(x_im) > 0 && gridPoint.CompareTo(x_i) <= 0)
                    {
                        double? y_interpolated = Interpolation.LinearlyInterpolate(y_i, y_im, x_i, x_im, gridPoint);

                        if (y_interpolated != null)
                        {
                            interpolatedSeries.AddDataPoint(gridPoint, y_interpolated.Value);
                            iStart = j == 1 ? 1 : j - 1;

                            break;
                        }
                    }
                }
            }

            return interpolatedSeries;
        }
        catch (Exception ex)
        {
            throw new MathException("Internal Exception", ex);
        }
    }

    /// <inheritdoc cref="ITimeSeries.MultiplyBy(double)"/>
    public ITimeSeries MultiplyBy(double constant)
    {
        var returnedSeries = new TimeSeries(AddConflictResolution.Replace);
        foreach (var key in DataPoints.Keys)
            returnedSeries.AddDataPoint(key, constant * DataPoints[key]);

        return returnedSeries;
    }

    /// <inheritdoc cref="ITimeSeries.MultiplyBy(ITimeSeries)"/>
    public ITimeSeries MultiplyBy(ITimeSeries otherSeries) => PerformMathOn(otherSeries, MathOperation.Multiply);

    /// <inheritdoc cref="ITimeSeries.DivideBy(ITimeSeries)"/>
    public ITimeSeries DivideBy(ITimeSeries otherSeries) => PerformMathOn(otherSeries, MathOperation.Divide);

    /// <inheritdoc cref="ITimeSeries.AddTo(ITimeSeries)"/>
    public ITimeSeries AddTo(ITimeSeries otherSeries) => PerformMathOn(otherSeries, MathOperation.Add);

    /// <inheritdoc cref="ITimeSeries.Subtract(ITimeSeries)"/>
    public ITimeSeries Subtract(ITimeSeries otherSeries) => PerformMathOn(otherSeries, MathOperation.Subtract);

    /// <inheritdoc cref="ITimeSeries.ComputeForwardRollingAverage(int)"/>
    public ITimeSeries ComputeForwardRollingAverage(int intervalToAverage)
    {
        var rollingAverage = new TimeSeries(_ConflictResolution);

        if (intervalToAverage > 0)
        {
            var sortedKeys = new List<DateTime>(DataPoints.Keys);
            sortedKeys.Sort();

            int i = 0;
            int count = sortedKeys.Count;
            var inverseDivisor = 1 / intervalToAverage;

            while (i + intervalToAverage - 1 < count)
            {
                double ithRollingAverage = 0;
                for (int j = 0; j < intervalToAverage; j++)
                    ithRollingAverage += DataPoints[sortedKeys[i + j]];

                rollingAverage.AddDataPoint(sortedKeys[i], ithRollingAverage * inverseDivisor);

                i++;
            }
        }

        return rollingAverage;
    }

    /// <inheritdoc cref="DataSeries{TKey, TVal}.PreProcessValueToAdd(TKey, TVal, bool, TVal)"/>
    protected override double PreProcessValueToAdd(DateTime key, double val, bool keyExists, double oldVal)
    {
        if (keyExists)
        {
            switch (_ConflictResolution)
            {
                case AddConflictResolution.Skip:
                    return oldVal;
                case AddConflictResolution.Average:
                    {
                        if (!_AverageConflictCount.ContainsKey(key))
                            _AverageConflictCount.Add(key, 1);

                        int currentCount = _AverageConflictCount[key];
                        _AverageConflictCount[key] = currentCount + 1;

                        return (oldVal * currentCount + val) / (currentCount + 1);
                    }
                case AddConflictResolution.Maximum:
                    return oldVal > val ? oldVal : val;
                case AddConflictResolution.Minimum:
                    return oldVal < val ? oldVal : val;
            }

            return val;
        }

        return val;
    }

    /// <inheritdoc cref="DataSeries{TKey, TVal}.PostProcessAddedDataPoint(TKey, TVal)"/>
    protected override void PostProcessAddedDataPoint(DateTime key, double val)
    {
        _StatisticsAreStale = true;

        if (_DateRange == null)
        {
            _DateRange = new DateRange(key, key);

            return;
        }

        _DateRange.Update(key);
    }

    /// <summary>
    /// Performs a mathematical operation on the series.
    /// </summary>
    /// <param name="otherSeries">The other series.</param>
    /// <param name="operation">The operation.</param>
    /// <returns>The resulting series.</returns>
    internal ITimeSeries PerformMathOn(ITimeSeries otherSeries, MathOperation operation)
    {
        if (otherSeries == null) return null;

        var interpolatedOtherSeries = otherSeries.InterpolateOntoGrid(DataPoints.Keys).DataPoints;
        var returnedSeries = new TimeSeries(AddConflictResolution.Replace);
        foreach (var key in DataPoints.Keys)
        {
            if (interpolatedOtherSeries.ContainsKey(key))
            {
                var thisValue = DataPoints[key];
                var otherValue = interpolatedOtherSeries[key];

                switch (operation)
                {
                    case MathOperation.Add:
                        returnedSeries.AddDataPoint(key, thisValue + otherValue);
                        break;
                    case MathOperation.Subtract:
                        returnedSeries.AddDataPoint(key, thisValue - otherValue);
                        break;
                    case MathOperation.Multiply:
                        returnedSeries.AddDataPoint(key, thisValue * otherValue);
                        break;
                    case MathOperation.Divide:
                        if (0 != otherValue)
                            returnedSeries.AddDataPoint(key, thisValue / otherValue);

                        break;
                }
            }
        }

        return returnedSeries;
    }

    private void UpdateStatistics()
    {
        if (_StatisticsAreStale)
            LockAndOperateOnDataPoints(DoUpdateStatistics);
    }

    private void DoUpdateStatistics(SortedDictionary<DateTime, double> dataPoints)
    {
        if (!_StatisticsAreStale) return;

        int count = 0;
        double max = double.MinValue;
        double min = double.MaxValue;
        double sum = 0.0;
        double avg = 0.0;
        double std = 0.0;

        KeyValuePair<DateTime, double>? latestPoint = default;
        KeyValuePair<DateTime, double>? maximumPoint = default;
        KeyValuePair<DateTime, double>? minimumPoint = default;

        foreach (var pair in dataPoints)
        {
            count++;
            sum += pair.Value;

            if (pair.Value > max)
            {
                maximumPoint = new KeyValuePair<DateTime, double>(pair.Key, pair.Value);
                max = pair.Value;
            }

            if (pair.Value < min)
            {
                minimumPoint = new KeyValuePair<DateTime, double>(pair.Key, pair.Value);
                min = pair.Value;
            }

            if (latestPoint == null || pair.Key > latestPoint.Value.Key)
                latestPoint = new KeyValuePair<DateTime, double>(pair.Key, pair.Value);
        }

        avg = count > 0 ? sum / count : 0;

        foreach (var pair in dataPoints)
            std += Math.Pow(pair.Value - avg, 2.0);

        std = count > 2 ? Math.Sqrt(std / (count - 1)) : 0;
        _Count = count;

        if (_Count == 0)
        {
            _Maximum = double.NaN;
            _Minimum = double.NaN;
            _Sum = double.NaN;
            _Average = double.NaN;
            _StandardDeviation = double.NaN;
        }
        else
        {
            _Maximum = max;
            _Minimum = min;
            _Sum = sum;
            _Average = avg;
            _StandardDeviation = std;
        }
        
        _LatestDataPoint = latestPoint;
        _MaximumDataPoint = maximumPoint;
        _MinimumDataPoint = minimumPoint;
        _StatisticsAreStale = false;
    }
}
