namespace NativeCode.Core.Platform.Security.KeyManagement
{
    using System;
    using System.IO;

    using NativeCode.Core.Platform.FileSystem;

    public class KeyManager : IKeyManager
    {
        public KeyManager(IPlatform platform, IFileSystem files)
        {
            this.Files = files;
            this.Platform = platform;
        }

        protected IFileSystem Files { get; }

        protected IPlatform Platform { get; }

        public string GetDefaultKey()
        {
            return this.GetKey("default");
        }

        public string GetKey(string name)
        {
            if (name == "default")
            {
                var filename = Path.Combine(this.Platform.DataPath, $"{this.Platform.MachineName}.default.key");

                if (this.Files.Exists(filename))
                {
                    return this.Files.GetText(filename);
                }

                var key = Guid.NewGuid().ToString();
                this.Files.SetText(filename, key);

                return key;
            }

            return default(string);
        }
    }
}