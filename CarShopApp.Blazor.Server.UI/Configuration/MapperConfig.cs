using AutoMapper;
using CarShopApp.Blazor.Server.UI.Services.Base;

namespace CarShopApp.Blazor.Server.UI.Configuration
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<ProducerReadOnlyDto, ProducerUpdateDto>().ReverseMap();
        }
    }
}
