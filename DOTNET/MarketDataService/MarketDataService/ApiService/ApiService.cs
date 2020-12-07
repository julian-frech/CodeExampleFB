using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using MarketDataService.Models;
using System.Text.Json;

namespace MarketDataService.ApiService
{
    public class ApiServiceHelper
    {

        public ApiServiceHelper()
        {

        }

        public async Task<Tuple<string, object>> PassArgumentsToApiBulk(IEXCloudBaseClass iEXCloudBaseClass, ModelClass modelClass)
        {
            string ConnectionString = StringBuilder(iEXCloudBaseClass);

            Console.WriteLine(ConnectionString);

            using (var httpClient = new HttpClient())
            {
                using var httpResponse = await httpClient.GetAsync(ConnectionString, HttpCompletionOption.ResponseHeadersRead);

                httpResponse.EnsureSuccessStatusCode(); // throws if not 200-299

                string json = await httpResponse.Content.ReadAsStringAsync();

                if (httpResponse.Content is object && httpResponse.Content.Headers.ContentType.MediaType == "application/json")
                {
                    var contentStream = await httpResponse.Content.ReadAsStreamAsync();

                    dynamic feedback = null;

                    List<IEXCloudClass> items = new List<IEXCloudClass>();

                    List<string> SingleJson = new List<string>();

                    var list = new List<string> { "company", "quote"};

                    try
                    {
                        if (list.Contains(iEXCloudBaseClass.Method))
                        {
                            feedback = await JsonSerializer.DeserializeAsync<IEXCloudClass>(contentStream, new JsonSerializerOptions { IgnoreNullValues = true, PropertyNameCaseInsensitive = true });
                            items.Add(feedback);
                        }
                        else
                        {
                            items = await JsonSerializer.DeserializeAsync<List<IEXCloudClass>>(contentStream, new JsonSerializerOptions { IgnoreNullValues = true, PropertyNameCaseInsensitive = true });

                        }

                        var FeedbackTuple = new Tuple<string, object>(json, items);


                        return FeedbackTuple;
                    }
                    catch (JsonException) // Invalid JSON
                    {
                        
                        Console.WriteLine("Invalid JSON.");
                    }
                }
                else
                {
                    Console.WriteLine("HTTP Response was invalid and cannot be deserialised.");
                }
            }
            return null;



            

        }

        private static string StringBuilder(IEXCloudBaseClass iEXCloudBaseClass)
        {
            string ConnectionString= "";

            string FirstLvl = "";
            string SecondLvl = "";
            string ThirdLvl = "";

            if (!(iEXCloudBaseClass.sourceAPI is null))
            {
                ConnectionString = $"https://financialmodelingprep.com/api/v3/historical-price-full/{iEXCloudBaseClass.Symbol}?apikey=pk_c15ea55a5b864c3faec32281aeb49116 ";
            }
            else { 
                string interval_input= (!(iEXCloudBaseClass.Interval is null)) ? "/" + iEXCloudBaseClass.Interval : ""; 

                string date_input = (!(iEXCloudBaseClass.Date is null)) ? "/date/" + iEXCloudBaseClass.Date : "";

                string news_input = (!(iEXCloudBaseClass.Last is null)) ? "/last/" + iEXCloudBaseClass.Last : "";

                string field_input = (!(iEXCloudBaseClass.Field is null)) ? "/" + iEXCloudBaseClass.Field : "";

                FirstLvl = iEXCloudBaseClass.Span;

                SecondLvl = iEXCloudBaseClass.Symbol;

                ThirdLvl = iEXCloudBaseClass.Method + interval_input + date_input + news_input + field_input;

                ConnectionString = $"https://cloud.iexapis.com/stable/{FirstLvl}/{SecondLvl}/{ThirdLvl}?token=pk_c15ea55a5b864c3faec32281aeb49116";
            }

            return ConnectionString;
        }



    }

    

}
