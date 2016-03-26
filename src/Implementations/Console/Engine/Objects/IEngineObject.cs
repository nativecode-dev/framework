namespace Console.Engine.Objects
{
    public interface IEngineObject
    {
        bool IsDirty { get; }

        string ObjectId { get; }

        string ObjectName { get; }

        void Invalidate();
    }
}