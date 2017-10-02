namespace NativeCode.Core.Web.WebSockets.Management
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Caching.Memory;
    using Reliability;

    public class HubManager : Disposable, IHubManager
    {
        private readonly IMemoryCache cache;

        public HubManager(IMemoryCache cache)
        {
            this.cache = cache;
        }

        public Task<IHub> CreateHub(Guid id)
        {
            IHub hub = new Hub(id);
            var entry = this.cache.CreateEntry(id);
            HubManager.SetEntryProperties(hub, entry);

            return Task.FromResult(hub);
        }

        public Task<IHub> GetOrCreateHub(Guid id)
        {
            return this.cache.GetOrCreateAsync(id, entry =>
            {
                IHub hub = new Hub(id);
                HubManager.SetEntryProperties(hub, entry);
                return Task.FromResult(hub);
            });
        }

        public Task<bool> HubExists(Guid id)
        {
            if (this.cache.TryGetValue(id, out var _))
            {
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }

        protected override void ReleaseManaged()
        {
            this.cache.Dispose();
        }

        private static void HubEvicted(object key, object value, EvictionReason reason, object state)
        {
            if (value is IHub hub)
            {
                hub.Dispose();
            }
        }

        private static void SetEntryProperties(IHub hub, ICacheEntry entry)
        {
            entry.RegisterPostEvictionCallback(HubManager.HubEvicted);
            entry.SlidingExpiration = TimeSpan.FromHours(2);
            entry.Value = hub;
        }
    }
}