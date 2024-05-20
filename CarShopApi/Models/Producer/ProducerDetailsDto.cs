using CarShopApi.Models.Car;

namespace CarShopApi.Models.Producer
{
    public class ProducerDetailsDto : ProducerReadOnlyDto
    {
        public List<CarReadOnlyDto> Cars { get; set; }
    }
}
