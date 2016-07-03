namespace Console
{
    using Console.Engine.Objects.Mobiles;
    using NativeCode.Core.DotNet.Win32;
    using Nito.AsyncEx;
    using System;
    using System.IO;
    using System.Threading.Tasks;

    internal class Program
    {
        private static readonly string MapFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "default.map");

        public static void Main(string[] args)
        {
            var encoding = NativeHelper.GetConsoleEncoding();

            AsyncContext.Run(MainAsync);
        }

        private static async Task MainAsync()
        {
#if RELEASE
            if (File.Exists(MapFilePath))
            {
                File.Delete(MapFilePath);
            }
#endif
            using (var engine = new Engine.Engine(150, 56))
            {
                var random = new Random(4096);

                if (File.Exists(MapFilePath))
                {
                    engine.Load(MapFilePath);
                }
                else
                {
                    engine.NewMap(MapFilePath, 4096, 4096);
                }

                var population = random.Next(1024, 3072);

                for (var index = 0; index < population; index++)
                {
                    var x = random.Next(0, 4095);
                    var y = random.Next(0, 4095);
                    engine.Context.Add(new Mogwai(x, y));
                }

                await engine.StartAsync().ConfigureAwait(false);
            }
        }
    }
}
