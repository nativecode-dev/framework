namespace NativeCode.Web.Models
{
    using System.Collections.Generic;
    using System.Linq;

    public class ResponseCollection<T> : Response
    {
        public IEnumerable<T> Items { get; set; } = Enumerable.Empty<T>();
    }
}