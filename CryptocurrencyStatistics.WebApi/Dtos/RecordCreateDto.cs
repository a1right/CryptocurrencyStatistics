
using System.ComponentModel.DataAnnotations;

namespace CryptocurrencyStatistics.WebApi.Dtos
{
    public class RecordCreateDto
    {
        [Required]
        public decimal Value { get; set; }
        [Required]
        public int CreatedDateTime { get; set; }
    }
}
