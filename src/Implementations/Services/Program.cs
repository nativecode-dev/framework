namespace Services
{
    using System;

    internal class Program
    {
        public static void Main(string[] args)
        {
            using (var app = new ServicesApplication())
            {
                var url = new Uri(app.Settings.GetValue("application.services.base_url", "http://localhost:9000"));

                using (app.Start<ServicesStartup>(url))
                {
                    Console.WriteLine($"Started host at {url}.");
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
}