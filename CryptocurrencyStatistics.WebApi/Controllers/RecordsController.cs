
using AutoMapper;
using CryptocurrencyStatistics.Application.Interfaces;
using CryptocurrencyStatistics.Domain;
using CryptocurrencyStatistics.WebApi.Dtos;
using CryptocurrencyStatistics.WebApi.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CryptocurrencyStatistics.WebApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class RecordsController : ControllerBase
    {
        private readonly IRecordsRelationalRepository _recordsRelationalRepository;
        private readonly IRecordsDocumentOrientedRepository _recordsDocumentOrientedRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private string _ethUsdPairName;
        private string _btcUsdPairName;
        private string _trxUsdtPairName;

        public RecordsController(IRecordsRelationalRepository recordsRelationalRepository,
                                 IRecordsDocumentOrientedRepository recordsDocumentOrientedRepository, IMapper mapper, IConfiguration configuration)
        {
            _recordsRelationalRepository = recordsRelationalRepository;
            _recordsDocumentOrientedRepository = recordsDocumentOrientedRepository;
            _mapper = mapper;
            _configuration = configuration;
            _btcUsdPairName = _configuration["YobitEndpoints:BtcUsd:Name"];
            _ethUsdPairName = _configuration["YobitEndpoints:EthUsd:Name"];
            _trxUsdtPairName = _configuration["YobitEndpoints:TrxUsdt:Name"];
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecordReadDto>>> GetAll(CancellationToken cancellationToken)
        {
            var records = await _recordsDocumentOrientedRepository.GetAll(cancellationToken);
            var response = _mapper.Map<IEnumerable<RecordReadDto>>(records);
            return Ok(response);
        }

        #region EthUsd

        [HttpGet("eth_usd")]
        public async Task<ActionResult<RecordReadDto>> GetLastEthUsdRecord(CancellationToken cancellationToken)
        {
            var record = await _recordsRelationalRepository.GetLastRecord(_ethUsdPairName, cancellationToken);
            if (record is null)
                return NotFound();
            var response = _mapper.Map<RecordReadDto>(record);
            return Ok(response);
        }
        [HttpGet("eth_usd/{dateRequest}")]
        public async Task<ActionResult<RecordReadDto>> GetLastEthUsdRecord(int dateRequest, CancellationToken cancellationToken)
        {
            var date = dateRequest.ToUniversalDateTime();
            var record = await _recordsRelationalRepository.GetRecordAtDate(_ethUsdPairName, date, cancellationToken);
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
            await _recordsRelationalRepository.CreateRecord(record, cancellationToken);
            await _recordsDocumentOrientedRepository.Create(record, cancellationToken);
            return Ok();
        }

        #endregion

        #region BtcUsd

        [HttpGet("btc_usd")]
        public async Task<ActionResult<RecordReadDto>> GetLastBtcUsdRecord(CancellationToken cancellationToken)
        {
            var record = await _recordsRelationalRepository.GetLastRecord(_btcUsdPairName, cancellationToken);
            if (record is null)
                return NotFound();
            var response = _mapper.Map<RecordReadDto>(record);
            return Ok(response);
        }
        [HttpGet("btc_usd/{dateRequest}")]
        public async Task<ActionResult<RecordReadDto>> GetLastBtcUsdRecord(int dateRequest, CancellationToken cancellationToken)
        {
            var date = dateRequest.ToUniversalDateTime();
            var record = await _recordsRelationalRepository.GetRecordAtDate(_btcUsdPairName, date, cancellationToken);
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
            await _recordsRelationalRepository.CreateRecord(record, cancellationToken);
            await _recordsDocumentOrientedRepository.Create(record, cancellationToken);
            return Ok();
        }

        #endregion

        #region TrxUsdt
        [HttpGet("trx_usdt")]
        public async Task<ActionResult<RecordReadDto>> GetLastTrxUsdtRecord(CancellationToken cancellationToken)
        {
            var record = await _recordsRelationalRepository.GetLastRecord(_trxUsdtPairName, cancellationToken);
            if (record is null)
                return NotFound();
            var response = _mapper.Map<RecordReadDto>(record);
            return Ok(response);
        }
        [HttpGet("trx_usdt/{dateRequest}")]
        public async Task<ActionResult<RecordReadDto>> GetLastTrxUsdtRecord(int dateRequest, CancellationToken cancellationToken)
        {
            var date = dateRequest.ToUniversalDateTime();
            var record = await _recordsRelationalRepository.GetRecordAtDate(_trxUsdtPairName, date, cancellationToken);
            if (record is null)
                return NotFound();
            var response = _mapper.Map<RecordReadDto>(record);
            return Ok(response);
        }

        [HttpPost("trx_usdt")]
        public async Task<ActionResult> CreateTrxUsdtRecord([FromBody] RecordCreateDto createDto, CancellationToken cancellationToken)
        {
            var record = _mapper.Map<Record>(createDto);
            record.CreatedDateTime = createDto.CreatedDateTime.ToUniversalDateTime();
            record.PairName = _trxUsdtPairName;
            await _recordsRelationalRepository.CreateRecord(record, cancellationToken);
            await _recordsDocumentOrientedRepository.Create(record, cancellationToken);
            return Ok();
        }


        #endregion
    }
}
