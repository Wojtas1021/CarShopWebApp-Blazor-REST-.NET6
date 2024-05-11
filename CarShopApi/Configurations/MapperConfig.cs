using AutoMapper;
using CarShopApi.Data;
using CarShopApi.Models.Producer;

namespace CarShopApi.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            // create mapp and reversed
            CreateMap<ProducerCreateDtocs, Producer>().ReverseMap();
            CreateMap<ProducerUpdateDto, Producer>().ReverseMap();
            CreateMap<ProducerReadOnlyDto, Producer>().ReverseMap();
        }
    }
}
