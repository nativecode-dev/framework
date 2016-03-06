namespace Console
{
    internal class ConsoleConfiguration
    {
        public ConsoleConfiguration(int width, int height)
        {
            this.Height = height;
            this.Width = width;
        }

        public int Height { get; }

        public int Width { get; }
    }
}