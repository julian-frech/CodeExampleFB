using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using AnalysisSignals.AnalysisDiffMethods;
using AnalysisSignals.Calculations;
using AnalysisSignals.Data;
using AnalysisSignals.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ninject;

namespace AnalysisSignals
{
    public class AnalysisWorker : BackgroundService
    {
        private readonly ILogger<AnalysisWorker> _logger;

        public AnalysisWorker(ILogger<AnalysisWorker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            string PathOfTriggerFile = "/Users/julianfrech/FlowControlDir/AnalysisTool/";

            int counter = 0;

            while (!stoppingToken.IsCancellationRequested)
            {

                if (Directory.Exists(PathOfTriggerFile))
                {
                    string storedProcedure;

                    string[] ListOfTriggerFiles = Directory.GetFiles(PathOfTriggerFile);

                    ListOfTriggerFiles = ListOfTriggerFiles.Where(x => !string.IsNullOrEmpty(x)).ToArray();

                    string exactPath = PathOfTriggerFile;

                    if (!(ListOfTriggerFiles is null))
                    {
                        foreach (var triggerfile in ListOfTriggerFiles)
                        {

                            Console.WriteLine(triggerfile);

                            try
                            {
                                string[] argumente = File.ReadLines(triggerfile).ToArray();


                                string args = File.ReadAllText(triggerfile);

                                string[] argArr = args.Split(";");

                                argArr = argArr.Where(x => !string.IsNullOrEmpty(x)).ToArray();

                                Dictionary<String, String> dicArgs = ParseCommandLineArguments(argArr);

                                string[] ParsingArgumentsForProperty = ParsingData(dicArgs);

                                File.Delete(triggerfile);

                                counter++;

                                Console.WriteLine("Triggerfile wurde abgeholt. {0} Triggerfiles bereits verarbeitet.", counter);

                                IKernel kernel = new StandardKernel();

                                kernel.Load(Assembly.GetExecutingAssembly());

                                kernel.Bind<IConnectionBuilder>().To<ConnectionBuilder1>();

                                kernel.Bind<IDataFetcher>().To<DatabaseFetcher>();

                                string tmp = "";

                                tmp = dicArgs.TryGetValue("Parameter", out tmp) ? tmp : tmp;

                                switch (tmp)
                                {
                                    case "simpleaverage":
                                        Console.WriteLine("Case Average");
                                        kernel.Bind<IAnalysisMethod>().To<AverageSimple>();
                                        break;
                                    case "rsi":
                                        Console.WriteLine("Case 2");
                                        kernel.Bind<IAnalysisMethod>().To<RSI>();
                                        break;
                                    default:
                                        Console.WriteLine("Default case empty");
                                        break;
                                }

                                try
                                {
                                    var calc = kernel.Get<Analysis>();

                                    calc.PassParameters(dicArgs);
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("Konnte Background Service nicht ausführen: {0}", e.Message);
                                }
                            }
                            catch (Exception e2)
                            {
                                Console.WriteLine("Could not read file: {0}", e2);
                            }
                        }

                        await Task.Delay(1000, stoppingToken);

                        Console.WriteLine("Service bereit. Verarbeitete Files: {0}", counter);
                    }
                    else
                    {
                        await Task.Delay(9000, stoppingToken);

                        Console.WriteLine("Service bereit. Verarbeitete Files: {0}", counter);
                    }
                }
                else
                {
                    Console.WriteLine("Kein Ordner BoersenDatenService vorhanden.");

                    await Task.Delay(10000, stoppingToken);

                    continue;
                }
            }
        }


        private static Dictionary<string, string> ParseCommandLineArguments(string[] args)
        {
            try
            {
                Dictionary<string, string> dicArgs = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

                foreach (string arg in args)
                { 
                    string temp = arg.ToString().Replace(" ","");
                    int idxEqual = arg.IndexOf('=');
                    string key = arg.Substring(0, idxEqual).Replace(" ", "");
                    string value = arg.Substring(idxEqual + 1);
                    dicArgs.Add(key.ToLower(), value.ToLower());
                }
                return dicArgs;
            }
            catch (Exception e)
            {
                Console.WriteLine("Fehler beim Parsen der Eingangsparameter:{0}", e.Message);

                return null;
            }

        }


        private static string[] ParsingData(Dictionary<String, String> _dicArgs)
        {
            string[] _parsingData = { "", "", "", "", "", "" };

            string tmp;

            _parsingData[0] = _dicArgs.TryGetValue("Parameter", out tmp) ? tmp : _parsingData[0];

            _parsingData[1] = _dicArgs.TryGetValue("Symbol", out tmp) ? tmp : _parsingData[1];

            _parsingData[2] = _dicArgs.TryGetValue("Market_Timestamp", out tmp) ? tmp : _parsingData[2];

            _parsingData[3] = _dicArgs.TryGetValue("FLOWID", out tmp) ? tmp : _parsingData[3];

            _parsingData[4] = _dicArgs.TryGetValue("Interval", out tmp) ? tmp : _parsingData[4];

            return _parsingData;
        }

    }
}
