using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using CryptocurrencyStatistics.Application.Dtos;
using CryptocurrencyStatistics.Application.Interfaces;

namespace CryptocurrencyStatistics.Application.Services
{
    public class YobitApiClient : IYobitApiClient

    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly YobitClientSettings _settings;
        private readonly HttpClient _httpClient;


        public YobitApiClient(IHttpClientFactory httpClientFactory, YobitClientSettings settings)
        {
            _httpClientFactory = httpClientFactory;
            _settings = settings;
            _httpClient = _httpClientFactory.CreateClient();
        }
        public async Task<EthUsdResponseDto> GetEthUsdUpdate(CancellationToken cancellationToken)
        {
            if(cancellationToken.IsCancellationRequested) return null;
            var response = await _httpClient.GetAsync(_settings.EthUsdUri, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var responseDto =
               await JsonSerializer.DeserializeAsync<EthUsdResponseDto>(await response.Content.ReadAsStreamAsync(), cancellationToken: cancellationToken);
            return responseDto;
        }

        public async Task<BtcUsdResponseDto> GetBtcUsdUpdate(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested) return null;
            var response = await _httpClient.GetAsync(_settings.BtcUsdUri, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var responseDto =
                await JsonSerializer.DeserializeAsync<BtcUsdResponseDto>(await response.Content.ReadAsStreamAsync(), cancellationToken: cancellationToken);
            return responseDto;
        }

        public async Task<TrxUsdtResponseDto> GetTrxUsdtUpdate(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested) return null;
            var response = await _httpClient.GetAsync(_settings.TrxUsdtUri, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var responseDto =
                await JsonSerializer.DeserializeAsync<TrxUsdtResponseDto>(await response.Content.ReadAsStreamAsync(), cancellationToken: cancellationToken);
            return responseDto;
        }
    }

    public class YobitClientSettings
    {
        public string EthUsdUri { get; set; }
        public string BtcUsdUri { get; set; }
        public string TrxUsdtUri { get; set; }
    }
}
