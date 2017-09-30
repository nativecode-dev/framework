namespace NativeCode
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;

    public class Program
    {
        public static void Main(string[] args)
        {
            Program.Configure(args).GetAwaiter().GetResult();
        }

        public static Task Configure(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<ProgramStartup>()
                .Build()
                .RunAsync();
        }
    }
}