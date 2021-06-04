using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orleans.Configuration;
using Orleans.Hosting;

namespace Sample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
               .UseOrleans(siloBuilder =>
               {
                   siloBuilder.UseLocalhostClustering()
                    .Configure<ClusterOptions>(options =>
                    {
                        options.ClusterId = "dev";
                        options.ServiceId = "HelloWorldApp";
                    }).
                     ConfigureEndpoints(siloPort: 11111, gatewayPort: 30000)                    
                     .ConfigureLogging(logging => logging.AddConsole());
               })
                .ConfigureLogging(logging =>
                {
                    logging.AddConsole();
                });
    }
}
