using System;
using System.Collections.Generic;

namespace CarShopApi.Data
{
    public partial class Producer
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? FullName { get; set; }
        public string? Info { get; set; }
        public bool? IsActive { get; set; }
    }
}
