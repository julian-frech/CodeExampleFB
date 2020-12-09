using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FiBroGraphQL.CoreServices;
using FiBroGraphQL.Data;
using FiBroGraphQL.GraphQL;
using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Playground;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FiBroGraphQL
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = "https://dev-n9cmwa8n.eu.auth0.com/";
                options.Audience = "https://127.0.0.1:5001/playground/";
            });

            services.AddDbContext<SqlDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("SqlDbContext")),
 ServiceLifetime.Singleton);

            services.AddSingleton<IUserDepotService, UserDepotService>();


            services.AddGraphQLServer()
                .AddAuthorizeDirectiveType()
                .AddType<UserDepotType>()
                .AddType<DepotDType>()
                .AddMutationType<Mutation>()
                .AddQueryType<Query>();

            services.AddScoped<Query>();
            services.AddScoped<Mutation>();


            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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

            app.UseAuthentication();
            app.UseGraphQL("/api");

        }
    }
}
