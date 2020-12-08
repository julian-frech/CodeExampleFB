using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OptionsPatternWorker.Services;

namespace OptionsPatternWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IFileProcessor _fileProcessor;

        public Worker(ILogger<Worker> logger, IFileProcessor fileWatcher)
        {
            _logger = logger;
            _fileProcessor = fileWatcher;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Worker started at: {time}", DateTimeOffset.Now);

            var InitialCsvCreated = _fileProcessor.WriteFile();

            _logger.LogInformation("Initial file created.");

            while (!stoppingToken.IsCancellationRequested)
            {
                
                var watchedCsv = _fileProcessor.ReadAppSettingsRead();

                string curFile = string.Concat(watchedCsv.FileLocation, watchedCsv.FileName);

                _logger.LogInformation(string.Concat("Configured csv file: " , curFile));

                bool Existence = (File.Exists(curFile));

                if (Existence)
                {
                    try { 
                    _logger.LogInformation("Trigger file available");
                    var feedback = _fileProcessor.ReadFile(curFile);
                    _logger.LogInformation(feedback);
                    await Task.Delay(5000, stoppingToken);
                    }
                    catch(Exception e)
                    {
                        _logger.LogCritical(String.Concat(MethodBase.GetCurrentMethod().DeclaringType.Name, ".", MethodBase.GetCurrentMethod().Name, ": Exception while reading csv file: ", e.Message));
                        await Task.Delay(120000, stoppingToken);
                    }
                }
                else
                {
                    _logger.LogInformation(string.Concat("Triggerfile available: ", Existence, " Sleeping for 1 Minute."));
                    _logger.LogInformation(string.Concat("----->>>>>>Adjust filename and file location in appsettings.json at location: ", Directory.GetCurrentDirectory(),"<<<<<<-----"));
                    await Task.Delay(60000, stoppingToken);
                }

            }
        }
    }
}
