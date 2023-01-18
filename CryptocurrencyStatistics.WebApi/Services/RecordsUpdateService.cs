﻿using System;
using System.Threading;
using System.Threading.Tasks;
using CryptocurrencyStatistics.Application.Interfaces;
using CryptocurrencyStatistics.Domain;
using CryptocurrencyStatistics.WebApi.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CryptocurrencyStatistics.WebApi.Services
{
    public class RecordsUpdateService : BackgroundService
    {
        private readonly IYobitApiClient _yobitApiClient;
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;

        public RecordsUpdateService(IYobitApiClient yobitApiClient, IConfiguration configuration, IServiceProvider serviceProvider)
        {
            _yobitApiClient = yobitApiClient;
            _configuration = configuration;
            _serviceProvider = serviceProvider;
        }

        
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("--> Background task started");
            var updateInterval = int.Parse(_configuration["YobitUpdateInterval"]);
            while (!cancellationToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var recordsRepository = scope.ServiceProvider.GetRequiredService<IRecordsRepository>();
                    await UpdateRecords(recordsRepository, cancellationToken);
                }
                await Task.Delay(TimeSpan.FromSeconds(updateInterval));
            }
        }

        private async Task UpdateRecords(IRecordsRepository recordsRepository, CancellationToken cancellationToken)
        {
            var ethUsdRecord = await GetUpdatedEthUsdRecord(cancellationToken);
            var btcUsdRecord = await GetUpdatedBtcUsdRecord(cancellationToken);
            var trxUsdtRecord = await GetUpdatedTrxUsdtRecord(cancellationToken);
            
            await recordsRepository.CreateRecord(ethUsdRecord, cancellationToken);
            await recordsRepository.CreateRecord(btcUsdRecord, cancellationToken);
            await recordsRepository.CreateRecord(trxUsdtRecord, cancellationToken);
        }

        private async Task<Record> GetUpdatedEthUsdRecord(CancellationToken cancellationToken)
        {
            var ethUsdUpdateDto = await _yobitApiClient.GetEthUsdUpdate(cancellationToken);
            var ethUsdRecord = new Record()
            {
                PairName = _configuration["YobitEndpoints:EthUsd:Name"],
                CreatedDateTime = ethUsdUpdateDto.eth_usd.updated.ToUniversalDateTime(),
                Value = (decimal)ethUsdUpdateDto.eth_usd.last,
            };
            return ethUsdRecord;
        }
        private async Task<Record> GetUpdatedBtcUsdRecord(CancellationToken cancellationToken)
        {
            var btcUsdUpdateDto = await _yobitApiClient.GetBtcUsdUpdate(cancellationToken);
            var btcUsdRecord = new Record()
            {
                PairName = _configuration["YobitEndpoints:BtcUsd:Name"],
                CreatedDateTime = btcUsdUpdateDto.btc_usd.updated.ToUniversalDateTime(),
                Value = (decimal)btcUsdUpdateDto.btc_usd.last,
            };
            return btcUsdRecord;
        }
        private async Task<Record> GetUpdatedTrxUsdtRecord(CancellationToken cancellationToken)
        {
            var trxUsdtUpdateDto = await _yobitApiClient.GetTrxUsdtUpdate(cancellationToken);
            var trxUsdtRecord = new Record()
            {
                PairName = _configuration["YobitEndpoints:TrxUsdt:Name"],
                CreatedDateTime = trxUsdtUpdateDto.trx_usdt.updated.ToUniversalDateTime(),
                Value = (decimal)trxUsdtUpdateDto.trx_usdt.last,
            };
            return trxUsdtRecord;
        }
    }
}
