namespace Roblox.Platform.Core.ExclusiveStartPaging;

/// <summary>
/// Default implementation of <see cref="IExclusiveStartKeyInfo{TExclusiveStartKey}" />
/// </summary>
/// <typeparam name="TExclusiveStartKey">The type of the exclusive start key.</typeparam>
/// <seealso cref="IExclusiveStartKeyInfo{TExclusiveStartKey}" />
public class ExclusiveStartKeyInfo<TExclusiveStartKey> : IExclusiveStartKeyInfo<TExclusiveStartKey>
{
    /// <inheritdoc cref="IExclusiveStartKeyInfo{TExclusiveStartKey}.SortOrder" />
    public SortOrder SortOrder { get; }

    /// <inheritdoc cref="IExclusiveStartKeyInfo{TExclusiveStartKey}.PagingDirection" />
    public PagingDirection PagingDirection { get; }

    /// <inheritdoc cref="IExclusiveStartKeyInfo{TExclusiveStartKey}.Count" />
    public int Count { get; }

    private ExclusiveStartKeyContainer<TExclusiveStartKey> ExclusiveStartKeyContainer { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ExclusiveStartKeyInfo{TExclusiveStartKey}" /> class.
    /// </summary>
    /// <param name="exclusiveStartKey">The exclusive start key.</param>
    /// <param name="count">The count.</param>
    public ExclusiveStartKeyInfo(TExclusiveStartKey exclusiveStartKey, int count)
    {
        ExclusiveStartKeyContainer = new ExclusiveStartKeyContainer<TExclusiveStartKey>(exclusiveStartKey);
        Count = count;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ExclusiveStartKeyInfo{TExclusiveStartKey}" /> class.
    /// </summary>
    /// <param name="sortOrder">The sort order.</param>
    /// <param name="pagingDirection">The paging direction.</param>
    /// <param name="count">The count.</param>
    public ExclusiveStartKeyInfo(SortOrder sortOrder, PagingDirection pagingDirection, int count)
    {
        SortOrder = sortOrder;
        PagingDirection = pagingDirection;
        Count = count;
        ExclusiveStartKeyContainer = null;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ExclusiveStartKeyInfo{TExclusiveStartKey}" /> class with specified exclusive start key.
    /// </summary>
    /// <param name="exclusiveStartKey">The exclusive start key.</param>
    /// <param name="sortOrder">The sort order.</param>
    /// <param name="pagingDirection">The paging direction.</param>
    /// <param name="count">The count.</param>
    public ExclusiveStartKeyInfo(TExclusiveStartKey exclusiveStartKey, SortOrder sortOrder, PagingDirection pagingDirection, int count)
    {
        SortOrder = sortOrder;
        PagingDirection = pagingDirection;
        Count = count;
        ExclusiveStartKeyContainer = new ExclusiveStartKeyContainer<TExclusiveStartKey>(exclusiveStartKey);
    }

    /// <inheritdoc cref="IExclusiveStartKeyInfo{TExclusiveStartKey}.TryGetExclusiveStartKey(out TExclusiveStartKey)" />
    public bool TryGetExclusiveStartKey(out TExclusiveStartKey exclusiveStartKey)
    {
        if (ExclusiveStartKeyContainer != null)
        {
            exclusiveStartKey = ExclusiveStartKeyContainer.ExclusiveStartKey;

            return true;
        }

        exclusiveStartKey = default(TExclusiveStartKey);

        return false;
    }
}
