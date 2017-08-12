namespace NativeCode.Core.Platform.Maintenance
{
    /// <summary>
    /// Provides a contract to manage maintenance state.
    /// </summary>
    public interface IMaintenanceProvider
    {
        /// <summary>
        /// Gets a value indicating whether this <see cref="IMaintenanceProvider" /> is maintenance.
        /// </summary>
        bool Active { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        string Name { get; }

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