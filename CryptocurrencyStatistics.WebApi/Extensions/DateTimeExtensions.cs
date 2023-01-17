using System;

namespace CryptocurrencyStatistics.WebApi.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime ToUniversalDateTime(this int yobitResponseValue)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
                .AddSeconds(yobitResponseValue).ToUniversalTime();
        }
    }
}
