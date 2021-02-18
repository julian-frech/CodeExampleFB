using System;
using DataOperator.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using ReportService.ConfigurationLogic;
using ReportService.QueueService;
using ReportWriter;
using ReportWriter.ConfigurationOption;
using ReportWriter.Service;
using Serilog;
using Serilog.Sinks.MSSqlServer;

namespace ReportService
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var _logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

            try
            {
                _logger.Info("Starting up the service");

                CreateHostBuilder(args).Build().Run();

            }
            catch (Exception ex)
            {
                _logger.Fatal(ex, "There was a problem starting the serivce");
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }

            
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, hostConfig) =>
            {
                hostConfig.AddEnvironmentVariables();

                Console.WriteLine(context.HostingEnvironment.EnvironmentName);

                if (context.HostingEnvironment.IsDevelopment())
                {
                    hostConfig.AddUserSecrets<Program>();
                }
            })

                .ConfigureServices((hostConfig, services) =>
                {

                    var configuration = hostConfig.Configuration;

                    services.Configure<AzureQueueConfig>(configuration.GetSection(
                                        AzureQueueConfig.AzureQueueConfigSection));

                    services.Configure<CloudFileWriterConfig>(configuration.GetSection(
                                        CloudFileWriterConfig.CloudFileWriterConfigSection));

                    services.Configure<FileWriterConfig>(configuration.GetSection(
                                        FileWriterConfig.FileWriterConfigSection));


                    services.AddDbContext<BaseDbContext>(options =>
                options.UseSqlServer(configuration["ConnectionString:SqlDatabaseContext"], a => a.MigrationsAssembly("ReportService")),
                    ServiceLifetime.Singleton);

                    services.AddHostedService<ReportServiceHosted>();

                    services.AddTransient<IFileWriter, FileWriter/*CloudFileWriter*/>();

                    services.AddTransient<IReportFile, ReportFile>();

                    services.AddTransient<IReportsQueue, ReportsQueue>();

                    services.AddTransient<ISqlExecuter, SqlExecuter>();

                    services.AddApplicationInsightsTelemetryWorkerService();
                })
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(LogLevel.Information);
                    logging.AddConsole();
                })
                    .UseNLog();


    }
}
