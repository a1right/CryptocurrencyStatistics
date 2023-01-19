using CryptocurrencyStatistics.Application;
using CryptocurrencyStatistics.Application.Services;
using CryptocurrencyStatistics.DocumentOrientedStorage;
using CryptocurrencyStatistics.Relational;
using CryptocurrencyStatistics.RelationalStorage;
using CryptocurrencyStatistics.WebApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Reflection;

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

            services.AddRecordsRelationalRepository();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddHttpClient();
            var yobitSettings = new YobitApiClientSettings()
            {
                EthUsdUri = Configuration["YobitEndpoints:EthUsd:Uri"],
                BtcUsdUri = Configuration["YobitEndpoints:BtcUsd:Uri"],
                TrxUsdtUri = Configuration["YobitEndpoints:TrxUsdt:Uri"],
            };
            var mongoSettings = new RecordsMongoDbSettings()
            {
                ConnectionString = Configuration["RecordsDb:ConnectionString"],
                DatabaseName = Configuration["RecordsDb:DatabaseName"],
                RecordsCollectionName = Configuration["RecordsDb:RecordsCollectionName"],
            };
            services.AddMongoDb(mongoSettings);
            services.AddRecordsDocumentOrientedRepository();
            services.AddYobitApiService(yobitSettings);
            services.AddHostedService<RecordsUpdateService>();
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
