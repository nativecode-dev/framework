namespace Services
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;

    using Common.Data.Entities;
    using Common.Web;
    using Common.Workers;

    using Nito.AsyncEx;

    using PowerArgs;

    internal class Program
    {
        public static void Main(string[] args)
        {
            var options = Args.Parse<CommandOptions>(args);
            AsyncContext.Run(async () => await MainAsync(options));
        }

        private static Task MainAsync(CommandOptions options)
        {
            Trace.Listeners.Add(new ConsoleTraceListener());

            using (var app = new ServicesApplication())
            {
                if (options.CreateSettings)
                {
                    return Task.FromResult(0);
                }

                using (var workers = app.Container.Resolver.Resolve<IWorkManager<Download>>())
                {
                    workers.StartAsync();
                    Console.ReadKey(true);
                }
            }

            return Task.FromResult(0);
        }

        public class CommandOptions
        {
            [ArgShortcut("settings")]
            public bool CreateSettings { get; set; }
        }
    }
}