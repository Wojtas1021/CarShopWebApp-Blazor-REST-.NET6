using System.ComponentModel.DataAnnotations;

namespace CarShopApi.Models.Producer
{
    public class ProducerCreateDtocs
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        public string FullName { get; set; }
        [StringLength(250)]
        public string Info { get; set; }
    }
}
