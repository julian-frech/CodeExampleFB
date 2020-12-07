using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using DataBaseWatcher.Data;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ninject;

namespace DataBaseWatcher
{
    public class DataBaseWorkerSerivce : BackgroundService
    {
        private readonly ILogger<DataBaseWorkerSerivce> _logger;

        public DataBaseWorkerSerivce(ILogger<DataBaseWorkerSerivce> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            int counter = 0;

            IKernel kernel = new StandardKernel();

            kernel.Bind<IConnectionBuilder>().To<ConnectionBuilder1>();

            kernel.Bind<IDataFetcher>().To<DataFetcher>();

            kernel.Load(Assembly.GetExecutingAssembly());

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var feedback = kernel.Get<DataBaseWatcher>();

                    feedback.PassParameters();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Konnte Background Service nicht ausführen: {0}", e.Message);
                }

                counter++;

                Console.WriteLine("Service Times: {0}. Service bereit.", counter);

                await Task.Delay(4000, stoppingToken);

            }
        }
    }
}
