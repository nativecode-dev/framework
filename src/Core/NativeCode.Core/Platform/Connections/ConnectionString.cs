namespace NativeCode.Core.Platform.Connections
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    using Humanizer;

    using JetBrains.Annotations;

    public class ConnectionString : DynamicObject
    {
        private readonly Dictionary<string, object> members = new Dictionary<string, object>();

        private readonly List<Func<string, string>> resolvers = new List<Func<string, string>>();

        public ConnectionString()
        {
        }

        public ConnectionString([NotNull] string connectionString)
            : this()
        {
            this.Parse(connectionString);
        }

        public object this[string key]
        {
            get
            {
                return this.members.ContainsKey(key) ? this.members[key] : default(object);
            }

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

        public override IEnumerable<string> GetDynamicMemberNames()
        {
            return this.members.Keys;
        }

        public override string ToString()
        {
            return string.Join("; ", this.members.Select(x => $"{this.Resolve(x.Key)}={x.Value}"));
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
            return (T)this[key];
        }

        protected void Resolver(Func<string, string> resolver)
        {
            this.resolvers.Add(resolver);
        }

        protected void SetValue<T>(T value, [CallerMemberName] string key = null)
        {
            this[key] = value;
        }

        protected void Parse([NotNull] string connectionString)
        {
            var properties = connectionString.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var property in properties)
            {
                var parts = property.Split('=');
                var key = parts[0].Trim().Dehumanize();
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