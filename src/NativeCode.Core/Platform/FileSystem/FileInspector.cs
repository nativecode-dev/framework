namespace NativeCode.Core.Platform.FileSystem
{
    public abstract class FileInspector : IFileInspector
    {
        public abstract string GetMimeTypeFromFileName(string filename);
    }
}