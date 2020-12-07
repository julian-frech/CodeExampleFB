using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BoersenDatenService1.Interfaces;
using BoersenDatenService2.ApiDataClasses;
using BoersenDatenService2.Services;
using Newtonsoft.Json;

namespace BoersenDatenService2.ApiServices
{
    public class Apifinancialmodelingprep : IApi
    {

        private readonly IDataBaseService dataBaseServiceSQLServer;

        private readonly IErrorService errorService;

        public Apifinancialmodelingprep(IDataBaseService dataBaseServiceSQLServer, IErrorService errorService)
        {
            this.dataBaseServiceSQLServer = dataBaseServiceSQLServer;
            this.errorService = errorService;
        }

        public List<financialmodelingprep> ApiFeedback { get; set; }

        public async Task<int> PassArgumentsToApiBulk(string[] PassingArguments)
        {
            try
            {
                using (var httpClient = new HttpClient())

                {
                    using (var request = new HttpRequestMessage(new HttpMethod("GET"), $"https://financialmodelingprep.com/api/v3/company/profile/{PassingArguments[0]}"))
                    {
                        Console.WriteLine($"https://financialmodelingprep.com/api/v3/company/profile/{PassingArguments[0]}");

                        request.Headers.TryAddWithoutValidation("Upgrade-Insecure-Requests", "1");

                        var response = await httpClient.SendAsync(request);

                        var contents = await response.Content.ReadAsStringAsync();

                        financialmodelingprep model = JsonConvert.DeserializeObject<financialmodelingprep>(contents);

                        DataTable TestTable = ConvertToDatatable(model.Profile, model.Symbol);

                        dataBaseServiceSQLServer.BulkInsert(TestTable, "import.Financialmodelingprep", Int32.Parse(PassingArguments[4]));
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



        private static DataTable ConvertToDatatable(Profile _data, string _symbol)
        {

            DataTable dTb = new DataTable();

            dTb.Columns.Add("Price", typeof(decimal));
            dTb.Columns.Add("Beta", typeof(string));
            dTb.Columns.Add("VolAvg", typeof(decimal));
            dTb.Columns.Add("MktCap", typeof(string));
            dTb.Columns.Add("LastDiv", typeof(decimal));
            dTb.Columns.Add("Range", typeof(string));
            dTb.Columns.Add("Changes", typeof(decimal));
            dTb.Columns.Add("Image", typeof(string));
            dTb.Columns.Add("Sector", typeof(string));
            dTb.Columns.Add("Ceo", typeof(string));
            dTb.Columns.Add("Description", typeof(string));
            dTb.Columns.Add("Website", typeof(string));
            dTb.Columns.Add("Industry", typeof(string));
            dTb.Columns.Add("CompanyName", typeof(string));
            dTb.Columns.Add("ChangesPercentage", typeof(string));
            dTb.Columns.Add("Symbol", typeof(string));

            DataRow dr = dTb.NewRow();

            dr["Price"] = CastDBNull(_data.Price);
            dr["Beta"] =CastDBNull(_data.Beta);
            dr["VolAvg"] =CastDBNull(_data.VolAvg);
            dr["LastDiv"] =CastDBNull(_data.LastDiv);
            dr["MktCap"] =CastDBNull(_data.MktCap);
            dr["Range"] =CastDBNull(_data.Range);
            dr["Changes"] =CastDBNull(_data.Changes);
            dr["Image"] =CastDBNull(_data.Image);
            dr["Sector"] = CastDBNull(_data.Sector);
            dr["Ceo"] =CastDBNull(_data.Ceo);
            dr["Description"] =CastDBNull(_data.Description);
            dr["Website"] =CastDBNull(_data.Website);
            dr["Industry"] =CastDBNull(_data.Industry);
            dr["CompanyName"] =CastDBNull(_data.CompanyName);
            dr["ChangesPercentage"] =CastDBNull(_data.ChangesPercentage);
            dr["Symbol"] = _symbol;

            dTb.Rows.Add(dr);

            return dTb;
        }

        
    }
}
