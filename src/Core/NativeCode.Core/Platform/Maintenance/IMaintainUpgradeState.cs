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
        /// Enters maintenance mode.
        /// </summary>
        /// <returns>Returns maintenance key.</returns>
        string EnterMaintenance();

        /// <summary>
        /// Exits maintenance mode.
        /// </summary>
        /// <param name="key">The key.</param>
        void ExitMaintenance(string key);
    }
}