
using System.Net.Http;
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
        private readonly YobitApiClientSettings _settings;
        private readonly HttpClient _httpClient;


        public YobitApiClient(IHttpClientFactory httpClientFactory, YobitApiClientSettings settings)
        {
            _httpClientFactory = httpClientFactory;
            _settings = settings;
            _httpClient = _httpClientFactory.CreateClient();
        }
        public async Task<EthUsdYobitResponseDto> GetEthUsdUpdate(CancellationToken cancellationToken)
        {
            if(cancellationToken.IsCancellationRequested) 
                return null;
            var response = await _httpClient.GetAsync(_settings.EthUsdUri, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var responseDto =
               await JsonSerializer.DeserializeAsync<EthUsdYobitResponseDto>(await response.Content.ReadAsStreamAsync(), cancellationToken: cancellationToken);
            return responseDto;
        }

        public async Task<BtcUsdYobitResponseDto> GetBtcUsdUpdate(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested) 
                return null;
            var response = await _httpClient.GetAsync(_settings.BtcUsdUri, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var responseDto =
                await JsonSerializer.DeserializeAsync<BtcUsdYobitResponseDto>(await response.Content.ReadAsStreamAsync(), cancellationToken: cancellationToken);
            return responseDto;
        }

        public async Task<TrxUsdtYobitResponseDto> GetTrxUsdtUpdate(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested) return null;
            var response = await _httpClient.GetAsync(_settings.TrxUsdtUri, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var responseDto =
                await JsonSerializer.DeserializeAsync<TrxUsdtYobitResponseDto>(await response.Content.ReadAsStreamAsync(), cancellationToken: cancellationToken);
            return responseDto;
        }
    }

    public class YobitApiClientSettings
    {
        public string EthUsdUri { get; set; }
        public string BtcUsdUri { get; set; }
        public string TrxUsdtUri { get; set; }
    }
}
