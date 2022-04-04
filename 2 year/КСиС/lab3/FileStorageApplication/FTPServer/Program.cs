using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace FTPServer
{
    public class Program
    {
        public static string RootPath { get; private set; }

        public static void Main(string[] args) =>
            CreateHostBuilder(args).Run();

        public static IWebHost CreateHostBuilder(string[] args)
        {
            if (args.Length != 3)
                throw new System.ArgumentException(
                    "Incorrect argument count passed.");
            var host = new WebHostBuilder().UseKestrel()
                .UseUrls($"http://{args[0]}:{args[1]}")
                .UseIISIntegration()
                .UseContentRoot(args[2]);
            RootPath = args[2];
            return host.UseStartup<Startup>().Build();
        }
    }
}
