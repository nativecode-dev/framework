namespace Console
{
    using System;

    using NativeCode.Core.DotNet.Console;

    internal class Program
    {
        public static void Main(string[] args)
        {
            using (var console = new StdOutputConsoleHandle())
            {
                Console.ReadKey(true);
            }
        }
    }
}