namespace NativeCode.Core.Types
{
    using System;

    public class StreamMonitorEventArgs : EventArgs
    {
        public StreamMonitorEventArgs(long current, long total)
        {
            this.Current = current;
            this.Total = total;
        }

        public long Current { get; }

        public decimal Percentage => ((decimal)this.Current / this.Total) * 100;

        public long Total { get; }
    }
}