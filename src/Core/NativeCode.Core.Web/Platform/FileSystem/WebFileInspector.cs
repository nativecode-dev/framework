namespace NativeCode.Core.Web.Platform.FileSystem
{
    using System.Web;

    using NativeCode.Core.Platform.FileSystem;

    public class WebFileInspector : FileInspector
    {
        public override string GetMimeTypeFromFileName(string filename)
        {
            return MimeMapping.GetMimeMapping(filename);
        }
    }
}
