﻿namespace NativeCode.Core.Platform.Connections
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using JetBrains.Annotations;

    public class ConnectionString : DynamicObject
    {
        private readonly Dictionary<string, object> members;

        private readonly List<Func<string, string>> resolvers;

        public ConnectionString()
        {
            this.members = new Dictionary<string, object>();
            this.resolvers = new List<Func<string, string>>();
        }

        public ConnectionString([NotNull] string connectionString)
        {
            this.members = new Dictionary<string, object>();
            this.resolvers = new List<Func<string, string>>();
            this.Parse(connectionString);
        }

        public ConnectionString([NotNull] ConnectionString connectionString)
        {
            this.members = connectionString.members;
            this.resolvers = connectionString.resolvers;
        }

        public object this[string key]
        {
            get => this.members.ContainsKey(key) ? this.members[key] : default(object);

            set
            {
                if (this.members.ContainsKey(key))
                    this.members[key] = value;
                else
                    this.members.Add(key, value);
            }
        }

        public static implicit operator string(ConnectionString instance)
        {
            return Equals(instance, null) ? null : instance.ToString();
        }

        public static implicit operator ConnectionString(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return new ConnectionString();

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
                    this.members[binder.Name] = value.ToString();
                else
                    this.members.Add(binder.Name, value.ToString());

                return true;
            }

            return base.TrySetMember(binder, null);
        }

        protected T GetValue<T>([CallerMemberName] string key = null)
        {
            return (T) this[this.Resolve(key)];
        }

        protected void Resolver(Func<string, string> resolver)
        {
            this.resolvers.Add(resolver);
        }

        protected void SetValue<T>(T value, [CallerMemberName] string key = null)
        {
            this[this.Resolve(key)] = value;
        }

        protected void Parse([NotNull] string connectionString)
        {
            var properties = connectionString.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var property in properties)
            {
                var parts = property.Split('=');
                var key = parts[0].Trim();
                var value = parts[1].Trim();

                if (!this.members.ContainsKey(key))
                    this.members.Add(key, value);
            }
        }

        private string Resolve(string key)
        {
            foreach (var resolver in this.resolvers)
            {
                var resolved = resolver(key);

                if (!string.IsNullOrWhiteSpace(resolved))
                    return resolved;
            }

            return key;
        }
    }
}