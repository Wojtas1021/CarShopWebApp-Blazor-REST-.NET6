using System.ComponentModel.DataAnnotations;

namespace CarShopApi.Models.User
{
    public class UserDto : LoginUserDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Role { get; set; }
    }
}