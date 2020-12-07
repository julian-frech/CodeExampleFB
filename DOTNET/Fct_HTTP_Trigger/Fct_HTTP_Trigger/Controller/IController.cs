using System;
using System.Net.Http;
using System.Threading.Tasks;
using Fct_HTTP_Trigger.Data;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace Fct_HTTP_Trigger.Controller
{
    public interface IController
    {


        public Task<string> ApiPostMethod(string ApiAddress, string JsonObject);
        

    }
}
