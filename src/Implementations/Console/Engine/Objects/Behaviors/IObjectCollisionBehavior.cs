namespace Console.Engine.Objects.Behaviors
{
    public interface IObjectCollisionBehavior
    {
        void Reaction(IEngineObjectElement source);
    }
}