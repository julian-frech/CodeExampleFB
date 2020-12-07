using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceBroGraphQL.Data;
using FinanceBroGraphQL.GraphQL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using HotChocolate;
using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Playground;
using FinanceBroGraphQL.Models;
using FinanceBroGraphQL.CoreServices;

namespace FinanceBroGraphQL
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

/// <summary>
/// Service Container
/// </summary>
/// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            ///Depending on the scope of the database.
            services.AddSingleton<IUserDepotService, UserDepotService>();

            ///Singleton database object can create issues if there are multiple calls to it. Need to extend the scope.
            ///Still the database is not thread save ---> Centralized saving mechanism ---> Create an incident for this
            services.AddDbContext<SqlDbContext>(options =>
                        options.UseSqlServer(Configuration.GetConnectionString("SqlDbContext")),
             ServiceLifetime.Singleton);


            services.AddGraphQLServer()
                .AddType<UserDepotType>()
                .AddType<DepotDType>()
                .AddMutationType<Mutation>()
                .AddQueryType<Query>();

            services.AddScoped<Query>();
            services.AddScoped<Mutation>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UsePlayground(new PlaygroundOptions
                {
                    QueryPath = "/api",
                    Path = "/playground"
                });
            }

            app.UseGraphQL("/api"); /// Use the new routing api ----> Really helpfull here .... --> Create incident for this as well


        }
    }
}
