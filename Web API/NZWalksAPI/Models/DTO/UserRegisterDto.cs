using System.ComponentModel.DataAnnotations;

namespace NZWalksAPI.Models.DTO
{
    public class UserRegisterDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
