using AutoMapper;
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

        }
    }
}
