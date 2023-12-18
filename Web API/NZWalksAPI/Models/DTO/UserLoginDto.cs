using System.ComponentModel.DataAnnotations;

namespace NZWalksAPI.Models.DTO
{
    public class UserLoginDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
