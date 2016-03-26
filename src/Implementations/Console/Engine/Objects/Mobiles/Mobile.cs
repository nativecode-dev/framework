namespace Console.Engine.Objects.Mobiles
{
    public abstract class Mobile : GameObjectActor
    {
        protected Mobile(string id, string name, int x, int y, int healthCurrent, int healthMax)
            : base(id, name, x, y, healthCurrent, healthMax)
        {
        }
    }
}