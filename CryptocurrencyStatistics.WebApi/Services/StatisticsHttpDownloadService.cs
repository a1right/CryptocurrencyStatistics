﻿using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CryptocurrencyStatistics.Application.Interfaces;
using CryptocurrencyStatistics.Domain;
using CryptocurrencyStatistics.WebApi.Dtos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CryptocurrencyStatistics.WebApi.Services
{
    public class StatisticsHttpDownloadService : BackgroundService
    {
        private readonly IYobitApiClient _yobitApiClient;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IServiceProvider _serviceProvider;

        public StatisticsHttpDownloadService(IYobitApiClient yobitApiClient, IConfiguration configuration, 
            IMapper mapper, IServiceProvider serviceProvider)
        {
            _yobitApiClient = yobitApiClient;
            _configuration = configuration;
            _mapper = mapper;
            _serviceProvider = serviceProvider;
        }

        
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("--> Background task started");
            while (!cancellationToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var recordsService = scope.ServiceProvider.GetRequiredService<IRecordsService>();
                    var ethUsdUpdateDto = await _yobitApiClient.GetEthUsdUpdate(cancellationToken);
                    var record = _mapper.Map<Record>(ethUsdUpdateDto);
                    record.PairName = _configuration["YobitEndpoints:EthUsd:Name"];
                    await recordsService.CreateRecord(record, cancellationToken);
                }
                await Task.Delay(TimeSpan.FromSeconds(2));
            }
        }
    }
}
