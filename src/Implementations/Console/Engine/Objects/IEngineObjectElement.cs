namespace Console.Engine.Objects
{
    using NativeCode.Core.Types.Structs;

    public interface IEngineObjectElement : IEngineObject
    {
        Position Current { get; }

        Position Previous { get; }

        void Move(int x, int y, int z = 0);
    }
}