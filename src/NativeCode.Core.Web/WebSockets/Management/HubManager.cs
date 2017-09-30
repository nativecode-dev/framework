namespace NativeCode.Core.Web.WebSockets.Management
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Caching.Memory;

    public class HubManager : IHubManager
    {
        private readonly IMemoryCache cache;

        public HubManager(IMemoryCache cache)
        {
            this.cache = cache;
        }

        public Task<IHub> GetHub(Guid id)
        {
            return this.cache.GetOrCreateAsync(id, entry =>
            {
                IHub hub = new Hub(id);
                entry.SlidingExpiration = TimeSpan.FromHours(2);
                entry.Value = hub;
                return Task.FromResult(hub);
            });
        }
    }
}