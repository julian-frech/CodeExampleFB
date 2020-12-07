using Fct_HTTP_Trigger;
using Fct_HTTP_Trigger.Controller;
using Fct_HTTP_Trigger.Data;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Configuration;

[assembly: FunctionsStartup(typeof(Startup))]
namespace Fct_HTTP_Trigger
{
    public class Startup : FunctionsStartup
    {

        public override void Configure(IFunctionsHostBuilder builder)
        {

            builder.Services.AddHttpClient();

            //A collection. Don't use this collection for the connection strings used by your function bindings.
            //    This collection is used only by frameworks that typically get connection strings from the ConnectionStrings
            //section of a configuration file, like Entity Framework. Connection strings in this object are added to the environment with
            //the provider type of System.Data.SqlClient. Items in this collection aren't published to Azure with other app settings.
            //You must explicitly add these values to the Connection strings collection of
            //your function app settings. If you're creating a SqlConnection in your function code,
            //you should store the connection string value with your other connections in Application Settings in the portal.

             builder.Services.AddDbContext<SqlDbContext>(
    options => options.UseSqlServer("Server=tcp:ThisIsNotTheAddress.database.windows.net,1433;Initial Catalog=Datenbank01;Persist Security Info=False;User ID=Password=WrongDBUser;Password=Password=WrongPWD;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False"));

            builder.Services.AddScoped<IController, MarketDataServiceController>();


        }
    }
}
