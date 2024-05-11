
namespace CarShopApi.Models.Producer
{
    public class ProducerReadOnlyDto : BaseDto
    {
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Info { get; set; }
    }
}
