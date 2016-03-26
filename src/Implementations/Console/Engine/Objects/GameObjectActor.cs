namespace Console.Engine.Objects
{
    public abstract class GameObjectActor : GameObject, IObjectPlayer
    {
        protected GameObjectActor(string id, string name, int x, int y, int healthCurrent, int healthMax)
            : base(id, name, x, y)
        {
            this.HealthCurrent = healthCurrent;
            this.HealthMax = healthMax;
        }

        public int HealthCurrent { get; }

        public int HealthMax { get; }
    }
}