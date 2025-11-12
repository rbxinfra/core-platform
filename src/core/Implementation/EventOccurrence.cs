namespace Roblox.Platform.Core;

using System;

/// <summary>
/// Represents an occurrence of an event.
/// </summary>
/// <typeparam name="TEvent">The type of the event.</typeparam>
/// <remarks>
/// Initializes a new instance of the <see cref="EventOccurrence{TEvent}" /> class.
/// </remarks>
/// <param name="e">The <see cref="IEvent" />.</param>
/// <param name="timeOfOccurrence">The time of occurrence.</param>
/// <exception cref="PlatformArgumentNullException"><paramref name="e"/></exception>
public class EventOccurrence<TEvent>(TEvent e, DateTime timeOfOccurrence) : IEventOccurrence<TEvent>
    where TEvent : IEvent
{
    /// <inheritdoc cref="IEventOccurrence{TEvent}.Event"/>
    public TEvent Event { get; private set; } = e ?? throw new PlatformArgumentNullException(nameof(e));

    /// <inheritdoc cref="IEventOccurrence{TEvent}.TimeOfOccurrence"/>
    public DateTime TimeOfOccurrence { get; private set; } = timeOfOccurrence;
}
