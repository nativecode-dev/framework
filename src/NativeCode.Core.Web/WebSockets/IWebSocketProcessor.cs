namespace NativeCode.Core.Web.WebSockets
{
    using System.Threading.Tasks;
    using Types;

    public interface IWebSocketProcessor
    {
        Task Start(WebSocketConnection connection);
    }
}