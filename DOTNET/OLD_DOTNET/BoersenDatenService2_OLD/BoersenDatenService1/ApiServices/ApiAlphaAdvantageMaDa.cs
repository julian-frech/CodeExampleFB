using System;
using System.Collections.Generic;
using ServiceStack;
using System.Data.SqlClient;
using BoersenDatenService1.Interfaces;
using Newtonsoft.Json;
using BoersenDatenService2.ApiDataClasses;
using System.Data;
using System.Threading.Tasks;
using BoersenDatenService2.Services;

namespace BoersenDatenService2.ApiServices
{

public class ApiAlphaAdvantageMaDa : IApi
    {
        private readonly IDataBaseService dataBaseServiceSQLServer;

        private readonly IErrorService errorService;

        public ApiAlphaAdvantageMaDa(IDataBaseService dataBaseServiceSQLServer, IErrorService errorService)
        {
            this.dataBaseServiceSQLServer = dataBaseServiceSQLServer;
            this.errorService = errorService;
        }

        static string ApiKey = "c";

        static string ApiDataType = "csv";

        public List<AlphaVantageData> ApiFeedback { get; set; }

        public string[] PassedArguments { get; set; }

        public async Task<int> PassArgumentsToApiBulk(string[] PassingArguments)
        {
            try
            {
                Console.WriteLine("\n 0 ... Requesting Data via API from Alpha Advantage.");

                Console.WriteLine($"https://www.alphavantage.co/query?function={PassingArguments[3]}&symbol={PassingArguments[0]}&interval={PassingArguments[1]}&outputsize={PassingArguments[2]}&apikey={ApiKey}&datatype={ApiDataType}");

                try
                {
                    ApiFeedback = $"https://www.alphavantage.co/query?function={PassingArguments[3]}&symbol={PassingArguments[0]}&interval={PassingArguments[1]}&outputsize={PassingArguments[2]}&apikey={ApiKey}&datatype={ApiDataType}".GetStringFromUrl().FromCsv<List<AlphaVantageData>>();

                }
                catch (Exception e) 
                {
                    
                    PassingArguments[1] = (PassingArguments[1] == "1min") ? "5min" : PassingArguments[1];

                    Console.WriteLine("Adjusting interval to {0}: {1}", PassingArguments[1],e.Message);

                    try
                    {
                        Console.WriteLine("In Try Block: {0}", PassingArguments[1]);

                        Console.WriteLine($"https://www.alphavantage.co/query?function={PassingArguments[3]}&symbol={PassingArguments[0]}&interval={PassingArguments[1]}&outputsize={PassingArguments[2]}&apikey={ApiKey}&datatype={ApiDataType}");

                        ApiFeedback = $"https://www.alphavantage.co/query?function={PassingArguments[3]}&symbol={PassingArguments[0]}&interval={PassingArguments[1]}&outputsize={PassingArguments[2]}&apikey={ApiKey}&datatype={ApiDataType}".GetStringFromUrl().FromCsv<List<AlphaVantageData>>();

                        return 1;
                    }
                    catch (Exception e2)
                    {

                        PassingArguments[1] = (PassingArguments[1] == "5min") ? "60min" : PassingArguments[1];

                        Console.WriteLine("Adjusting interval to {0}: {1}", PassingArguments[1], e2.Message);

                        try
                        {
                            Console.WriteLine("In Try Block: {0}", PassingArguments[1]);

                            Console.WriteLine($"https://www.alphavantage.co/query?function={PassingArguments[3]}&symbol={PassingArguments[0]}&interval={PassingArguments[1]}&outputsize={PassingArguments[2]}&apikey={ApiKey}&datatype={ApiDataType}");

                            ApiFeedback = $"https://www.alphavantage.co/query?function={PassingArguments[3]}&symbol={PassingArguments[0]}&interval={PassingArguments[1]}&outputsize={PassingArguments[2]}&apikey={ApiKey}&datatype={ApiDataType}".GetStringFromUrl().FromCsv<List<AlphaVantageData>>();

                            return 1;
                        }
                        catch (Exception e3)
                        {

                            Console.WriteLine("Could not request from API:{0}", e3.Message);

                            errorService.ErrorHandler(Int32.Parse(PassingArguments[4]), 999);

                            return 2;

                        }
                    }
                }
                



                DataTable TestTable = ConvertToDatatable(ApiFeedback, PassingArguments[0]);

                try
                {
                    Console.WriteLine("\n Executing Database Operations");

                    Console.WriteLine("=========================================\n");

                    dataBaseServiceSQLServer.BulkInsert(TestTable, "import.S_Alpha_Advantage_MARKET_DATA", Int32.Parse(PassingArguments[4]));

                    Console.WriteLine("=========================================\n");

                    Console.WriteLine("All Actions executed successfully.");
                }
                catch (Exception e4)
                {
                    Console.WriteLine("Fail bei Database Operations: {0}.", e4.Message);

                    errorService.ErrorHandler(Int32.Parse(PassingArguments[4]), 999);

                    return 2;
                }
                return 1;
            }

            catch(Exception e5)
            {
                Console.WriteLine("Failed requesting Data from AlphaAdvantage: {0}", e5.Message);

                errorService.ErrorHandler(Int32.Parse(PassingArguments[4]), 999);

                return 2;
            }

        }
    
        private static DataTable ConvertToDatatable(List<AlphaVantageData> list, string symbol)
        {
            foreach (var item in list)
            {
                item.Symbol = (item.Symbol is null) ? symbol : item.Symbol;
            }

            string json = JsonConvert.SerializeObject(list);

            DataTable pDt = JsonConvert.DeserializeObject<DataTable>(json);

            return pDt;
        }


    }
}
