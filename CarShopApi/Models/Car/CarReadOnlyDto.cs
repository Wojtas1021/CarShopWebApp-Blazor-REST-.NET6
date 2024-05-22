namespace CarShopApi.Models.Car
{
    public class CarReadOnlyDto : BaseDto
    {
        public string Name { get; set; }
        public string Model { get; set; }
        public string Number { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public int ProducerId { get; set; }
        public string ProducerName { get; set; }
    }
}
