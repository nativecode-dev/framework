namespace Console
{
    using NativeCode.Core.DotNet.Console;

    internal class Program
    {
        public static void Main(string[] args)
        {
            var settings = new ScreenSettings { CursorVisible = false, ScreenHeight = 56, ScreenWidth = 150 };

            using (var screen = new Screen(settings))
            {
                screen.Start();
            }
        }
    }
}