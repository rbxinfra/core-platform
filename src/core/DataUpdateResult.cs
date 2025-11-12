namespace Roblox.Platform.Core;

/// <summary>
/// This is effectively a tuple to encapsulate data change results (creation/modification) at the entity level in an auditing pattern
/// </summary>
/// <typeparam name="TDataEntity">The type of the Entity object representing the data record in question</typeparam>
/// <typeparam name="TAuditEntryEntity">The type of AuditEntryEntity</typeparam>
/// <remarks>
/// Initializes a new instance of the <see cref="DataUpdateResult{TDataEntity, TAuditEntryEntity}"/> struct.
/// </remarks>
/// <param name="dataEntity">The data entity.</param>
/// <param name="auditEntryEntity">The audit entry entity.</param>
public readonly struct DataUpdateResult<TDataEntity, TAuditEntryEntity>(TDataEntity dataEntity, TAuditEntryEntity auditEntryEntity)
{
    /// <summary>
    /// Gets the data entity that was created/modified
    /// </summary>
    public TDataEntity DataEntity { get; } = dataEntity;

    /// <summary>
    /// Gets the audit entry entity that was created/modified
    /// </summary>
    public TAuditEntryEntity AuditEntryEntity { get; } = auditEntryEntity;
}
