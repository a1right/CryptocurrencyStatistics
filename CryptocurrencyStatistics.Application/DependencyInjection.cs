
using System.Net.Http;
using CryptocurrencyStatistics.Application.Interfaces;
using CryptocurrencyStatistics.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CryptocurrencyStatistics.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRecordsService(this IServiceCollection services)
        {
            services.AddScoped<IRecordsService, RecordsService>();
            return services;
        }

        public static IServiceCollection AddYobitApiService(this IServiceCollection services,
            YobitClientSettings settings)
        {
            services.AddSingleton<IYobitApiClient>(provider => new YobitApiClient(provider.GetService<IHttpClientFactory>(), settings));
            return services;
        }
    }
}
