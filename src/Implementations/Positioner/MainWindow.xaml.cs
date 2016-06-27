namespace Positioner
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Windows;

    using NativeCode.Core.DotNet.Win32;
    using NativeCode.Core.DotNet.Win32.Structs;

    /// <summary>
    /// Interaction logic for MainWindow.
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.AppProcess = Process.GetCurrentProcess();
            this.InitializeComponent();
        }

        protected Process AppProcess { get; }

        protected ForegroundChangeHook ForegroundChangeHook { get; private set; }

        protected IntPtr? ForegroundHandle { get; private set; }

        protected override void OnActivated(EventArgs e)
        {
            if (this.ForegroundChangeHook == null)
            {
                this.ForegroundChangeHook = new ForegroundChangeHook(this.AppProcess.MainWindowHandle, this.HandleForegroundChange);
                Trace.WriteLine("Created hook.");
            }

            base.OnActivated(e);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (this.ForegroundChangeHook != null)
            {
                this.ForegroundChangeHook.Dispose();
                this.ForegroundChangeHook = null;
                Trace.WriteLine("Removed hook.");
            }

            this.Visibility = Visibility.Hidden;
            e.Cancel = true;

            base.OnClosing(e);
        }

        private void HandleForegroundChange(IntPtr hwnd)
        {
            if (this.Visibility != Visibility.Visible)
            {
                return;
            }

            try
            {
                uint id;

                NativeMethods.GetWindowThreadProcessId(hwnd, out id);

                using (var process = Process.GetProcessById((int)id))
                {
                    if (process.Id != this.AppProcess.Id)
                    {
                        this.ForegroundHandle = process.MainWindowHandle;
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }

        private bool GetWindowThreadProcessId(IntPtr hwnd, out uint id)
        {
            NativeMethods.GetWindowThreadProcessId(hwnd, out id);
            return true;
        }

        private void ClickCenter(object sender, RoutedEventArgs e)
        {
            if (this.ForegroundHandle.HasValue)
            {
                WindowHelper.Center(this.ForegroundHandle.Value);
            }
        }

        private void ClickBottom(object sender, RoutedEventArgs e)
        {
            if (this.ForegroundHandle.HasValue)
            {
                WindowHelper.CenterBottom(this.ForegroundHandle.Value);
            }
        }

        private void ClickTop(object sender, RoutedEventArgs e)
        {
            if (this.ForegroundHandle.HasValue)
            {
                WindowHelper.CenterTop(this.ForegroundHandle.Value);
            }
        }

        private void ClickQuit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(0);
        }

        private void ClickShow(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Visible;
        }
    }
}