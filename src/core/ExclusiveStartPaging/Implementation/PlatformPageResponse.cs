namespace Roblox.Platform.Core.ExclusiveStartPaging;

using System;
using System.Linq;


/// <summary>
/// A model to hold information about paged results that can be returned by platform.
/// </summary>
/// <typeparam name="TExclusiveStartKey">The type of the exclusive start key.</typeparam>
/// <typeparam name="TPagedItem">The type of the paged items.</typeparam>
/// <seealso cref="IPlatformPageResponse{TExclusiveStartKey, TPagedItem}" />
public class PlatformPageResponse<TExclusiveStartKey, TPagedItem> : IPlatformPageResponse<TExclusiveStartKey, TPagedItem>
{
    /// <inheritdoc cref="IPlatformPageResponse{TExclusiveStartKey, TPagedItem}.Count" />
    public int Count { get; set; }

    /// <inheritdoc cref="IPlatformPageResponse{TExclusiveStartKey, TPagedItem}.Items" />
    public TPagedItem[] Items { get; set; }

    /// <inheritdoc cref="IPlatformPageResponse{TExclusiveStartKey, TPagedItem}.SortOrder" />
    public SortOrder SortOrder { get; set; }

    internal ExclusiveStartKeyContainer<TExclusiveStartKey> PreviousPageExclusiveStartKeyContainer { get; }
    internal ExclusiveStartKeyContainer<TExclusiveStartKey> NextPageExclusiveStartKeyContainer { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="PlatformPageResponse{TExclusiveStartKey, TPagedItem}" /> model.
    /// </summary>
    public PlatformPageResponse(TPagedItem[] originalItems, IExclusiveStartKeyInfo<TExclusiveStartKey> exclusiveStartKeyInfo, Func<TPagedItem, TExclusiveStartKey> getExclusiveStartKey, bool? hasMoreItems = null)
    {
        if (originalItems == null) throw new PlatformArgumentNullException(nameof(originalItems));
        if (exclusiveStartKeyInfo == null) throw new PlatformArgumentNullException(nameof(exclusiveStartKeyInfo));
        if (getExclusiveStartKey == null) throw new PlatformArgumentNullException(nameof(getExclusiveStartKey));

        Count = exclusiveStartKeyInfo.Count;
        Items = originalItems.Take(Count).ToArray();
        SortOrder = exclusiveStartKeyInfo.SortOrder;
        if (!Items.Any()) return;

        ExclusiveStartKeyContainer<TExclusiveStartKey> previousPageExclusiveStartKeyContainer = null;
        ExclusiveStartKeyContainer<TExclusiveStartKey> nextPageExclusiveStartKeyContainer = null;
        if (exclusiveStartKeyInfo.TryGetExclusiveStartKey(out var exclusiveStartKey) && exclusiveStartKey != null && !exclusiveStartKey.Equals(default(TExclusiveStartKey)))
            previousPageExclusiveStartKeyContainer = new ExclusiveStartKeyContainer<TExclusiveStartKey>(getExclusiveStartKey(Items.First()));
        if (originalItems.Length > Items.Length || hasMoreItems == true)
            nextPageExclusiveStartKeyContainer = new ExclusiveStartKeyContainer<TExclusiveStartKey>(getExclusiveStartKey(Items.Last()));

        if (exclusiveStartKeyInfo.PagingDirection.Equals(PagingDirection.Backward))
        {
            PreviousPageExclusiveStartKeyContainer = nextPageExclusiveStartKeyContainer;
            NextPageExclusiveStartKeyContainer = previousPageExclusiveStartKeyContainer;
            Items = Items.Reverse().ToArray();

            return;
        }

        PreviousPageExclusiveStartKeyContainer = previousPageExclusiveStartKeyContainer;
        NextPageExclusiveStartKeyContainer = nextPageExclusiveStartKeyContainer;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PlatformPageResponse{TExclusiveStartKey, TPagedItem}" /> model.
    /// Use it only if you know what you are doing. Created for inclusive start/prev ES paging workaround.
    /// </summary>
    public PlatformPageResponse(
        TPagedItem[] originalItems, 
        IExclusiveStartKeyInfo<TExclusiveStartKey> exclusiveStartKeyInfo, 
        ExclusiveStartKeyContainer<TExclusiveStartKey> previousKeyContainer, 
        ExclusiveStartKeyContainer<TExclusiveStartKey> nextKeyContainer
    )
    {
        if (exclusiveStartKeyInfo == null) throw new PlatformArgumentNullException(nameof(exclusiveStartKeyInfo));
        Count = exclusiveStartKeyInfo.Count;

        originalItems = originalItems?.Take(Count).ToArray();
        Items = originalItems ?? throw new PlatformArgumentNullException(nameof(originalItems));

        SortOrder = exclusiveStartKeyInfo.SortOrder;
        PreviousPageExclusiveStartKeyContainer = previousKeyContainer;
        NextPageExclusiveStartKeyContainer = nextKeyContainer;
    }

    /// <summary>
    /// Initializes a new PlatformPageResponse with specific start keys.
    /// This constructor is not meant to be public, and should only be used for converting internally.
    /// </summary>
    /// <param name="nextPageExclusiveStartKeyContainer">The <see cref="ExclusiveStartKeyContainer{TExclusiveStartKey}" /> for the next page start key.</param>
    /// <param name="previousPageExclusiveStartKeyContainer">The <see cref="ExclusiveStartKeyContainer{TExclusiveStartKey}" /> for the previous page start key.</param>
    internal PlatformPageResponse(ExclusiveStartKeyContainer<TExclusiveStartKey> nextPageExclusiveStartKeyContainer, ExclusiveStartKeyContainer<TExclusiveStartKey> previousPageExclusiveStartKeyContainer)
    {
        NextPageExclusiveStartKeyContainer = nextPageExclusiveStartKeyContainer;
        PreviousPageExclusiveStartKeyContainer = previousPageExclusiveStartKeyContainer;
    }

    /// <inheritdoc cref="IPlatformPageResponse{TExclusiveStartKey, TPagedItem}.TryGetPreviousPageExclusiveStartKey(out TExclusiveStartKey)" />
    public bool TryGetPreviousPageExclusiveStartKey(out TExclusiveStartKey previousPageExclusiveStartKey)
    {
        if (PreviousPageExclusiveStartKeyContainer != null)
        {
            previousPageExclusiveStartKey = PreviousPageExclusiveStartKeyContainer.ExclusiveStartKey;

            return true;
        }

        previousPageExclusiveStartKey = default(TExclusiveStartKey);

        return false;
    }

    /// <inheritdoc cref="IPlatformPageResponse{TExclusiveStartKey, TPagedItem}.TryGetNextPageExclusiveStartKey(out TExclusiveStartKey)" />
    public bool TryGetNextPageExclusiveStartKey(out TExclusiveStartKey nextPageExclusiveStartKey)
    {
        if (NextPageExclusiveStartKeyContainer != null)
        {
            nextPageExclusiveStartKey = NextPageExclusiveStartKeyContainer.ExclusiveStartKey;

            return true;
        }

        nextPageExclusiveStartKey = default(TExclusiveStartKey);

        return false;
    }
}
