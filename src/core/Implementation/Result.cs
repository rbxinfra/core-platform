namespace Roblox.Platform.Core;

/// <inheritdoc cref="IResult{T}" />
/// <remark>
/// Having the constructor be public is better than having each assembly have to create their own base Result class
/// </remark>
/// <param name="response">Enum response value. Cannot be null.</param>
public class Result<T>(T response) : IResult<T> where T : struct
{
    /// <inheritdoc cref="IResult{T}.Response" />
    public T Response { get; } = response;
}
