using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetShop.Core.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; } // Storing hashed password

        [MaxLength(20)]
        public string? Phone { get; set; }

        [Required]
        [MaxLength(50)]
        public string Role { get; set; } // Customer, Employee, Admin

        public DateTime CreatedAt { get; set; }

        // Navigation properties
        public ICollection<Order>? Orders { get; set; }
        public ICollection<Pet>? Pets { get; set; }
        public ICollection<Appointment>? Appointments { get; set; }
    }
}
