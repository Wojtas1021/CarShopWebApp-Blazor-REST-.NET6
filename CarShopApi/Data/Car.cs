namespace CarShopApi.Data;

public partial class Car
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Model { get; set; }
    public string? Series { get; set; }
    public string? Number { get; set; }
    public decimal? Price { get; set; }
    public string? Colour { get; set; }
    public string? Description { get; set; }
    public string? Image { get; set; }
    public bool? IsActive { get; set; }
    public int? ProducerId { get; set; }

    public virtual Producer? Producer { get; set; }
}
