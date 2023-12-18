using System.ComponentModel.DataAnnotations;

namespace NZWalksAPI.Models.DTO
{
    public class UpdateWalkDto
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Name do not exceed 100 characters")]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Range(0, 100, ErrorMessage = "Length is in between 0 to 100")]
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        [Required]
        public Guid DifficultyId { get; set; }
        [Required]
        public Guid RegionId { get; set; }
    }
}
