using System;
using System.Collections.Generic;
using System.Text;

namespace CryptocurrencyStatistics.Domain
{
    public class Record
    {
        public int Id { get; set; }
        public string PairName { get; set; }
        public decimal Value { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}
