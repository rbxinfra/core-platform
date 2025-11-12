namespace Roblox.Platform.Core.ExclusiveStartPaging;

/// <summary>
/// Container for the exclusive start key
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="ExclusiveStartKeyContainer{TExclusiveStartKey}"/> class.
/// </remarks>
/// <param name="exclusiveStartKey">The exclusive start key.</param>
public class ExclusiveStartKeyContainer<TExclusiveStartKey>(TExclusiveStartKey exclusiveStartKey)
{
    /// <summary>
    /// Gets the exclusive start key.
    /// </summary>
    public TExclusiveStartKey ExclusiveStartKey { get; } = exclusiveStartKey;
}
