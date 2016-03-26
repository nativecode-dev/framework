namespace Console.Engine.Objects
{
    using NativeCode.Core.Types.Structs;

    public abstract class EngineObjectElement : EngineObject, IEngineObjectElement
    {
        protected EngineObjectElement(string id, string name, int x, int y, int z = 0)
            : base(id, name)
        {
            this.Current = new Position(x, y, z);
            this.Previous = new Position(x, y, z);
        }

        public Position Current { get; private set; }

        public Position Previous { get; private set; }

        public void Move(int x, int y, int z = 0)
        {
            this.Previous = this.Current;
            this.Current = new Position(x, y, z);
        }
    }
}