namespace Console.Engine
{
    using System.IO;

    public abstract class GameFileCollectionStream<T> : GameFileStream
        where T : struct
    {
        protected GameFileCollectionStream(Stream stream, bool owner = true)
            : base(stream, owner)
        {
        }

        protected void IndexStream()
        {
        }
    }
}