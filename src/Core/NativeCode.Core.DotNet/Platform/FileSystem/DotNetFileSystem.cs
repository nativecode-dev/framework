namespace NativeCode.Core.DotNet.Platform.FileSystem
{
    using System.IO;
    using Core.Platform.FileSystem;

    public class DotNetFileSystem : IFileSystem
    {
        public string GetText(string filename)
        {
            return File.ReadAllText(filename);
        }

        public bool Exists(string filename)
        {
            return File.Exists(filename);
        }

        public void SetText(string filename, string text)
        {
            File.WriteAllText(filename, text);
        }
    }
}