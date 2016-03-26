namespace Console.Engine.Objects.Mobiles
{
    public class Player : GameObjectActor
    {
        public Player(string id, string name, int x, int y, int healthCurrent, int healthMax)
            : base(id, name, x, y, healthCurrent, healthMax)
        {
        }
    }
}