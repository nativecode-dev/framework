namespace NativeCode.Core.Web.WebSockets.Management
{
    using System;
    using System.Threading.Tasks;

    public interface IHubManager : IDisposable
    {
        Task<IHub> CreateHub(Guid id);

        Task<IHub> GetOrCreateHub(Guid id);

        Task<bool> HubExists(Guid id);
    }
}