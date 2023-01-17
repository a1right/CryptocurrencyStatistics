using CryptocurrencyStatistics.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace CryptocurrencyStatistics.Application.Interfaces
{
    public interface IYobitApiClient
    {
        public Task<EthUsdResponseDto> GetEthUsdUpdate(CancellationToken cancellationToken);
    }
}
