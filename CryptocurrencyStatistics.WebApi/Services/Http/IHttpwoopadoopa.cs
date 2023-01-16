using System.Threading.Tasks;
using CryptocurrencyStatistics.WebApi.Models;

namespace CryptocurrencyStatistics.WebApi.Services.Http
{
    public interface IHttpwoopadoopa
    {
        public Task<EthUsdResponseTemplate> GetEthUsdUpdate();
    }
}
