namespace NativeCode.Core.Platform.Connections
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using Dependencies.Attributes;
    using JetBrains.Annotations;

    [IgnoreDependency("Use the new operator.")]
    public class ConnectionString : DynamicObject
    {
        private readonly Dictionary<string, object> members;

        private readonly List<Func<string, string>> resolvers;

        public ConnectionString() : this(new Dictionary<string, object>(), new List<Func<string, string>>())
        {
        }

        public ConnectionString([NotNull] string connectionString) : this()
        {
            this.Parse(connectionString);
        }

        public ConnectionString([NotNull] ConnectionString connectionString) : this(connectionString.members,
            connectionString.resolvers)
        {
        }

        private ConnectionString(Dictionary<string, object> members, List<Func<string, string>> resolvers)
        {
            this.members = members;
            this.resolvers = resolvers;

            this.ResolvePropertyKey(x => string.Equals(x, "HostName") ? "HostName" : null);
            this.ResolvePropertyKey(x => string.Equals(x, "Port") ? "Port" : null);
            this.ResolvePropertyKey(x => string.Equals(x, "Protocol") ? "Protocol" : null);
        }

        public string HostName
        {
            get => this.GetValue<string>();
            set => this.SetValue(value);
        }

        public int Port
        {
            get => this.GetValue<int>();
            set => this.SetValue(value);
        }

        public string Protocol
        {
            get => this.GetValue<string>();
            set => this.SetValue(value);
        }

        public char[] Separator => new[] { this.SeparatorChar };

        public char SeparatorChar { get; protected set; } = ';';

        public object this[string key]
        {
            get => this.members.ContainsKey(key) ? this.members[key] : default(object);
            set
            {
                if (this.members.ContainsKey(key))
                {
                    this.members[key] = value;
                }
                else
                {
                    this.members.Add(key, value);
                }
            }
        }

        public static implicit operator string(ConnectionString instance)
        {
            return object.Equals(instance, null) ? null : instance.ToString();
        }

        public static implicit operator ConnectionString(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return new ConnectionString();
            }

            return new ConnectionString(value);
        }

        public override IEnumerable<string> GetDynamicMemberNames()
        {
            return this.members.Keys;
        }

        public override string ToString()
        {
            return string.Join("; ", this.members.Select(x => $"{this.Resolve(x.Key)}={x.Value}"));
        }

        public Uri ToUri()
        {
            return null;
        }

        public override bool TryConvert([NotNull] ConvertBinder binder, out object result)
        {
            if (binder.Type == typeof(string))
            {
                result = this.ToString();
                return true;
            }

            return base.TryConvert(binder, out result);
        }

        public override bool TryGetMember([NotNull] GetMemberBinder binder, out object result)
        {
            if (this.members.ContainsKey(binder.Name))
            {
                result = this.members[binder.Name];
                return true;
            }

            return base.TryGetMember(binder, out result);
        }

        public override bool TrySetMember([NotNull] SetMemberBinder binder, object value)
        {
            if (value != null)
            {
                if (this.members.ContainsKey(binder.Name))
                {
                    this.members[binder.Name] = value.ToString();
                }
                else
                {
                    this.members.Add(binder.Name, value.ToString());
                }

                return true;
            }

            return base.TrySetMember(binder, null);
        }

        protected T GetValue<T>([CallerMemberName] string key = null)
        {
            return (T) this[this.Resolve(key)];
        }

        protected void ResolvePropertyKey(Func<string, string> resolver)
        {
            this.resolvers.Add(resolver);
        }

        protected void SetValue<T>(T value, [CallerMemberName] string key = null)
        {
            this[this.Resolve(key)] = value;
        }

        protected void Parse([NotNull] string connectionString)
        {
            var properties = connectionString.Split(this.Separator, StringSplitOptions.RemoveEmptyEntries);

            foreach (var property in properties)
            {
                var parts = property.Split('=');
                var key = parts[0].Trim();
                var value = parts[1].Trim();

                if (!this.members.ContainsKey(key))
                {
                    this.members.Add(key, value);
                }
            }
        }

        private string Resolve(string key)
        {
            foreach (var resolver in this.resolvers)
            {
                var resolved = resolver(key);

                if (!string.IsNullOrWhiteSpace(resolved))
                {
                    return resolved;
                }
            }

            return key;
        }
    }
}