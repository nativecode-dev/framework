namespace NativeCode.Core.Platform.FileSystem
{
    using JetBrains.Annotations;

    public interface IFileInspector
    {
        string GetMimeTypeFromFileName([NotNull] string filename);
    }
}