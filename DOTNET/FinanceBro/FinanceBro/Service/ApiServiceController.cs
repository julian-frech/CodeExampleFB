using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using FinanceBro.Data;
using Microsoft.Extensions.Logging;

namespace FinanceBro.Service
{

    public interface IApiServiceController
    {
        public Task<Tuple<int,string>> GetApiValues(string ApiKind, Dictionary<string, string> KeyValuePairsApi) { return null; }
        private IApiService ApiRouter(string ApiKind) { return null; }
        public List<string> AvailableApiStrategies();

    }

    public class ApiServiceController : IApiServiceController
    {
        private readonly HttpClient _client;
        private readonly ILatestDatesListService _latestDatesListService;
        private readonly ILogger _logger;
        private IApiService _apiService;
        //public string[] AvailableApiStrategies = { "MarketDataService", "CalculatorService" };
        

        public List<string> AvailableApiStrategies()
        {
            return  new List<string> {
                "MarketDataService",
                "CalculatorService"};

        }


        /// <summary>
        /// Constructor for Context of strategy pattern.
        /// ApiServiceController is used to decide which concret implementation of the IApiService is requested by the client.
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="latestDatesListService"></param>
        /// <param name="logger"></param>
        /// <param name="apiService"></param>
        public ApiServiceController(HttpClient httpClient, ILatestDatesListService latestDatesListService, ILogger<ApiServiceController> logger, IApiService apiService)
        {
            this._client = httpClient;
            this._latestDatesListService = latestDatesListService;
            this._logger = logger;
            this._apiService = apiService;
        }

        /// <summary>
        /// Central method for communication with the client.
        /// </summary>
        /// <param name="_ApiKind"></param>
        /// <returns>Https Feedback or Exception message.</returns>
        public async Task<Tuple<int,string>> GetApiValues(string _ApiKind, Dictionary<string, string> keyValuePairsApi)
        {

            try
            {
                _apiService = this.ApiRouter(_ApiKind);

                return await _apiService.SendRequestToApiAsync(keyValuePairsApi);

            }
            catch (Exception e)
            {
                _logger.LogCritical(String.Concat(MethodBase.GetCurrentMethod().DeclaringType.Name, ".", MethodBase.GetCurrentMethod().Name, e.Message));
                return new Tuple<int,string>(1, String.Concat("Could not process API request. Critical error:",e.Message));
            }


        }

        /// <summary>
        /// Method to route to the correct Api strategy based on the client input _ApiKind.
        /// </summary>
        /// <param name="_ApiKind"> String input based on available Api services. Currently MarketDataService and CalculatorService.</param>
        /// <returns>IApiService with concret class stored in _apiService. </returns>
        private IApiService ApiRouter(string _ApiKind)
        {

            switch (_ApiKind)
            {
                case "MarketDataService":
                    _logger.LogInformation(String.Concat(MethodBase.GetCurrentMethod().DeclaringType.Name, ".", MethodBase.GetCurrentMethod().Name, ": Case: ", _ApiKind));
                    _apiService = new MarketDataApiService(_client, _latestDatesListService);
                    break;
                case "CalculatorService":
                    _logger.LogInformation(String.Concat(MethodBase.GetCurrentMethod().DeclaringType.Name, ".", MethodBase.GetCurrentMethod().Name, ": Case: ", _ApiKind));
                    _apiService = new testApi();
                    break;
                default:
                    _logger.LogError(String.Concat(MethodBase.GetCurrentMethod().DeclaringType.Name, ".", MethodBase.GetCurrentMethod().Name, ": Unknown Api strategy!"));
                    return null;
            }

            return _apiService;
        }

    }
}
