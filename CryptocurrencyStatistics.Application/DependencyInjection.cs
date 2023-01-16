using System;
using System.Collections.Generic;
using System.Text;
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
    }
}
