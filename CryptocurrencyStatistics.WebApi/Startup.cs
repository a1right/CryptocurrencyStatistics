using System;
using System.Net.Http;
using CryptocurrencyStatistics.Application;
using CryptocurrencyStatistics.Application.Interfaces;
using CryptocurrencyStatistics.Application.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CryptocurrencyStatistics.Relational;
using CryptocurrencyStatistics.RelationalStorage;
using CryptocurrencyStatistics.WebApi.Services;

namespace CryptocurrencyStatistics.WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IHostEnvironment Environment { get; }
        public Startup(IConfiguration configuration, IHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            if (Environment.IsProduction())
            {
                services.AddRecordsDbContext(Configuration.GetConnectionString("RecordsDbContext"));
            }
            else
            {
                services.AddRecordsDbContextInMemory();
                Console.WriteLine("--> Using InMemoryDb");
            }

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddRecordsService();
            services.AddHttpClient();
            var yobitSettings = new YobitClientSettings()
            {
                EthUsdUri = Configuration["YobitEndpoints:EthUsd:Uri"],
            };
            services.AddYobitApiService(yobitSettings);
            services.AddHostedService<StatisticsHttpDownloadService>();
        }
        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            using var scope = app.ApplicationServices.CreateScope();
            DbInitializer.Initialize(scope.ServiceProvider.GetService<RecordsDbContext>(), Environment.IsProduction());
        }
    }
}