namespace PetShop.Core.DTOs
{
    public class ServiceDto
    {
        public Guid ServiceId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; } = string.Empty;
    }
}
