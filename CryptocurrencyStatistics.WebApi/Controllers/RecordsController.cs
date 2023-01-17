using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CryptocurrencyStatistics.Application.Interfaces;
using CryptocurrencyStatistics.Domain;
using CryptocurrencyStatistics.WebApi.Dtos;
using CryptocurrencyStatistics.WebApi.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CryptocurrencyStatistics.WebApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class RecordsController : ControllerBase
    {
        private readonly IRecordsService _recordsService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private string _ethUsdPairName;
        private string _btcUsdPairName;

        public RecordsController(IRecordsService recordsService, IMapper mapper, IConfiguration configuration)
        {
            _recordsService = recordsService;
            _mapper = mapper;
            _configuration = configuration;
            _btcUsdPairName = _configuration["YobitEndpoints:BtcUsd:Name"];
            _ethUsdPairName = _configuration["YobitEndpoints:EthUsd:Name"];
        }

        #region EthUsd

        [HttpGet("eth_usd")]
        public async Task<ActionResult<RecordReadDto>> GetLastEthUsdRecord(CancellationToken cancellationToken)
        {
            var record = await _recordsService.GetLastRecord(_ethUsdPairName, cancellationToken);
            if (record is null)
                return NotFound();
            var response = _mapper.Map<RecordReadDto>(record);
            return Ok(response);
        }
        [HttpGet("eth_usd/{dateRequest}")]
        public async Task<ActionResult<RecordReadDto>> GetLastEthUsdRecord(int dateRequest ,CancellationToken cancellationToken)
        {
            var date = dateRequest.ToUniversalDateTime();
            var record = await _recordsService.GetRecordAtDate(_ethUsdPairName, date, cancellationToken);
            if (record is null)
                return NotFound();
            var response = _mapper.Map<RecordReadDto>(record);
            return Ok(response);
        }

        [HttpPost("eth_usd")]
        public async Task<ActionResult> CreateEthUsdRecord([FromBody] RecordCreateDto createDto, CancellationToken cancellationToken)
        {
            var record = _mapper.Map<Record>(createDto);
            record.CreatedDateTime = createDto.CreatedDateTime.ToUniversalDateTime();
            record.PairName = _ethUsdPairName;
            await _recordsService.CreateRecord(record, cancellationToken);
            return Ok(record.Id);
        }

        #endregion

        #region BtcUsd

        [HttpGet("btc_usd")]
        public async Task<ActionResult<RecordReadDto>> GetLastBtcUsdRecord(CancellationToken cancellationToken)
        {
            var record = await _recordsService.GetLastRecord(_btcUsdPairName, cancellationToken);
            if (record is null)
                return NotFound();
            var response = _mapper.Map<RecordReadDto>(record);
            return Ok(response);
        }
        [HttpGet("btc_usd/{dateRequest}")]
        public async Task<ActionResult<RecordReadDto>> GetLastBtcUsdRecord(int dateRequest, CancellationToken cancellationToken)
        {
            var date = dateRequest.ToUniversalDateTime();
            var record = await _recordsService.GetRecordAtDate(_btcUsdPairName, date, cancellationToken);
            if (record is null)
                return NotFound();
            var response = _mapper.Map<RecordReadDto>(record);
            return Ok(response);
        }

        [HttpPost("btc_usd")]
        public async Task<ActionResult> CreateBtcUsdRecord([FromBody] RecordCreateDto createDto, CancellationToken cancellationToken)
        {
            var record = _mapper.Map<Record>(createDto);
            record.CreatedDateTime = createDto.CreatedDateTime.ToUniversalDateTime();
            record.PairName = _btcUsdPairName;
            await _recordsService.CreateRecord(record, cancellationToken);
            return Ok(record.Id);
        }

        #endregion
    }
}
