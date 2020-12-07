using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using BoersenDatenService1.Interfaces;
using BoersenDatenService2.ApiDataClasses;
using BoersenDatenService2.Services;
using Newtonsoft.Json;

namespace BoersenDatenService2.ApiServices
{
    public class ApiFMPMaDa : IApi
    {

        private readonly IDataBaseService dataBaseServiceSQLServer;

        private readonly IErrorService errorService;

        public ApiFMPMaDa(IDataBaseService dataBaseServiceSQLServer, IErrorService errorService)
        {
            this.dataBaseServiceSQLServer = dataBaseServiceSQLServer;
            this.errorService = errorService;
        }

        public List<FMPStockPrices> ApiFeedback { get; set; }

        public async Task<int> PassArgumentsToApiBulk(string[] PassingArguments)
        {
            try
            {
                using (var httpClient = new HttpClient())
                
                {
                    using (var request = new HttpRequestMessage(new HttpMethod("GET"), $"https://financialmodelingprep.com/api/v3/historical-price-full/{PassingArguments[0]}"))
                    {
                        Console.WriteLine($"https://financialmodelingprep.com/api/v3/historical-price-full/{PassingArguments[0]}");

                        request.Headers.TryAddWithoutValidation("Upgrade-Insecure-Requests", "1");

                        var response = await httpClient.SendAsync(request);

                        var contents = await response.Content.ReadAsStringAsync();

                        FMPStockPrices model = JsonConvert.DeserializeObject<FMPStockPrices>(contents);

                        DataTable TestTable = this.ConvertToDatatable(model.Historical, model.Symbol);

                        int Feedback =  await dataBaseServiceSQLServer.BulkInsert(TestTable, "import.FMAHistoricalMaDa", Int32.Parse(PassingArguments[4]));

                    }
                }
                return 1;
            }
            catch(Exception e)
            {
                errorService.ErrorHandler(Int32.Parse(PassingArguments[4]), 999);

                Console.WriteLine("Hat nicht geklappt: {0}.",e.Message);

                return 2;

            }
        }

        static dynamic CastDBNull(dynamic _row)
        {
            if (_row is null)
            {
                return DBNull.Value;
            }
            else
            {
                return _row;
            }

        }



        private DataTable ConvertToDatatable(IList<Historical> __data, string _symbol)
        {

            DataTable dTb = new DataTable();

            dTb.Columns.Add("Symbol", typeof(string));
            dTb.Columns.Add("date", typeof(DateTime));
            dTb.Columns.Add("open", typeof(decimal));
            dTb.Columns.Add("high", typeof(decimal));
            dTb.Columns.Add("low", typeof(decimal));
            dTb.Columns.Add("close", typeof(decimal));
            dTb.Columns.Add("adjClose", typeof(decimal));
            dTb.Columns.Add("volume", typeof(int));
            dTb.Columns.Add("unadjustedVolume", typeof(int));
            dTb.Columns.Add("change", typeof(decimal));
            dTb.Columns.Add("changePercent", typeof(decimal));
            dTb.Columns.Add("vwap", typeof(decimal));
            dTb.Columns.Add("label", typeof(string));
            dTb.Columns.Add("changeOverTime", typeof(decimal));
            

            foreach(var _data in __data)
            {
                DataRow dr = dTb.NewRow();

                dr["Symbol"] = _symbol;
                dr["date"] = CastDBNull(_data.date);
                dr["open"] = CastDBNull(_data.open);
                dr["high"] = CastDBNull(_data.high);
                dr["low"] = CastDBNull(_data.low);
                dr["close"] = CastDBNull(_data.close);
                dr["adjclose"] = CastDBNull(_data.adjClose);
                dr["volume"] = CastDBNull(_data.volume);
                dr["unadjustedVolume"] = CastDBNull(_data.unadjustedVolume);
                dr["change"] = CastDBNull(_data.change);
                dr["changePercent"] = CastDBNull(_data.changePercent);
                dr["vwap"] = CastDBNull(_data.vwap);
                dr["label"] = CastDBNull(_data.label);
                dr["changeOverTime"] = CastDBNull(_data.changeOverTime);
                

                dTb.Rows.Add(dr);
            }
            

            return dTb;
        }

        
    }
}
