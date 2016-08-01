namespace NativeCode.Core.Platform.Connections
{
    using JetBrains.Annotations;

    public class SqlServerConnectionString : ConnectionString
    {
        public SqlServerConnectionString()
        {
            this.Resolver(x => string.Equals(x, "AsynchronousProcessing") ? "Asynchronous Processing" : null);
            this.Resolver(x => string.Equals(x, "DataSource") ? "Data Source" : null);
            this.Resolver(x => string.Equals(x, "InitialCatalog") ? "Initial Catalog" : null);
            this.Resolver(x => string.Equals(x, "IntegratedSecurity") ? "Integrated Security" : null);
            this.Resolver(x => string.Equals(x, "MultipleActiveResultSets") ? "MultipleActiveResultSets" : null);
            this.Resolver(x => string.Equals(x, "TrustedConnection") ? "Trusted_Connection" : null);
            this.Resolver(x => string.Equals(x, "UserId") ? "User Id" : null);
        }

        public SqlServerConnectionString([NotNull] string connectionString)
            : this()
        {
            this.Parse(connectionString);
        }

        public bool AsynchronousProcessing
        {
            get
            {
                return this.GetValue<bool>();
            }

            set
            {
                this.SetValue(value);
            }
        }

        public bool DataSource
        {
            get
            {
                return this.GetValue<bool>();
            }

            set
            {
                this.SetValue(value);
            }
        }

        public bool IntialCatalog
        {
            get
            {
                return this.GetValue<bool>();
            }

            set
            {
                this.SetValue(value);
            }
        }

        public bool IntegratedSecurity
        {
            get
            {
                return this.GetValue<bool>();
            }

            set
            {
                this.SetValue(value);
            }
        }

        public bool MultipleActiveResultSets
        {
            get
            {
                return this.GetValue<bool>();
            }

            set
            {
                this.SetValue(value);
            }
        }

        public bool TrustedConnection
        {
            get
            {
                return this.GetValue<bool>();
            }

            set
            {
                this.SetValue(value);
            }
        }

        public bool UserId
        {
            get
            {
                return this.GetValue<bool>();
            }

            set
            {
                this.SetValue(value);
            }
        }
    }
}
