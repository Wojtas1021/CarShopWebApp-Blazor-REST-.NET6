namespace CarShopApi.Data;

public partial class Producer
{
    public Producer()
    {
        Cars = new HashSet<Car>();
    }

    public int Id { get; set; }
    public string? Name { get; set; }
    public string? FullName { get; set; }
    public string? Info { get; set; }
    public bool? IsActive { get; set; }

    public virtual ICollection<Car> Cars { get; set; }
}
