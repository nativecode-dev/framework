namespace Console.Engine
{
    using System.IO;

    using NativeCode.Core.Types;

    public abstract class GameFileStream : Disposable
    {
        protected GameFileStream(Stream stream, bool owner = true)
        {
            this.Stream = stream;
            this.StreamOwner = owner;
        }

        protected Stream Stream { get; private set; }

        protected bool StreamOwner { get; }
    }
}