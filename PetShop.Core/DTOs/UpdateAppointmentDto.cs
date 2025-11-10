using System.ComponentModel.DataAnnotations;

namespace PetShop.Core.DTOs
{
    public class UpdateAppointmentDto
    {
        public Guid? UserId { get; set; }
        public Guid? PetId { get; set; }
        public Guid? ServiceId { get; set; }
        public DateTime? DateTime { get; set; }

        [MaxLength(200)]
        public string? Location { get; set; }

        [MaxLength(50)]
        public string? Status { get; set; }
    }
}
