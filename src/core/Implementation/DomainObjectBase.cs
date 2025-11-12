namespace Roblox.Platform.Core;

using System;

using Newtonsoft.Json;

/// <summary>
/// Represents an object that is part of a domain.
/// </summary>
/// <typeparam name="TDomainFactories">The type of the container containing all factories in the domain.</typeparam>
/// <seealso cref="IDomainObject{TDomainFactories}" />
/// <remarks>
/// Initializes a new instance of the <see cref="DomainObjectBase{TDomainFactories}" /> class.
/// </remarks>
/// <param name="domainFactories">The domain factories.</param>
/// <exception cref="PlatformArgumentNullException"><paramref name="domainFactories" /></exception>
public abstract class DomainObjectBase<TDomainFactories>(TDomainFactories domainFactories) : IDomainObject<TDomainFactories>
    where TDomainFactories : DomainFactoriesBase
{
    /// <inheritdoc cref="IDomainObject{TDomainFactories}.DomainFactories" />
    [JsonIgnore]
    public TDomainFactories DomainFactories { get; } = domainFactories ?? throw new PlatformArgumentNullException(nameof(domainFactories));
}

/// <summary>
/// Represents an identifiable object that is part of a domain.
/// </summary>
/// <typeparam name="TDomainFactories">The type of the container containing all factories in the domain.</typeparam>
/// <typeparam name="TId">The type of the identifier that uniquely identifies the object.</typeparam>
/// <seealso cref="IDomainObject{TDomainFactories, TId}" />
/// <remarks>
/// Initializes a new instance of the <see cref="DomainObjectBase{TDomainFactories, TId}" /> class.
/// </remarks>
/// <param name="domainFactories">The domain factories.</param>
/// <exception cref="PlatformArgumentNullException"><paramref name="domainFactories" /></exception>
public abstract class DomainObjectBase<TDomainFactories, TId>(TDomainFactories domainFactories) : DomainObjectBase<TDomainFactories>(domainFactories), IDomainObject<TDomainFactories, TId>
    where TDomainFactories : DomainFactoriesBase
    where TId : struct, IEquatable<TId>
{
    /// <inheritdoc cref="IDomainObjectIdentifier{TId}.Id" />
    public TId Id { get; protected set; }

    /// <summary>
    /// Determines whether the specified <see cref="IDomainObject{TDomainFactories, TId}" /> is equal to the current <see cref="DomainObjectBase{TDomainFactories, TId}" />.
    /// </summary>
    /// <param name="other">The <see cref="IDomainObject{TDomainFactories, TId}" /> to compare with the current <see cref="DomainObjectBase{TDomainFactories, TId}" />.</param>
    /// <returns><c>true</c> if the specified <see cref="IDomainObject{TDomainFactories, TId}" /> is equal to the current <see cref="DomainObjectBase{TDomainFactories, TId}" />; otherwise, <c>false</c>.</returns>
    public virtual bool Equals(IDomainObject<TDomainFactories, TId> other)
    {
        if (other != null)
            return Id.Equals(other.Id);

        return false;
    }
}
