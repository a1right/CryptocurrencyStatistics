﻿using CryptocurrencyStatistics.Application.Dtos;
using System.Threading.Tasks;
using System.Threading;

namespace CryptocurrencyStatistics.Application.Interfaces
{
    public interface IYobitApiClient
    {
        public Task<EthUsdResponseDto> GetEthUsdUpdate(CancellationToken cancellationToken);
        public Task<BtcUsdResponseDto> GetBtcUsdUpdate(CancellationToken cancellationToken);
        public Task<TrxUsdtResponseDto> GetTrxUsdtUpdate(CancellationToken cancellationToken);
    }
}
