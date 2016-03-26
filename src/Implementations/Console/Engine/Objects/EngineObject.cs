namespace Console.Engine.Objects
{
    using NativeCode.Core.Settings;

    public abstract class EngineObject : JsonSettings, IEngineObject
    {
        protected EngineObject(string id, string name)
        {
            this.ObjectId = id;
            this.ObjectName = name;
        }

        public bool IsDirty { get; private set; }

        public string ObjectId { get; }

        public string ObjectName { get; }

        public void Invalidate()
        {
        }
    }
}