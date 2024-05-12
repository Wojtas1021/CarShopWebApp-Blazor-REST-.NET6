using Microsoft.AspNetCore.Identity;

namespace CarShopApi.Data
{
    public class ApiUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
