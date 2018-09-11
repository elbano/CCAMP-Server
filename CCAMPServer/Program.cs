using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

namespace CCAMPServer
{
    public class Program
    {
        private static CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

        public static void Main(string[] args)
        {            
            BuildWebHost(args).RunAsync(cancelTokenSource.Token).Wait();
        }

        public static IWebHost BuildWebHost(String[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseSerilog()
                .Build();
        }
           

        static void SelectConfiguration(WebHostBuilderContext ctx, IConfigurationBuilder config)
        {
            config.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{ctx.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true);

        }

        public static void Shutdown()
        {
            cancelTokenSource.Cancel();
        }
    }
}
