using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using Fct_HTTP_Trigger.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Fct_HTTP_Trigger.Model;
using Fct_HTTP_Trigger.Controller;
using System.Net.Http.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Fct_HTTP_Trigger
{
    public class Fct_HTTP_Trigger
    {
        private readonly HttpClient _client;
        private readonly SqlDbContext _context;
        private readonly IController _controller;

        public Fct_HTTP_Trigger(HttpClient httpClient, SqlDbContext context, IController controller)
        {
            this._client = httpClient;
            this._context = context;
            this._controller = controller;

        }

        /// <summary>
        /// This function is a timer that runs every cron formatted time.
        /// The method Selects all rows inserted in F_API_TRIGGERS and executes the incorporated views that resemble
        /// api calls supposed to be executed.
        /// Those calls are gathered and then executed via a controller method: ApiPostMethod
        /// </summary>
        /// <param name="myTimer"></param>
        /// <param name="log"></param>
        [FunctionName("Fct_Time_Trigger")]
        public async Task RunTimer(
            [TimerTrigger("5 * * * * *")] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            var feedback = await this.ManageContext(log);

        }



        /// <summary>
        /// Azure function to be triggered via http post method.
        /// The method Selects all rows inserted in F_API_TRIGGERS and executes the incorporated views that resemble
        /// api calls supposed to be executed.
        /// Those calls are gathered and then executed via a controller method: ApiPostMethod
        /// </summary>
        /// <param name="req">Takes the httprequest instance</param>
        /// <param name="log">Takes the ILogger instance</param>
        /// <returns>Return if the http call was successfull.</returns>
        [FunctionName("MyHttpTrigger")]
        public async Task<IActionResult> RunHttp(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            dynamic data = JsonConvert.DeserializeObject(requestBody);

            var Feedback = await this.ManageContext(log);

            return new OkObjectResult(Feedback);
        }

        public async Task<List<Tuple<string, string>>> ManageContext(ILogger log)
        {

            double SuccessRate = 1.0;
            int NumberOfCalls = 0;
            int NumberSuccess = 0;

            List<Tuple<string, string>> Feedback = new List<Tuple<string, string>>();

            var ApiTriggers = _context.API_TRIGGERS
                .ToList();


            foreach (var _triggerType in ApiTriggers)
            {
                string TriggerView = "Select * from " + _triggerType.ApiView;

                var ApiCalls = _context.API_CALLS.FromSqlRaw(TriggerView)
                        .ToList<API_CALL>();

                NumberOfCalls = ApiCalls.Count();
                NumberSuccess = 0;


                foreach (var _apiCall in ApiCalls)
                {
                    try
                    {
                        await _controller.ApiPostMethod(_triggerType.ApiAddress, _apiCall.API_CALLS);
                        ++NumberSuccess;
                    }
                    catch (Exception exc)
                    {
                        log.LogInformation("Exception:" + exc);
                    }

                }

                string tmp = "Success rate= " + NumberSuccess.ToString() + "/" + NumberOfCalls.ToString();

                Feedback.Add(new Tuple<string, string>(TriggerView, tmp));

                SuccessRate = SuccessRate * NumberSuccess / NumberOfCalls;

                string StoredProcedure = "Execute " + _triggerType.TransferSP;

                _context.Database.ExecuteSqlCommandAsync(
                       StoredProcedure
                       );

                log.LogInformation("HTTP trigger function Successrate = {0}", Feedback);
            }

            return Feedback;
        }



    }
}
