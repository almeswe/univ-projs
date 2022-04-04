using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace FTPServer
{
    public class Program
    {
        public static void Main(string[] args) =>
            CreateHostBuilder().Run();

        public static IWebHost CreateHostBuilder()
        {
            return new WebHostBuilder()
                .UseKestrel()
                .UseUrls("http://localhost:5000", "http://192.168.100.72:5000")
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();
        }
    }
}
