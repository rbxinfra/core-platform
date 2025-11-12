namespace Roblox.Platform.Math;

using System;

/// <summary>
/// Defines a range of dates.
/// </summary>
public sealed class DateRange : IEquatable<DateRange>
{
    private DateTime _StartDate;
    private DateTime _EndDate;

    /// <summary>
    /// Initializes a new instance of the <see cref="DateRange"/> class.
    /// </summary>
    /// <param name="startDate">The start date.</param>
    /// <param name="endDate">The end date.</param>
    /// <exception cref="InvalidOperationException">Thrown if the start date is greater than the end date.</exception>
    public DateRange(DateTime startDate, DateTime endDate)
    {
        AssertStartDateFollowsEndDate(startDate, endDate);

        _StartDate = startDate;
        _EndDate = endDate;
    }

    /// <summary>
    /// Gets or sets the start date.
    /// </summary>
    public DateTime StartDate
    {
        get => _StartDate;
        set
        {
            AssertStartDateFollowsEndDate(value, _EndDate);

            _StartDate = value;
        }
    }

    /// <summary>
    /// Gets or sets the end date.
    /// </summary>
    public DateTime EndDate
    {
        get => _EndDate;
        set
        {
            AssertStartDateFollowsEndDate(_StartDate, value);

            _EndDate = value;
        }
    }

    /// <summary>
    /// Updates the date range if the provided date is outside the current range.
    /// </summary>
    /// <param name="possibleNewEndpoint">The possible new endpoint.</param>
    /// <returns>True if the date range was updated, false otherwise.</returns>
    public bool Update(DateTime possibleNewEndpoint)
    {
        if (possibleNewEndpoint < _StartDate)
        {
            StartDate = possibleNewEndpoint;

            return true;
        }

        if (possibleNewEndpoint > _EndDate)
        {
            EndDate = possibleNewEndpoint;

            return true;
        }

        return false;
    }

    /// <inheritdoc cref="IEquatable{T}.Equals(T)"/>
    public bool Equals(DateRange other) => other != null && _StartDate == other.StartDate && _EndDate == other.EndDate;

    private void AssertStartDateFollowsEndDate(DateTime startDate, DateTime endDate)
    {
        if (endDate < startDate)
            throw new InvalidOperationException("Start Date must be less than or equal to End Date");
    }
}
