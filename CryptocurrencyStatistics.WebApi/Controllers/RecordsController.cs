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

        public RecordsController(IRecordsService recordsService, IMapper mapper, IConfiguration configuration)
        {
            _recordsService = recordsService;
            _mapper = mapper;
            _configuration = configuration;
            _ethUsdPairName = _configuration["YobitEndpoints:EthUsd:Name"];
        }
        [HttpGet("/eth_usd")]
        public async Task<ActionResult<RecordReadDto>> GetLastEthUsdRecord(CancellationToken cancellationToken)
        {
            var record = await _recordsService.GetLastRecord(_ethUsdPairName, cancellationToken);
            if (record is null)
                return NotFound();
            var response = _mapper.Map<RecordReadDto>(record);
            return Ok(response);
        }
        [HttpGet("/eth_usd/{dateRequest}")]
        public async Task<ActionResult<RecordReadDto>> GetLastEthUsdRecord(int dateRequest ,CancellationToken cancellationToken)
        {
            var date = dateRequest.ToUniversalDateTime();
            var record = await _recordsService.GetRecordAtDate(_ethUsdPairName, date, cancellationToken);
            if (record is null)
                return NotFound();
            var response = _mapper.Map<RecordReadDto>(record);
            return Ok(response);
        }

        [HttpPost("/eth_usd")]
        public async Task<ActionResult> CreateEthUsdRecord([FromBody] RecordCreateDto createDto, CancellationToken cancellationToken)
        {
            var record = _mapper.Map<Record>(createDto);
            await _recordsService.CreateRecord(record, cancellationToken);
            return Ok(record.Id);
        }
    }
}
