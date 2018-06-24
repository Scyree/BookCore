using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using ExploreBooks;

namespace ExploreBooks
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls("https://localhost:44307/")
                .Build();
    }
}
