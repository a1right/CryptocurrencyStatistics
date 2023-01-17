using System;
using AutoMapper;
using CryptocurrencyStatistics.Application.Dtos;
using CryptocurrencyStatistics.Domain;
using CryptocurrencyStatistics.WebApi.Dtos;

namespace CryptocurrencyStatistics.WebApi.Mappings
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            CreateMap<Record, RecordReadDto>();
            CreateMap<RecordCreateDto, Record>();
            CreateMap<EthUsdResponseDto, Record>()
                .ForMember(dest => dest.Value,
                    options => options
                        .MapFrom(source => source.eth_usd.last));

        }
    }
}
