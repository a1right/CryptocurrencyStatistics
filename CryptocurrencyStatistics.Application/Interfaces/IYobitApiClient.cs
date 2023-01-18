using CryptocurrencyStatistics.Application.Dtos;
using System.Threading.Tasks;
using System.Threading;

namespace CryptocurrencyStatistics.Application.Interfaces
{
    public interface IYobitApiClient
    {
        public Task<EthUsdYobitResponseDto> GetEthUsdUpdate(CancellationToken cancellationToken);
        public Task<BtcUsdYobitResponseDto> GetBtcUsdUpdate(CancellationToken cancellationToken);
        public Task<TrxUsdtYobitResponseDto> GetTrxUsdtUpdate(CancellationToken cancellationToken);
    }
}
