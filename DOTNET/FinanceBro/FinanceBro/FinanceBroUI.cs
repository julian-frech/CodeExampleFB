using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace FinanceBro
{
    public class FinanceBroUI
    {
        public static void Main(string[] args)
        {
            //string pathToContentRoot = "/bin/Release/netcoreapp3.1/publish/";
            //NLog.LogManager.LogFactory.SetCandidateConfigFilePaths(new List<string> { $"{Path.Combine(pathToContentRoot, "nlog.config")}" });


            //var logger = NLog.Web.NLogBuilder.ConfigureNLog($"{Path.Combine(pathToContentRoot, "nlog.config")}").GetCurrentClassLogger();

            var logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();



            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzQ0NjMwQDMxMzgyZTMzMmUzMEJBZHJmRGpEemhtMS95YmdFQ2JNVW1qRytDMXdCUHBCbkp5Z0JXWUIzckU9;MzQ0NjMxQDMxMzgyZTMzMmUzMGNqUU9mcEE1WlR4aktKR1pHWXJsOXF1RUZwRTkzeHd0bEtiMTQvK3B3Zzg9;MzQ0NjMyQDMxMzgyZTMzMmUzMFF1ejR1Q1FNaWp1aFlzMzlTWjhJcnYwK1EyVkVNWHU4ckRUaTZKVk5FeWM9;MzQ0NjMzQDMxMzgyZTMzMmUzME9vRGR1QVQvTGQzdGhlZnFMazdsM1pHTUh2d1BRVmNrSXZrRlRxcEg0MHc9;MzQ0NjM0QDMxMzgyZTMzMmUzMFJRZm1xL3EvS2U0aUVSNksybGxjdzFEOTZBQksvSWI5eWs1RHlTclRZd1k9;MzQ0NjM1QDMxMzgyZTMzMmUzMEpyNzJoV0dqQjBvWng5ZnJscW1NbVhXVExudE9vcGx5WHpMTkREMVNzYVE9;MzQ0NjM2QDMxMzgyZTMzMmUzMFNEUWdPTzFrSHAzeGpWWHdlNHpOOW9lQyt5Um5CY2RqTVVJb3c2THU0aGs9;MzQ0NjM3QDMxMzgyZTMzMmUzMEIwUDBPU25ORWx1TGh4VTQvSkNySWFPNWpTelQrNVVwT0puamFEemV2c2s9;MzQ0NjM4QDMxMzgyZTMzMmUzMG9MWndjOEQ3ZFFUM2RQTUkwMk9zVTh5Q05YZE8vU3pldTJBQTJlM3h4L0E9;MzQ0NjM5QDMxMzgyZTMzMmUzMENXZ0swN2RqWDk5QUlpVUhHMkZTOVJYSm9yNzhaSDFHRHR6TThjM2dQWVU9");
            CreateHostBuilder(args).Build().Run();

            //logger.Debug("FinanceBro initialized by Main().");
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();

                logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
            })
      .UseNLog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>(); 
                });
        
    }
}
