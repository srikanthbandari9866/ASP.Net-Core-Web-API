using System.Text.Json.Serialization;

namespace NZWalksAPI.Models.DTO
{
    public class UserDto
    {

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        //[JsonIgnore] public string Password { get; set; }
        public string Role { get; set; }
    }
}
