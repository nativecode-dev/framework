namespace NativeCode.Core.Platform.FileSystem
{
    using JetBrains.Annotations;

    public interface IFileInspector
    {
        [NotNull]
        string GetMimeTypeFromFileName([NotNull] string filename);
    }
}