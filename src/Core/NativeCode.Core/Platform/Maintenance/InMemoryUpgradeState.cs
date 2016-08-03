namespace NativeCode.Core.Platform.Maintenance
{
    using System;
    using System.Threading;

    public class InMemoryUpgradeState : IMaintainUpgradeState
    {
        private static int state;

        private static string generated;

        /// <summary>
        /// Gets a value indicating whether this <see cref="IMaintainUpgradeState" /> is in maintenance.
        /// </summary>
        public bool Active => state != 0;

        /// <summary>
        /// Gets the maintenance key.
        /// </summary>
        public string MaintenanceKey => generated;

        public void EnterMaintenance()
        {
            if (Interlocked.CompareExchange(ref state, 1, 1) == 0)
            {
                generated = this.GenerateMaintenanceKey();
            }
        }

        public void ExitMaintenance(string key)
        {
            if (Interlocked.CompareExchange(ref state, 0, 0) == 1 && key == generated)
            {
                key = null;
            }
        }

        public virtual string GenerateMaintenanceKey()
        {
            return Guid.NewGuid().ToString();
        }
    }
}