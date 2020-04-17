using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.Data.DTOs
{
    public class UserDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [StringLength(15, MinimumLength = 8)]
        public string Password { get; set; }
    }
}