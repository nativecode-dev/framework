namespace NativeCode.Core.Platform.Maintenance
{
    using System;
    using System.Threading;

    public abstract class MaintenanceProvider : IMaintenanceProvider
    {
        private static int state;

        private static string generated;

        /// <summary>
        /// Gets a value indicating whether this <see cref="IMaintenanceProvider" /> is in maintenance.
        /// </summary>
        public bool Active => state != 0;

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name => $"In-Memory Upgrade State";

        /// <summary>
        /// Gets the maintenance key.
        /// </summary>
        public string MaintenanceKey => generated;

        public string EnterMaintenance()
        {
            if (Interlocked.CompareExchange(ref state, 1, 1) == 0)
            {
                generated = this.GenerateMaintenanceKey();
            }

            return generated;
        }

        public void ExitMaintenance(string key)
        {
            if (Interlocked.CompareExchange(ref state, 0, 0) == 1 && key == generated)
            {
                generated = null;
            }
        }

        protected virtual string GenerateMaintenanceKey()
        {
            return Guid.NewGuid().ToString();
        }
    }
}