using System.ComponentModel.DataAnnotations;

namespace PetShop.Core.DTOs
{
    public class CreateServiceDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public decimal Price { get; set; }

        [Required]
        [MaxLength(50)]
        public string Type { get; set; } = string.Empty;
    }
}
