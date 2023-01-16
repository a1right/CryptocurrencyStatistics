using AutoMapper;
using CryptocurrencyStatistics.Domain;
using CryptocurrencyStatistics.WebApi.Dtos;
using CryptocurrencyStatistics.WebApi.Models;

namespace CryptocurrencyStatistics.WebApi.Mappings
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            CreateMap<Record, RecordReadDto>();
            CreateMap<RecordCreateDto, Record>();
            CreateMap<EthUsdResponseTemplate, RecordCreateDto>()
                .ForMember(dest => dest.Value,
                    options => options.MapFrom(source =>
                        source.eth_usd.last));
        }
    }
}
