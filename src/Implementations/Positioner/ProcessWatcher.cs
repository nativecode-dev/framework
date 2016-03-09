namespace Positioner
{
    using System.Management;

    using WMI.Win32;

    public class ProcessWatcher : ManagementEventWatcher
    {
        private const string QueryProcesses = @"SELECT * FROM __InstanceOperationEvent WITHIN 1 WHERE TargetInstance ISA 'Win32_Process'";

        public delegate void ProcessEventHandler(Win32Process proc);

        public ProcessWatcher()
        {
            this.EventArrived += this.HandleEventArrived;
            this.Query.QueryLanguage = "WQL";
            this.Query.QueryString = QueryProcesses;
        }

        public event ProcessEventHandler ProcessCreated;

        public event ProcessEventHandler ProcessRemoved;

        public event ProcessEventHandler ProcessUpdated;

        private void HandleEventArrived(object sender, EventArrivedEventArgs e)
        {
            var type = e.NewEvent.ClassPath.ClassName;
            var process = new Win32Process(e.NewEvent["TargetInstance"] as ManagementBaseObject);

            switch (type)
            {
                case "__InstanceCreationEvent":
                    this.ProcessCreated?.Invoke(process);
                    break;

                case "__InstanceDeletionEvent":
                    this.ProcessRemoved?.Invoke(process);
                    break;

                case "__InstanceModificationEvent":
                    this.ProcessUpdated?.Invoke(process);
                    break;
            }
        }
    }
}