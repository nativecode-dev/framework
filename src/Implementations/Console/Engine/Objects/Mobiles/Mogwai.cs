namespace Console.Engine.Objects.Mobiles
{
    using System;

    public class Mogwai : Mobile
    {
        public Mogwai(int x, int y)
            : base(Guid.NewGuid().ToString(), "Mogwai", x, y, 100, 100)
        {
        }
    }
}