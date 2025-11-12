namespace Roblox.Platform.Math;

using System;
using System.Collections.Generic;

/// <summary>
/// Sorted list of evenly spaced DateTime points. The list is sorted by
/// construct, and is always returned in a sorted state.
/// </summary>
public sealed class DateTimeGrid
{
    private readonly List<DateTime> _Grid = new();

    /// <summary>
    /// The starting point of the grid
    /// </summary>
    public DateTime StartDate { get; }

    /// <summary>
    /// The interval between grid points
    /// </summary>
    public TimeSpan Interval { get; }

    /// <summary>
    /// The number of grid points, inclusive. The number of intervals is numberOfGridPoints-1
    /// </summary>
    public int Count { get; }

    /// <summary>
    /// Construct a uniform DateTime grid
    /// </summary>
    /// <param name="startDate">The grid starting point</param>
    /// <param name="interval">The timespan between grid points</param>
    /// <param name="numberOfGridPoints">The number of grid points, inclusive. The number of intervals is numberOfGridPoints-1</param>
    public DateTimeGrid(DateTime startDate, TimeSpan interval, int numberOfGridPoints)
    {
        StartDate = startDate;
        Interval = interval;
        Count = numberOfGridPoints;
        
        if (Count > 0)
        {
            var lastPoint = startDate;
            _Grid.Add(StartDate);

            for (int i = 1; i < numberOfGridPoints; i++)
            {
                lastPoint = lastPoint.Add(interval);

                _Grid.Add(lastPoint);
            }
        }
    }

    /// <summary>
    /// Returns an ICollection instead of an IEnumerable because the expected usage
    /// is mathematical: we will want to know count and be able to go to count-1.
    /// </summary>
    public ICollection<DateTime> GridPoints => _Grid;

    /// <summary>
    /// Build a grid of evenly spaced DateTime points
    /// </summary>
    /// <param name="startDate">The starting point of the grid</param>
    /// <param name="endDate">The ending point of the grid</param>
    /// <param name="numberOfGridPoints">The number of grid points, inclusive. The number of intervals is numberOfGridPoints-1</param>
    /// <returns>A list of DateTime points</returns>
    public static IList<DateTime> BuildGridPoints(DateTime startDate, DateTime endDate, int numberOfGridPoints)
    {
        var ticks = (endDate - startDate).Ticks / numberOfGridPoints;
        var points = new List<DateTime>(numberOfGridPoints);

        var lastPoint = startDate;
        points.Add(startDate);
        for (int i = 1; i < numberOfGridPoints; i++)
        {
            lastPoint = lastPoint.AddTicks(ticks);

            points.Add(lastPoint);
        }
        
        return points;
    }
}
