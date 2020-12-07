using System;
using System.Reflection;
using Ninject;
using BoersenDatenService1.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using FlowController.Interfaces;
using FlowController.Data;

namespace FlowController
{
    public class FlowControlRunner : BackgroundService
    {
        private readonly ILogger<FlowControlRunner> _logger;

        public FlowControlRunner(ILogger<FlowControlRunner> logger)
        {
            _logger = logger;
            
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            
            int counter = 0;

            IKernel kernel = new StandardKernel();

            kernel.Bind<IConnectionBuilder>().To<ConnectionBuilder1>();

            kernel.Bind<IDataFetcher>().To<DatabaseFetcher>();

            kernel.Load(Assembly.GetExecutingAssembly());

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var feedback = kernel.Get<TriggerFileWriter>();

                    feedback.PassParameters();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Konnte Background Service nicht ausführen: {0}", e.Message);
                }

                counter++;

                Console.WriteLine("Service Times: {0}. Service bereit.",counter);

                await Task.Delay(4000, stoppingToken);

            }
        }
    }

}
