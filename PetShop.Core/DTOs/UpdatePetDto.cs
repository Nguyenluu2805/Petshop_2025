using System.ComponentModel.DataAnnotations;

namespace PetShop.Core.DTOs
{
    public class UpdatePetDto
    {
        [MaxLength(100)]
        public string? Name { get; set; }

        [MaxLength(50)]
        public string? Type { get; set; }

        [MaxLength(50)]
        public string? Breed { get; set; }

        [Range(0, 30, ErrorMessage = "Age must be between 0 and 30.")]
        public int? Age { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Weight must be greater than 0.")]
        public decimal? Weight { get; set; }

        [MaxLength(500)]
        public string? HealthInfo { get; set; }
    }
}
