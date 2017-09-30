namespace NativeCode.Core.Web.WebSockets.Management
{
    using System;
    using System.Threading.Tasks;

    public interface IHubManager
    {
        Task<IHub> GetHub(Guid id);
    }
}