namespace Console.Engine.Objects
{
    public abstract class GameObject : EngineObjectElement
    {
        protected GameObject(string id, string name, int x, int y)
            : base(id, name, x, y)
        {
        }
    }
}