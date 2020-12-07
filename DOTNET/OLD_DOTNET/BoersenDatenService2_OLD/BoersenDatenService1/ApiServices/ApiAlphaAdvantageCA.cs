using System;
using System.Collections.Generic;
using ServiceStack;
using System.Data.SqlClient;
using BoersenDatenService1.Interfaces;
using BoersenDatenService2.ApiDataClasses;
using System.Data;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Threading.Tasks;
using BoersenDatenService2.Services;

namespace BoersenDatenService2.ApiServices
{

    public class ApiAlphaAdvantageCA : IApi
    {
        private readonly IDataBaseService dataBaseServiceSQLServer;

        public ApiAlphaAdvantageCA(IDataBaseService dataBaseServiceSQLServer)
        {
            this.dataBaseServiceSQLServer = dataBaseServiceSQLServer;
        }

        static string ApiKey = "4Z493J5FMQ1HB8FU";

        static string ApiDataType = "csv";

        public List<AlphaVantageData> ApiFeedback { get; set; }

        public string[] PassedArguments { get; set; }

        public async Task<int> PassArgumentsToApiBulk(string[] PassingArguments)
        {
            
            try
            {
                Console.WriteLine("\n 0 ... Requesting Data via API from Alpha Advantage.");
                ApiFeedback = $"https://www.alphavantage.co/query?function=TIME_SERIES_DAILY_ADJUSTED&symbol={PassingArguments[0]}&interval={PassingArguments[1]}&outputsize={PassingArguments[2]}&apikey={ApiKey}&datatype={ApiDataType}".GetStringFromUrl().FromCsv<List<AlphaVantageData>>();

                foreach (var item in ApiFeedback)
                {
                    item.Symbol = PassingArguments[0];
                }


                DataTable TestTable = ConvertToDatatable(ApiFeedback);

                try
                {
                    Console.WriteLine("\n Executing Database Operations");

                    Console.WriteLine("=========================================\n");

                    dataBaseServiceSQLServer.BulkInsert(TestTable, "import.S_Alpha_Advantage_CA", Int32.Parse(PassingArguments[4]));

                    Console.WriteLine("=========================================\n");

                    Console.WriteLine("All Actions executed successfully.");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Fail bei Database Operations: {0}.", e.Message);
                }

                return 1;
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed requesting Data from AlphaAdvantage: {0}", e.Message);
                return 2;
            }

        }

        private static DataTable ConvertToDatatable(List<AlphaVantageData> list)
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(list);

            DataTable pDt = JsonConvert.DeserializeObject<DataTable>(json);

            return pDt;
        }

    }
}
