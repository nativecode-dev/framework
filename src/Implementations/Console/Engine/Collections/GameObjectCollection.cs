namespace Console.Engine.Collections
{
    using System.Collections.ObjectModel;

    using Console.Engine.Objects;

    public abstract class GameObjectCollection<T> : Collection<T>
        where T : GameObject
    {
    }
}