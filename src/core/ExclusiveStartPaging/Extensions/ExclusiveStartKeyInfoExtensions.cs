namespace Roblox.Platform.Core.ExclusiveStartPaging;

using System;

/// <summary>
/// Extensions for <see cref="IExclusiveStartKeyInfo{TExclusiveStartKey}" />
/// </summary>
/// <seealso cref="IExclusiveStartKeyInfo{TExclusiveStartKey}" />
public static class ExclusiveStartKeyInfoExtensions
{
    /// <summary>
    /// Gets sort order to be used when requesting from the database.
    /// If the direction is backwards we want to return the reversed sort order.
    /// </summary>
    /// <returns></returns>
    public static SortOrder GetDatabaseRequestSortOrder<TId>(this IExclusiveStartKeyInfo<TId> exclusiveStartKeyInfo)
    {
        if (exclusiveStartKeyInfo.PagingDirection != PagingDirection.Backward)
            return exclusiveStartKeyInfo.SortOrder;

        if (exclusiveStartKeyInfo.SortOrder != SortOrder.Asc)
            return SortOrder.Asc;

        return SortOrder.Desc;
    }

    /// <summary>
    /// Gets the exclusive start key from an <see cref="IExclusiveStartKeyInfo{TExclusiveStartKey}" />
    /// If TryGet fails, returns null.
    /// </summary>
    /// <typeparam name="TId">The exclusive start key type.</typeparam>
    /// <param name="exclusiveStartKeyInfo">The <see cref="IExclusiveStartKeyInfo{TExclusiveStartKey}" />.</param>
    /// <returns><see cref="Nullable" /> <typeparamref name="TId" /></returns>
    public static TId? GetNullableExclusiveStartKey<TId>(this IExclusiveStartKeyInfo<TId> exclusiveStartKeyInfo)
        where TId : struct, IComparable, IFormattable, IConvertible
    {
        if (!exclusiveStartKeyInfo.TryGetExclusiveStartKey(out var exclusiveStartKey))
            return null;

        return exclusiveStartKey;
    }

    /// <summary>
    /// Gets a non-nullable exclusive start id for basic data types.
    /// The default exclusive start id based on the <see cref="SortOrder" /> of <paramref name="exclusiveStartKeyInfo" />.
    /// If ascending will always be 0. If descending will be the max value of the datatype.
    /// </summary>
    /// <typeparam name="TId">The exclusive start key type.</typeparam>
    /// <param name="exclusiveStartKeyInfo">The <see cref="IExclusiveStartKeyInfo{TExclusiveStartKey}" />.</param>
    /// <returns>Non-nullable exclusive start key.</returns>
    public static TId GetDefaultExclusiveStartId<TId>(this IExclusiveStartKeyInfo<TId> exclusiveStartKeyInfo)
        where TId : struct, IComparable, IFormattable, IConvertible
    {
        if (exclusiveStartKeyInfo.TryGetExclusiveStartKey(out var exclusiveStartKey))
            return exclusiveStartKey;

        if (exclusiveStartKeyInfo.GetDatabaseRequestSortOrder() == SortOrder.Asc)
            return default(TId);

        return (TId)typeof(TId).GetField("MaxValue").GetValue(null);
    }
}
