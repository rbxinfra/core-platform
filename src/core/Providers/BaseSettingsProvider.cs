namespace Roblox.Platform.Core;

using System;

using Configuration;

/// <summary>
/// Base provider class for settings providers.
/// </summary>
public abstract class PlatformSettingsProvider : VaultProvider
{
    private const string _vaultMountEnvVar = "VAULT_MOUNT";
    private static readonly string _vaultMount = Environment.GetEnvironmentVariable(_vaultMountEnvVar) ?? "platform-settings";

    /// <summary>
    /// Gets the domain of the platform, e.g. "membership" or "authentication".
    /// </summary>
    protected abstract string Domain { get; }

    /// <summary>
    /// Gets the subordinate path for the settings provider.
    /// </summary>
    protected abstract string ChildPath { get; }

    /// <inheritdoc cref="IVaultProvider.Mount"/>
    public override string Mount => _vaultMount;

    /// <inheritdoc cref="IVaultProvider.Path"/>
    public override string Path => $"{EnvironmentNameProvider.EnvironmentName}/platform/rbx-platform/{Domain}/{ChildPath}";

    /// <summary>
    /// Construct a new instance of <see cref="PlatformSettingsProvider"/>
    /// </summary>
    protected PlatformSettingsProvider()
        : base(EventLog.Logger.Singleton)
    {
    }
}

/// <summary>
/// Base provider class for settings providers that exposes a singleton.
/// </summary>
/// <typeparam name="T">The type of the settings provider.</typeparam>
public abstract class PlatformSettingsProvider<T> : PlatformSettingsProvider
    where T : PlatformSettingsProvider<T>, new()
{
    private static readonly object _InstanceLock = new();
    private static T _Instance;

    /// <summary>
    /// Gets the singleton instance of the settings provider.
    /// </summary>
    public static T Singleton
    {
        get
        {
            if (_Instance == null)
            {
                lock (_InstanceLock)
                    _Instance ??= new T();
            }

            return _Instance;
        }
    }
}
