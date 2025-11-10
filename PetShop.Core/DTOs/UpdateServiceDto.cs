using System.ComponentModel.DataAnnotations;

namespace PetShop.Core.DTOs
{
    public class UpdateServiceDto
    {
        [MaxLength(100)]
        public string? Name { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public decimal? Price { get; set; }

        [MaxLength(50)]
        public string? Type { get; set; }
    }
}
