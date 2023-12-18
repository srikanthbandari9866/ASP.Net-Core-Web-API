using System.ComponentModel.DataAnnotations;

namespace NZWalksAPI.Models.DTO
{
    public class ImageUploadDto
    {
        [Required]
        public IFormFile File { get; set; }
        [Required]
        public string FileName { get; set; }
    }
}
