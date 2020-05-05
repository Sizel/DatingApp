using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.Data.DTOs
{
    public class UserForLoginDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
    }
}