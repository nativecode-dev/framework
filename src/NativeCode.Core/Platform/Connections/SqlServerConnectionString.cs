namespace NativeCode.Core.Platform.Connections
{
    using Dependencies.Attributes;
    using JetBrains.Annotations;

    [IgnoreDependency("Use the new operator.")]
    public class SqlServerConnectionString : ConnectionString
    {
        public SqlServerConnectionString()
        {
            this.ResolvePropertyKey(x => string.Equals(x, "AsynchronousProcessing") ? "Asynchronous Processing" : null);
            this.ResolvePropertyKey(x => string.Equals(x, "DataSource") ? "Data Source" : null);
            this.ResolvePropertyKey(x => string.Equals(x, "InitialCatalog") ? "Initial Catalog" : null);
            this.ResolvePropertyKey(x => string.Equals(x, "IntegratedSecurity") ? "Integrated Security" : null);
            this.ResolvePropertyKey(x => string.Equals(x, "MultipleActiveResultSets") ? "MultipleActiveResultSets" : null);
            this.ResolvePropertyKey(x => string.Equals(x, "Password") ? "Password" : null);
            this.ResolvePropertyKey(x => string.Equals(x, "TrustedConnection") ? "Trusted_Connection" : null);
            this.ResolvePropertyKey(x => string.Equals(x, "UserId") ? "User Id" : null);
        }

        public SqlServerConnectionString([NotNull] string connectionString) : this()
        {
            this.Parse(connectionString);
        }

        public bool AsynchronousProcessing
        {
            get => this.GetValue<bool>();
            set => this.SetValue(value);
        }

        public string DataSource
        {
            get => this.GetValue<string>();
            set => this.SetValue(value);
        }

        public string IntialCatalog
        {
            get => this.GetValue<string>();
            set => this.SetValue(value);
        }

        public bool IntegratedSecurity
        {
            get => this.GetValue<bool>();
            set => this.SetValue(value);
        }

        public bool MultipleActiveResultSets
        {
            get => this.GetValue<bool>();
            set => this.SetValue(value);
        }

        public string Password
        {
            get => this.GetValue<string>();
            set => this.SetValue(value);
        }

        public bool TrustedConnection
        {
            get => this.GetValue<bool>();
            set => this.SetValue(value);
        }

        public string UserId
        {
            get => this.GetValue<string>();
            set => this.SetValue(value);
        }
    }
}