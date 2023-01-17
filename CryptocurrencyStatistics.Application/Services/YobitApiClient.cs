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


        public YobitApiClient(IHttpClientFactory httpClientFactory, YobitClientSettings settings)
        {
            _httpClientFactory = httpClientFactory;
            _settings = settings;
        }
        public async Task<EthUsdResponseDto> GetEthUsdUpdate(CancellationToken cancellationToken)
        {
            using var httpClient = _httpClientFactory.CreateClient();
            if(cancellationToken.IsCancellationRequested) return null;
            var response = await httpClient.GetAsync(_settings.EthUsdUri, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var responseDto =
               await JsonSerializer.DeserializeAsync<EthUsdResponseDto>(await response.Content.ReadAsStreamAsync(), cancellationToken: cancellationToken);
            return responseDto;
        }
    }

    public class YobitClientSettings
    {
        public string EthUsdUri { get; set; }
    }
}
