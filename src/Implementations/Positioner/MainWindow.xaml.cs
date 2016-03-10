namespace Positioner
{
    using System;
    using System.Diagnostics;
    using System.Windows;

    using NativeCode.Core.DotNet.Win32;

    using Rect = NativeCode.Core.DotNet.Win32.Structs.Rect;

    /// <summary>
    /// Interaction logic for MainWindow.
    /// </summary>
    public partial class MainWindow : Window, IDisposable
    {
        public MainWindow()
        {
            this.Process = Process.GetCurrentProcess();

            this.InitializeComponent();
            this.ForegroundChangeHook = new ForegroundChangeHook(this.Process.MainWindowHandle, this.HandleForegroundChange);
        }

        protected ForegroundChangeHook ForegroundChangeHook { get; private set; }

        protected IntPtr? ForegroundHandle { get; private set; }

        protected bool Disposed { get; private set; }

        protected Process Process { get; }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !this.Disposed)
            {
                this.Disposed = true;

                if (this.ForegroundChangeHook != null)
                {
                    this.ForegroundChangeHook.Dispose();
                    this.ForegroundChangeHook = null;
                }
            }
        }

        private void HandleForegroundChange(IntPtr hwnd)
        {
            try
            {
                uint id;

                NativeMethods.GetWindowThreadProcessId(hwnd, out id);

                using (var process = Process.GetProcessById((int)id))
                {
                    if (process.Id != this.Process.Id)
                    {
                        Rect bounds;

                        if (NativeMethods.GetWindowRect(hwnd, out bounds))
                        {
                            this.ForegroundHandle = process.MainWindowHandle;
                            this.Left = bounds.Right;
                            this.Top = bounds.Top;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }

        private void ClickCenter(object sender, RoutedEventArgs e)
        {
            if (this.ForegroundHandle.HasValue)
            {
                WindowHelper.Center(this.ForegroundHandle.Value);
            }
        }

        private void ClickHorizontally(object sender, RoutedEventArgs e)
        {
            if (this.ForegroundHandle.HasValue)
            {
                WindowHelper.CenterHorizontal(this.ForegroundHandle.Value);
            }
        }

        private void ClickVertically(object sender, RoutedEventArgs e)
        {
            if (this.ForegroundHandle.HasValue)
            {
                WindowHelper.CenterVertically(this.ForegroundHandle.Value);
            }
        }

        private void ClickBottom(object sender, RoutedEventArgs e)
        {
            if (this.ForegroundHandle.HasValue)
            {
                WindowHelper.CenterBottom(this.ForegroundHandle.Value);
            }
        }
    }
}