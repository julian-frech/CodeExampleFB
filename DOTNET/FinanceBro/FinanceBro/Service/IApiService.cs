using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinanceBro.Service
{
    public interface IApiService
    {
        public string JsonBody { get; }
        public string Address { get; }
        public async Task<Tuple<int,string>> SendRequestToApiAsync(Dictionary<string, string> ApiDictionary) { return null; }

    }

    public class testApi : IApiService
    {
        public string JsonBody { get => ""; }
        public string Address { get => ""; }

        async Task<Tuple<int,string>> SendRequestToApiAsync(Dictionary<string, string> ApiDictionary) { return null; }
    }


    //public class MarketDataServiceApi : IApiService
    //{
    //    public string JsonBody { get => "{  \"method\":\"chart\",  \"symbol\":\"_PlaceholderSymbol_\",  \"span\":\"stock\",  \"interval\":\"_PlaceholderInterval_\"}"; }
    //    public string Address { get => "http://marketdataservice/api/Trigger"; }

    //    async Task<string> SendRequestToApiAsync() { return null; }
    //}


    public class CalculatorServiceApi : IApiService
    {
        public string JsonBody { get => "{  \"method\":\"chart\",  \"symbol\":\"_PlaceholderSymbol_\",  \"span\":\"stock\",  \"interval\":\"_PlaceholderInterval_\"}"; }
        public string Address { get => "http://calculatorservice/api/Analysis"; }

        async Task<Tuple<int,string>> SendRequestToApiAsync(Dictionary<string, string> ApiDictionary) { return null; }
    }




}
