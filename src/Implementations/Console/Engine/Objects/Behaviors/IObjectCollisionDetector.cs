namespace Console.Engine.Objects.Behaviors
{
    public interface IObjectCollisionDetector
    {
        bool IsAdjacent(IEngineObjectElement source, IEngineObjectElement target);
    }
}