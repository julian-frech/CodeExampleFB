using System;
using System.Reflection;
using Ninject;
using BoersenDatenService1.Interfaces;
using BoersenDatenService1.ApiProperties;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using BoersenDatenService2.ApiServices;
using BoersenDatenService2.ApiProperties;
using BoersenDatenService2.Services;

namespace BoersenDatenService1
{
    public class NewWorkerService : BackgroundService
    {
        private readonly ILogger<NewWorkerService> _logger;

        public NewWorkerService(ILogger<NewWorkerService> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //string PathOfTriggerFile = "/Users/julianfrech/FlowControlDir/BoersenDatenService/";

            
            string PathOfTriggerFile = "home/FlowControlDir" + "/BoersenDatenService/";

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

                                kernel.Bind<IDataBaseService>().To<DataBaseServiceSQLServer>();

                                kernel.Bind<IErrorService>().To<ErrorService>();

                                if (dicArgs["apitype"] == "alphaadvantagemada")
                                {
                                    kernel.Bind<IProperties>().To<PropertyApiAlphAdv2>();

                                    kernel.Bind<IApi>().To<ApiAlphaAdvantageMaDa>();

                                    storedProcedure = "[import].[S_TRANSFORM_ALPHA_ADVANTAGE_MaDa]";

                                }
                                else if (dicArgs["apitype"] == "financialmodelingprep")
                                {
                                    kernel.Bind<IProperties>().To<Propertyfinancialmodelingprep>();

                                    kernel.Bind<IApi>().To<Apifinancialmodelingprep>();

                                    storedProcedure = "[import].[S_Transform_SymbolData]";
                                }
                                else if (dicArgs["apitype"] == "fmpmada")
                                {
                                    kernel.Bind<IProperties>().To<Propertyfinancialmodelingprep>();

                                    kernel.Bind<IApi>().To<ApiFMPMaDa>();

                                    storedProcedure = "[import].[S_TRANSFORM_FMP_MaDa]";
                                }
                                else if (dicArgs["apitype"] == "alphaadvantageca")
                                {
                                    kernel.Bind<IProperties>().To<PropertyApiKursDaten>();

                                    kernel.Bind<IApi>().To<ApiAlphaAdvantageCA>();

                                    storedProcedure = "[import].[S_TRANSFORM_ALPHA_ADVANTAGE_CA]";
                                }
                                else if (dicArgs["apitype"] == "openfigi")
                                {
                                    kernel.Bind<IProperties>().To<PropertyApiKursDaten>();

                                    kernel.Bind<IApi>().To<ApiOpenFigi>();

                                    storedProcedure = "";

                                }
                                else
                                {
                                    continue;
                                }

                                try
                                {
                                    var feedback = kernel.Get<BoersenDatenService>();

                                    int Feedback = await feedback.PassParameters(ParsingArgumentsForProperty, storedProcedure);

                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("Konnte Background Service nicht ausführen: {0}", e.Message);
                                }
                            }catch(Exception e2)
                            {
                                Console.WriteLine("Could not read file: {0}", e2);
                            }
                        }

                        

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

                foreach (var arg in args)
                {
                    string temp = arg.ToString();
                    int idxEqual = arg.IndexOf('=');
                    string key = arg.Substring(0, idxEqual);
                    string value = arg.Substring(idxEqual + 1);
                    dicArgs.Add(key.ToLower(), value.ToLower());
                }
                return dicArgs;
            }
            catch(Exception e)
            {
                Console.WriteLine("Fehler beim Parsen der Eingangsparameter:{0}", e.Message);

                return null;
            }
            
        }

        private static string[] ParsingData(Dictionary<String, String> _dicArgs)
        {
            string[] _parsingData = {"","","","","","" };

            string tmp;

            _parsingData[0] = _dicArgs.TryGetValue("identifier", out tmp) ? tmp : _parsingData[0];

            _parsingData[1] = _dicArgs.TryGetValue("interval", out tmp) ? tmp : _parsingData[1];

            _parsingData[2] = _dicArgs.TryGetValue("outputsize", out tmp) ? tmp : _parsingData[2];

            _parsingData[3] = _dicArgs.TryGetValue("function", out tmp) ? tmp : _parsingData[3];

            _parsingData[4] = _dicArgs.TryGetValue("flowid", out tmp) ? tmp : _parsingData[4];

            return _parsingData;
        }
    }

}
