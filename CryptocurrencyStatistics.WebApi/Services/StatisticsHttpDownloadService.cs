using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CryptocurrencyStatistics.Application.Interfaces;
using CryptocurrencyStatistics.Domain;
using CryptocurrencyStatistics.WebApi.Dtos;
using CryptocurrencyStatistics.WebApi.Models;
using CryptocurrencyStatistics.WebApi.Services.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace CryptocurrencyStatistics.WebApi.Services
{
    public class StatisticsHttpDownloadService : BackgroundService
    {
        private readonly IHttpwoopadoopa _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IRecordsService _recordsService;
        private DateTime _nextUpdateTime;

        public StatisticsHttpDownloadService(IHttpwoopadoopa httpClient, IConfiguration configuration, IMapper mapper, IRecordsService recordsService)
        {
            _httpClient = httpClient;
            _httpClient = httpClient;
            _configuration = configuration;
            _mapper = mapper;
            _recordsService = recordsService;
        }

        
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("--> Background task started");
            _nextUpdateTime = DateTime.Now;
            while (stoppingToken.IsCancellationRequested)
            {
                if (DateTime.Now >= _nextUpdateTime)
                {
                    _httpClient.GetEthUsdUpdate();
                    _nextUpdateTime += TimeSpan.FromMinutes(1);
                }
            }
            return Task.CompletedTask;
        }
    }
}
