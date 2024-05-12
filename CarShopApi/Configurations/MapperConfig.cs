using AutoMapper;
using CarShopApi.Data;
using CarShopApi.Models.Car;
using CarShopApi.Models.Producer;

namespace CarShopApi.Configurations;

public class MapperConfig : Profile
{
    public MapperConfig()
    {
        // create mapp and reversed
        CreateMap<ProducerCreateDto, Producer>().ReverseMap();
        CreateMap<ProducerUpdateDto, Producer>().ReverseMap();
        CreateMap<ProducerReadOnlyDto, Producer>().ReverseMap();
        
        CreateMap<CarCreateDto, Car>().ReverseMap();
        CreateMap<CarUpdateDto, Car>().ReverseMap();
        CreateMap<Car, CarReadOnlyDto>().
            ForMember(p => p.ProducerName, c => c.MapFrom(map => $"{map.Producer.Name}"))
            .ReverseMap();
        CreateMap<Car, CarDetailsDto>().
            ForMember(p => p.ProducerName, c => c.MapFrom(map => $"{map.Producer.Name}"))
            .ReverseMap();
    }
}
