using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ApiWrapper.Services;
using System.IO;

namespace ApiWrapper
{
    public class ApiWorker : BackgroundService
    {
        private readonly ILogger<ApiWorker> _logger;
        private readonly IWrapper _wrapper;
        private readonly IFileProcessor _fileProcessor;

        public ApiWorker(ILogger<ApiWorker> logger, IWrapper wrapper, IFileProcessor fileWatcher)
        {
            _logger = logger;
            _wrapper = wrapper;
            _fileProcessor = fileWatcher;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            
            
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                var watchedCsv = _fileProcessor.ReadAppSettings();
                _logger.LogInformation("Configured csv file:{file}", watchedCsv);

                string curFile = string.Concat(watchedCsv.FileLocation, watchedCsv.FileName);
                
                bool Existence = (File.Exists(curFile));

                if (Existence)
                {
                    _logger.LogInformation("Trigger file available");
                    var feedback = _fileProcessor.ReadFile(curFile);
                    _logger.LogInformation(feedback);
                    await Task.Delay(5000, stoppingToken);
                }
                else
                {
                    _logger.LogInformation(string.Concat("Triggerfile available: ", Existence, " Sleeping for 1 Minute."));
                    await Task.Delay(60000, stoppingToken);
                }  

                


            }
        }
    }
}
