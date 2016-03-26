namespace Console.Engine
{
    using System.Collections.Generic;

    using Console.Engine.Collections;
    using Console.Engine.Objects.Mobiles;

    public class EngineContext
    {
        private readonly MobileCollection mobiles = new MobileCollection();

        private readonly PlayerCollection players = new PlayerCollection();

        public IReadOnlyList<Mobile> Mobiles => this.mobiles;

        public IReadOnlyList<Player> Players => this.players;

        public void Add(Player player)
        {
            this.players.Add(player);
        }

        public void Add(Mobile mobile)
        {
            this.mobiles.Add(mobile);
        }
    }
}