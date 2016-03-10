namespace Services
{
    using System;

    using Microsoft.Owin.Hosting;

    internal class Program
    {
        public static void Main(string[] args)
        {
            using (WebApp.Start<ServicesApplication>("http://localhost:9000/"))
            {
                Console.ReadKey(true);
            }
        }
    }
}