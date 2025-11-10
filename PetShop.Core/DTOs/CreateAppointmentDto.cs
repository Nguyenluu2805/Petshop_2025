using System.ComponentModel.DataAnnotations;

namespace PetShop.Core.DTOs
{
    public class CreateAppointmentDto
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid PetId { get; set; }

        [Required]
        public Guid ServiceId { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        [MaxLength(200)]
        public string Location { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Status { get; set; } = string.Empty;
    }
}
