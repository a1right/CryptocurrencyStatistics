using System;
using System.Text.Json.Serialization;

namespace CryptocurrencyStatistics.WebApi.Dtos
{
    public class RecordCreateDto
    {
        public string PairName { get; set; }
        public decimal Value { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}
