using System.ComponentModel.DataAnnotations;

namespace PetShop.Core.DTOs
{
    public class UpdateProductDto
    {
        [MaxLength(100)]
        public string? Name { get; set; }

        [MaxLength(50)]
        public string? Category { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public decimal? Price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Stock cannot be negative.")]
        public int? Stock { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        [MaxLength(250)]
        public string? ImageUrl { get; set; }
    }
}
