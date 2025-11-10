using System.ComponentModel.DataAnnotations;

namespace PetShop.Core.DTOs
{
    public class CreateOrderDto
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Payment { get; set; } = string.Empty;

        [Required]
        public List<CreateOrderItemDto> OrderItems { get; set; } = new List<CreateOrderItemDto>();
    }
}
