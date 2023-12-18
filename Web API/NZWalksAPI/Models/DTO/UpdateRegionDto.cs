using System.ComponentModel.DataAnnotations;

namespace NZWalksAPI.Models.DTO
{
    public class UpdateRegionDto
    {
        [Required]
        [MaxLength(3, ErrorMessage = "Code  is only contains 3 max characters")]
        [MinLength(3, ErrorMessage = "Code  is only contains 3 min characters")]
        public string Code { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = " name is only contains 3 max characters")]
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
