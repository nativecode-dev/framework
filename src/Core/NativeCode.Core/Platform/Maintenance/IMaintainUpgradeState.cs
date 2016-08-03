namespace NativeCode.Core.Platform.Maintenance
{
    /// <summary>
    /// Provides a contract to manage maintenance state.
    /// </summary>
    public interface IMaintainUpgradeState
    {
        /// <summary>
        /// Gets a value indicating whether this <see cref="IMaintainUpgradeState" /> is maintenance.
        /// </summary>
        bool Active { get; }

        /// <summary>
        /// Enters the maintenance.
        /// </summary>
        void EnterMaintenance();

        /// <summary>
        /// Exits the maintenance.
        /// </summary>
        /// <param name="key">The key.</param>
        void ExitMaintenance(string key);

        /// <summary>
        /// Generates the maintenance key.
        /// </summary>
        /// <returns>Returns a maintenance key.</returns>
        string GenerateMaintenanceKey();
    }
}