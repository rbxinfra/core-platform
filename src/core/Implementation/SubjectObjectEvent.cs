namespace Roblox.Platform.Core;

/// <summary>
/// Represents an <see cref="IEvent" /> that has a subject and an object.
/// </summary>
/// <typeparam name="TSubject">The type of the subject.</typeparam>
/// <typeparam name="TObject">The type of the object.</typeparam>
/// <remarks>
/// Initializes a new instance of the <see cref="SubjectObjectEvent{TSubject, TObject}" /> class.
/// </remarks>
/// <param name="subject">The subject.</param>
/// <param name="o">The object.</param>
/// <exception cref="PlatformArgumentNullException">
/// <paramref name="subject"/>
/// or
/// <paramref name="o"/>
/// </exception>
public class SubjectObjectEvent<TSubject, TObject>(TSubject subject, TObject o) : ISubjectObjectEvent<TSubject, TObject>
{
    /// <inheritdoc cref="ISubjectObjectEvent{TSubject, TObject}.Subject"/>
    public TSubject Subject { get; private set; } = subject ?? throw new PlatformArgumentNullException(nameof(subject));

    /// <inheritdoc cref="ISubjectObjectEvent{TSubject, TObject}.Object"/>
    public TObject Object { get; private set; } = o ?? throw new PlatformArgumentNullException(nameof(o));
}
