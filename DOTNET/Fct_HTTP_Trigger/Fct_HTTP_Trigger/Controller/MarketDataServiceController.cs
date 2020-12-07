using System;
using System.Net.Http;
using System.Threading.Tasks;
using Fct_HTTP_Trigger.Data;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace Fct_HTTP_Trigger.Controller
{
    public class MarketDataServiceController: IController
    {


        private readonly HttpClient _client;
        private readonly SqlDbContext _context;

        public MarketDataServiceController(HttpClient httpClient, SqlDbContext context)
        {
            this._client = httpClient;
            this._context = context;
        }

        public async Task<string> ApiPostMethod(string ApiAddress,string JsonObject)
        {
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
