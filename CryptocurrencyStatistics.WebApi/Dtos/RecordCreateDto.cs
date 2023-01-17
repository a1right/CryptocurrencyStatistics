using System;
using System.Text.Json.Serialization;

namespace CryptocurrencyStatistics.WebApi.Dtos
{
    public class RecordCreateDto
    {
        public decimal Value { get; set; }
        public int CreatedDateTime { get; set; }
    }
}
