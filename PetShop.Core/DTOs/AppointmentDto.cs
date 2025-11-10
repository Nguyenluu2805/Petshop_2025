namespace PetShop.Core.DTOs
{
    public class AppointmentDto
    {
        public Guid AppointId { get; set; }
        public Guid UserId { get; set; }
        public Guid PetId { get; set; }
        public Guid ServiceId { get; set; }
        public DateTime DateTime { get; set; }
        public string Location { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}
