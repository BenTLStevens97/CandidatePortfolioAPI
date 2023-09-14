using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CandidatePortfolioAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens;

namespace CandidatePortfolioAPI
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
            //Add Response Caching Service
            services.AddResponseCaching();
            services.AddMemoryCache();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CandidatePortfolioAPI", Version = "v1" });
            });

            //include set ICosmosDBService to specified Azure Cosmos DB in appSettings

            services.AddSingleton<ICosmosDbService>(InitializeCosmosClientInstanceAsync(Configuration.GetSection("CosmosDb")).GetAwaiter().GetResult());


                        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                       .AddJwtBearer(options =>
                       {
                           options.TokenValidationParameters = new TokenValidationParameters
                           {
                               ValidateIssuer = true,
                               ValidateAudience = true,
                               ValidateLifetime = true,
                               ValidateIssuerSigningKey = true,
                               ValidIssuer = Configuration["Jwt:Issuer"],
                               ValidAudience = Configuration["Jwt:Issuer"],
                               IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                           };
                       });
            services.AddMvc();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CandidatePortfolioAPI v1"));
            }

            //Add Response Caching AND CORS POLICY TO API Runtime

            app.UseCors("CorsPolicy");
            app.UseResponseCaching();

            app.UseAuthentication();
            //app.UseMvc();

            app.UseHttpsRedirection();

            app.UseRouting();


            //add authorization to API RUNTIME
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        //Setup Connection configuration to Azure Cosmos DB
        private static async Task<CosmosDbService> InitializeCosmosClientInstanceAsync(IConfigurationSection configurationSection)
        {
            var databaseName = configurationSection["DatabaseName"];
            var containerName = configurationSection["ContainerName"];
            var account = configurationSection["Account"];
            var key = configurationSection["Key"];
            var client = new Microsoft.Azure.Cosmos.CosmosClient(account, key);
            var database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
            await database.Database.CreateContainerIfNotExistsAsync(containerName, "/id");
            var cosmosDbService = new CosmosDbService(client, databaseName, containerName);
            return cosmosDbService;
        }
    }

}

