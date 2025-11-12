namespace Roblox.Platform.Core.ExclusiveStartPaging;

using System;
using System.Linq;

/// <summary>
/// Extensions for <see cref="IPlatformPageResponse{TExclusiveStartKey, TPagedItem}" />.
/// </summary>
/// <seealso cref="IPlatformPageResponse{TExclusiveStartKey, TPagedItem}" />
public static class PlatformPageResponseExtensions
{
    /// <summary>
    /// Converts items in a <see cref="IPlatformPageResponse{TExclusiveStartKey, TPagedItem}" /> from <typeparamref name="TPagedItem" />
    /// to <typeparamref name="TNewPagedItem" /> in a new <see cref="IPlatformPageResponse{TExclusiveStartKey, TPagedItem}" />.
    /// </summary>
    /// <typeparam name="TExclusiveStartKey">The exclusive start key type.</typeparam>
    /// <typeparam name="TPagedItem">The original type of the items.</typeparam>
    /// <typeparam name="TNewPagedItem">The new type of the paged items.</typeparam>
    /// <param name="platformPageResponse">The original <see cref="IPlatformPageResponse{TExclusiveStartKey, TPagedItem}" />.</param>
    /// <param name="convertFunc">The convert function.</param>
    /// <returns><see cref="IPlatformPageResponse{TExclusiveStartKey, TPagedItem}" /></returns>
    public static IPlatformPageResponse<TExclusiveStartKey, TNewPagedItem> Convert<TExclusiveStartKey, TPagedItem, TNewPagedItem>(this IPlatformPageResponse<TExclusiveStartKey, TPagedItem> platformPageResponse, Func<TPagedItem, TNewPagedItem> convertFunc)
    {
        var nextPageExclusiveStartKeyContainer = platformPageResponse.TryGetNextPageExclusiveStartKey(out var nextPageExclusiveStartKey)
            ? new ExclusiveStartKeyContainer<TExclusiveStartKey>(nextPageExclusiveStartKey)
            : null;
        var previousPageContainer = platformPageResponse.TryGetPreviousPageExclusiveStartKey(out var previousPageExclusiveStartKey) 
            ? new ExclusiveStartKeyContainer<TExclusiveStartKey>(previousPageExclusiveStartKey) 
            : null;

        return new PlatformPageResponse<TExclusiveStartKey, TNewPagedItem>(nextPageExclusiveStartKeyContainer, previousPageContainer)
        {
            Count = platformPageResponse.Count,
            Items = platformPageResponse.Items.Select(convertFunc).ToArray(),
            SortOrder = platformPageResponse.SortOrder
        };
    }
}
