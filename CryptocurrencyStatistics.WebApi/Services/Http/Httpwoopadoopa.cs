using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using CryptocurrencyStatistics.WebApi.Models;
using Microsoft.Extensions.Configuration;

namespace CryptocurrencyStatistics.WebApi.Services.Http
{
    public class Httpwoopadoopa : IHttpwoopadoopa
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public Httpwoopadoopa(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        public async Task<EthUsdResponseTemplate> GetEthUsdUpdate()
        {
            var uri = _configuration["YoBitEndpoints:EthUsd:Uri"];
            var pairName = _configuration["YoBitEndpoints:EthUsd:Name"];
            var response = await _httpClient.GetAsync(uri);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return await JsonSerializer.DeserializeAsync<EthUsdResponseTemplate>(
                await response.Content.ReadAsStreamAsync());
        }
    }
}