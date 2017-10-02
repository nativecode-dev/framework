namespace NativeCode.Core.Web.WebSockets.Types
{
    using Core.Extensions;
    using Management;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Platform.Serialization;
    using Reliability;
    using System;
    using System.IO;
    using System.Net.WebSockets;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public class WebSocketConnection : Disposable
    {
        private readonly IStringSerializer serializer;

        protected internal WebSocketConnection(HttpContext context, WebSocket socket, IHub hub)
        {
            this.Context = context;
            this.Hub = hub;
            this.WebSocket = socket;

            this.serializer = context.RequestServices.GetService<IStringSerializer>();
        }

        public IHub Hub { get; }

        public Guid HubId => this.Hub.HubId;

        public bool IsReadable => this.Token.IsCancellationRequested == false && this.WebSocket.State == WebSocketState.Open;

        public CancellationToken Token => this.Context.RequestAborted;

        public WebSocket WebSocket { get; }

        protected HttpContext Context { get; }

        public async Task<T> GetNext<T>(CancellationToken token)
        {
            var json = await this.GetNextText(token).NoCapture();

            if (string.IsNullOrWhiteSpace(json))
            {
                return default(T);
            }

            return this.serializer.Deserialize<T>(json);
        }

        public async Task<Stream> GetNextStream(CancellationToken token)
        {
            var buffer = new ArraySegment<byte>(new byte[Common.Streams.BufferSize]);
            var stream = new MemoryStream();

            try
            {
                WebSocketReceiveResult result;

                do
                {
                    result = await this.WebSocket.ReceiveAsync(buffer, token).NoCapture();
                    await stream.WriteAsync(buffer.Array, buffer.Offset, result.Count, token).NoCapture();
                }
                while (result.EndOfMessage == false);

                stream.Seek(0, SeekOrigin.Begin);

                var tempStream = stream;
                stream = null;

                return tempStream;
            }
            catch
            {
                stream?.Dispose();
                throw;
            }
        }

        public async Task<string> GetNextText(CancellationToken token)
        {
            var buffer = new ArraySegment<byte>(new byte[8192]);

            using (var stream = await this.GetNextStream(token).NoCapture())
            {
                using (var reader = new StreamReader(stream, Encoding.UTF8, true, buffer.Array.Length, true))
                {
                    return await reader.ReadToEndAsync().NoCapture();
                }
            }
        }

        public Task Send<T>(T data, CancellationToken token)
        {
            var json = this.serializer.Serialize(data);

            if (string.IsNullOrWhiteSpace(json))
            {
                return Task.CompletedTask;
            }

            return this.SendText(json, token);
        }

        public async Task SendBinary(Stream stream, CancellationToken token)
        {
            var buffer = new ArraySegment<byte>();

            do
            {
                await stream
                    .ReadAsync(buffer.Array, buffer.Offset, Common.Streams.BufferSize, token)
                    .NoCapture();
            }
            while (stream.Position < stream.Length);
        }

        public Task SendText(string data, CancellationToken token)
        {
            var bytes = Encoding.UTF8.GetBytes(data);
            var buffer = new ArraySegment<byte>(bytes);

            // NOTE: We have to set endOfMessage to true!
            return this.WebSocket.SendAsync(buffer, WebSocketMessageType.Text, true, token);
        }

        protected override void ReleaseManaged()
        {
            this.Hub.Remove(this);
            this.WebSocket.Dispose();
        }
    }
}