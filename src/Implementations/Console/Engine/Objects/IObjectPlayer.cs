namespace Console.Engine.Objects
{
    public interface IObjectPlayer : IEngineObjectElement
    {
        int HealthCurrent { get; }

        int HealthMax { get; }
    }
}