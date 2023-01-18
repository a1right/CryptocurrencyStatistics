
using System.Net.Http;
using CryptocurrencyStatistics.Application.Interfaces;
using CryptocurrencyStatistics.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CryptocurrencyStatistics.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddYobitApiService(this IServiceCollection services,
            YobitApiClientSettings settings)
        {
            services.AddSingleton<IYobitApiClient>(provider => new YobitApiClient(provider.GetService<IHttpClientFactory>(), settings));
            return services;
        }
    }
}
