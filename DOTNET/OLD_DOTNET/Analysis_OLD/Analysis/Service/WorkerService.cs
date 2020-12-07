using Analysis.Calculators;
using Analysis.Calculators.General;
using Analysis.Calculators.MovingAverage;
using Analysis.Core.Stocks;
using Analysis.Core.Time;
using Analysis.Data.DataFetcher;
using Analysis.Data.DataInterpolation;
using Analysis.Data.DataProvider;
using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;

namespace Analysis.Service
{
    /// <summary>
    /// Binding für die Dependency-Injection
    /// </summary>
    public class ServiceBinding : NinjectModule
    {
        public override void Load()
        {
            Bind<ICalculatorFactory>().To<StandardCalculatorFactory>().InSingletonScope();
            Bind<IDataProvider>().To<DataProvider>().InSingletonScope();
            Bind<IDataFetcher>().To<DatabaseFetcher>().InSingletonScope();
            Bind<IDataInterpolation>().To<LinearDataInterpolation>().InSingletonScope();
        }
    }


    public class WorkerService
    {
        static void Main()
        {
            //Bereite Dependency-Injection vor
            StandardKernel kernel = new StandardKernel(new ServiceBinding());

            IDataProvider provider = kernel.Get<IDataProvider>();
            Stock paypal = provider.GetUpdatedStock("pypl", TimeInterval.DAILY, DateTime.Now);

            ICalculatorFactory factory = kernel.Get<ICalculatorFactory>();
            ICalculatorMovingAverage calculator = factory.Create_CalculatorMovingAverage(Dependency.DayClose);
            List<ValueTimeAware<decimal>> results = calculator.Calculate(paypal);

            foreach(ValueTimeAware<decimal> result in results){
                Console.WriteLine(String.Format("MovingAverage at time {0} based on {1} is {2}",result.Timestamp, Dependency.DayClose, result.Value));
            }

        }
    }
}

