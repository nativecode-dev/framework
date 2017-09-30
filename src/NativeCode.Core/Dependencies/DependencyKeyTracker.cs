namespace NativeCode.Core.Dependencies
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;

    public class DependencyKeyTracker
    {
        private readonly ConcurrentDictionary<Type, DependencyKey> keys;

        public DependencyKeyTracker()
        {
            this.keys = new ConcurrentDictionary<Type, DependencyKey>();
        }

        public IEnumerable<DependencyDescription> GetDependencies(Type contract)
        {
            return this.keys[contract].GetDescriptions();
        }

        public bool HasKey(Type contract)
        {
            return this.keys.ContainsKey(contract);
        }

        public bool TryAdd(DependencyDescription description)
        {
            var key = description.Contract;

            if (this.keys.ContainsKey(key) == false)
            {
                var item = new DependencyKey(description.KeyValue);

                if (this.keys.TryAdd(key, item) == false)
                {
                    throw new InvalidOperationException($"Failed to add key: {key}");
                }
            }

            try
            {
                this.keys[key].Add(description);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public class DependencyKey
        {
            private readonly ConcurrentBag<DependencyDescription> dependencies;

            public DependencyKey(string key)
            {
                this.dependencies = new ConcurrentBag<DependencyDescription>();

                this.Key = key;
            }

            public string Key { get; }

            public void Add(DependencyDescription description)
            {
                this.dependencies.Add(description);
            }

            public IEnumerable<DependencyDescription> GetDescriptions()
            {
                return this.dependencies.ToArray();
            }
        }
    }
}