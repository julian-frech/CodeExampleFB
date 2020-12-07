using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FinanceBro.Data;
using Microsoft.Extensions.Logging;

namespace FinanceBro.Service
{
    //public interface IMarketDataApiService
    //{
    //    Task<string> ApiPostMethod(string ApiAddress, string JsonObject);
    //    Task<string> RouterApiMarketData(string symbol);
    //}

    public class MarketDataApiService : IApiService //IMarketDataApiService
    {

        public string JsonBody { get => "{  \"method\":\"chart\",  \"symbol\":\"_PlaceholderSymbol_\",  \"span\":\"stock\",  \"interval\":\"_PlaceholderInterval_\"}"; }
        public string Address { get => "http://marketdataservice/api/Trigger"; }

        private readonly HttpClient _client;
        private readonly ILatestDatesListService _latestDatesListService;

        //private readonly ILogger _logger;

        //public MarketDataApiService(HttpClient httpClient, ILatestDatesListService latestDatesListService, ILogger<MarketDataApiService> logger)
        //{
        //    this._client = httpClient;
        //    this._latestDatesListService = latestDatesListService;
        //    this._logger = logger;
        //}

        public MarketDataApiService(HttpClient httpClient, ILatestDatesListService latestDatesListService) {
            this._client = httpClient;
            this._latestDatesListService = latestDatesListService;
            //this._logger = logger;
        }



        public async Task<Tuple<int,string>> SendRequestToApiAsync(Dictionary<string, string> ApiDictionary)
        {

            string ApiSymbolInput = ApiDictionary.GetValueOrDefault("symbol");

            var GetData = await _latestDatesListService.Get(ApiSymbolInput);

            var MonthCeiled = (GetData is null) ? 3 : Math.Ceiling(DateTime.Today.Subtract(Convert.ToDateTime(GetData.latestDate)).TotalDays / 30);

            if (MonthCeiled > 1)
            {
                MonthCeiled = 3;
            }

            var JsonBodyForApi = JsonBody.Replace("_PlaceholderInterval_", MonthCeiled.ToString() + "M").Replace("_PlaceholderSymbol_", ApiSymbolInput);

            string feedback = await this.ApiPostMethod(Address, JsonBodyForApi);

            Console.WriteLine(feedback);

            return new Tuple<int,string>(1,feedback);

        }


        public async Task<string> ApiPostMethod(string ApiAddress, string JsonObject)
        {
            Console.WriteLine(ApiAddress);
            Console.WriteLine(JsonObject);
            var httpContent = new StringContent(JsonObject, Encoding.UTF8, "application/json");

            string myContent = await httpContent.ReadAsStringAsync();

            using var httpResponse =
            await _client.PostAsync(ApiAddress, httpContent);

            httpResponse.EnsureSuccessStatusCode();

            string jsonFeedback = await httpResponse.Content.ReadAsStringAsync();

            return jsonFeedback;
        }


    }




}
