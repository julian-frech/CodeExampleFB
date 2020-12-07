using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ninject;
using Serilog;
using Serilog.Events;

namespace FlowController
{
    public class Program
    {
        private readonly ILogger <Program> _logger;

        public static void Main(string[] args)
        {
            string TempPath = Directory.GetCurrentDirectory();

            string path = @"home/FlowControlDir";

            //string path = @"/Users/julianfrech/FlowControlDir";

            DirectoryInfo di = Directory.CreateDirectory(path);

            CreateHostBuilder(args).Build().Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<FlowControlRunner>();
                })
            .ConfigureLogging(logging =>
            {

                // clear default logging providers
                //logging.ClearProviders();

                // add built-in providers manually, as needed 
                logging.AddConsole();
                logging.AddDebug();
                logging.AddEventSourceLogger();
    });

    }
}
