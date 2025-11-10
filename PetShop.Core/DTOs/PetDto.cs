namespace PetShop.Core.DTOs
{
    public class PetDto
    {
        public Guid PetId { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string? Breed { get; set; }
        public int? Age { get; set; }
        public decimal? Weight { get; set; }
        public string? HealthInfo { get; set; }
    }
}
