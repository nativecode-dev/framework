namespace Services
{
    using System;

    using Common.Data.Entities;
    using Common.Web;
    using Common.Workers;

    using PowerArgs;

    internal class Program
    {
        public static void Main(string[] args)
        {
            var options = Args.Parse<CommandOptions>(args);

            using (var app = new ServicesApplication())
            {
                if (options.CreateSettings)
                {
                    return;
                }

                var url = new Uri(app.Settings.GetValue("application.services.base_url", "http://localhost:9000"));

                using (app.Start<ServicesStartup>(url))
                {
                    Console.WriteLine($"Started host at {url}.");

                    using (var downloads = app.Container.Resolver.Resolve<IWorkManager<Download>>())
                    {
                        downloads.StartAsync();
                        Console.WriteLine($"Started download watcher.");

                        ConsoleKeyInfo key;

                        do
                        {
                            key = Console.ReadKey(true);
                        }
                        while (key.KeyChar != 'q');
                    }
                }
            }
        }

        public class CommandOptions
        {
            [ArgShortcut("settings")]
            public bool CreateSettings { get; set; }
        }
    }
}