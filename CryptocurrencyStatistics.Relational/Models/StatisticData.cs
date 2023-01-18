using System;
using System.Collections.Generic;
using System.Text;

namespace CryptocurrencyStatistics.RelationalStorage.Models
{
    public class StatisticData
    {
        public int Id { get; set; }
        public int PairId { get; set; }
        public decimal Value { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public Pair Pair { get; set; }
    }
}
