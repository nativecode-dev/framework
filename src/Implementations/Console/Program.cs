namespace Console
{
    using System;
    using System.Diagnostics;

    using NativeCode.Core.DotNet.Console;

    internal class Program
    {
        public static void Main(string[] args)
        {
            Trace.Listeners.Add(new ConsoleTraceListener());

            Console.SetWindowSize(150, 50);
            Console.SetBufferSize(150, 50);

            var screen = new Screen();
            screen.ActiveBuffer.Write("abc");
            Console.ReadKey(true);
        }
    }
}