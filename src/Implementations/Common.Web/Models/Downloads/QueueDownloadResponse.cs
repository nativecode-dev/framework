namespace Common.Web.Models.Downloads
{
    using System;

    using NativeCode.Web.Models;

    public class QueueDownloadResponse : Response
    {
        public Guid Id { get; set; }
    }
}