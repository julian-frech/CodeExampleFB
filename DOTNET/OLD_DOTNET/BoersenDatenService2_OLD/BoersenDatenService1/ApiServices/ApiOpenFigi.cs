using System;
using BoersenDatenService1.Interfaces;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using BoersenDatenService2.ApiDataClasses;

namespace BoersenDatenService2.ApiServices
{
    public class ApiOpenFigi : IApi
    {

        public async Task<int> PassArgumentsToApiBulk(string[] PassingArguments)
        {

            string Isin = PassingArguments[4];
            try
            {
                Console.WriteLine("\n 0 ... Requesting Data via API from OpenFigi.");

                ApiFeedbackGenericList datalist = await HttpClientRequestOpenFigi(Isin);

                try
                {
                    Console.WriteLine("\n Executing Database Operations");

                    Console.WriteLine("=========================================\n");

                    int DatabaseOperations_Succes = DatabaseOperations_Success(datalist, Isin);

                    Console.WriteLine("=========================================\n");

                    Console.WriteLine("All Actions executed successfully.");

                }
                catch (Exception e)
                {
                    Console.WriteLine("Fail bei Database Operations: {0}.", e.Message);
                }

                Console.WriteLine("Open Figi Request Finished.");

                return 1;
            }
            catch (Exception e)
            {
                Console.WriteLine("Konnte die Abfrage nicht zusammenstellen: {0}.", e.Message);
                return 2;
            } 

        }

        private async Task<ApiFeedbackGenericList> HttpClientRequestOpenFigi(string Isin)
        {
            var httpClient = new HttpClient();
            var request = new HttpRequestMessage(new HttpMethod("POST"), "https://api.openfigi.com/v2/mapping");
            string HttpString = String.Concat("[{\"idType\":\"ID_ISIN\", \"idValue\":\"", Isin.ToUpper(), "\"}]");
            request.Content = new StringContent(HttpString);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var responseString = await httpClient.SendAsync(request);

            var contents = await responseString.Content.ReadAsStringAsync();
            contents = contents.Substring(+1, contents.Length - 2);

            ApiFeedbackGenericList datalist = JsonConvert.DeserializeObject<ApiFeedbackGenericList>(contents);
            return datalist;
        }

        private int DatabaseOperations_Success(ApiFeedbackGenericList DataList, string Isin)
        {

            SqlConnectionStringBuilder ConBuilder = new SqlConnectionStringBuilder
            {
                DataSource = "teq92.database.windows.net",
                UserID = "Developer",
                Password = "WeCreateCoolFeatures2020WithBugs",
                InitialCatalog = "Datenbank01"
            };

            using (SqlConnection connection = new SqlConnection(ConBuilder.ConnectionString))
            {

                connection.Open();

                Console.WriteLine("\n 1 ... Data Transfer into Import Schema.");

                foreach (var item in DataList.data)
                {
                    try
                    {
                        string sql = "Insert into [import].[S_SYMBOL_REFERENCE_DATA_FIGI] (isin,figi,name,ticker,exchCode,compositeFIGI,uniqueID,securityType,marketSector,shareClassFIGI,securityType2,securityDescription,ITS,UTS) Values(@isin,@figi,@name,@ticker,@exchCode,@compositeFIGI,@uniqueID,@securityType,@marketSector,@shareClassFIGI,@securityType2,@securityDescription,@ITS,@UTS)";
                        SqlCommand insert = new SqlCommand(sql, connection);

                        insert.CommandType = System.Data.CommandType.Text;
                        insert.Parameters.AddWithValue("@isin", Isin);
                        insert.Parameters.AddWithValue("@figi", item.figi);
                        insert.Parameters.AddWithValue("@name", item.name);
                        insert.Parameters.AddWithValue("@ticker", item.ticker);
                        insert.Parameters.AddWithValue("@exchCode", item.exchCode);
                        insert.Parameters.AddWithValue("@compositeFIGI", item.compositeFIGI);
                        insert.Parameters.AddWithValue("@uniqueID", item.uniqueID);
                        insert.Parameters.AddWithValue("@securityType", item.securityType);
                        insert.Parameters.AddWithValue("@marketSector", item.marketSector);
                        insert.Parameters.AddWithValue("@shareClassFIGI", item.shareClassFIGI);
                        //insert.Parameters.AddWithValue("@uniqueIDFutOpt", item.uniqueIDFutOpt);
                        insert.Parameters.AddWithValue("@securityType2", item.securityType2);
                        insert.Parameters.AddWithValue("@securityDescription", item.securityDescription);
                        insert.Parameters.AddWithValue("@ITS", DateTime.Now);
                        insert.Parameters.AddWithValue("@UTS", DateTime.Now);
                        insert.ExecuteNonQuery();

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Skipped one insert{0}", e.Message);
                        continue;
                    }
                }

                Console.WriteLine("\n 2 ... Data Transformation into Production Schema.");

                SqlCommand TransferData = new SqlCommand("execute [import].[S_Open_Figi_To_Prod]", connection);

                TransferData.ExecuteNonQuery();

                connection.Close();

            }

            int _success = 1;

            return _success;
        }
    }
}
