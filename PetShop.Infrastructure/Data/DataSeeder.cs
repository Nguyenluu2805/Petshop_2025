using PetShop.Core.Entities;
using BCrypt.Net;

namespace PetShop.Infrastructure.Data
{
    public class DataSeeder
    {
        private readonly PetShopDbContext _context;

        public DataSeeder(PetShopDbContext context)
        {
            _context = context;
        }

        public void SeedData()
        {
            if (!_context.Users.Any())
            {
                var adminUser = new User
                {
                    UserId = Guid.NewGuid(),
                    Name = "Admin User",
                    Email = "admin@petshop.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                    Phone = "1112223333",
                    Role = "Admin",
                    CreatedAt = DateTime.UtcNow
                };

                var employeeUser = new User
                {
                    UserId = Guid.NewGuid(),
                    Name = "Employee User",
                    Email = "employee@petshop.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Employee@123"),
                    Phone = "4445556666",
                    Role = "Employee",
                    CreatedAt = DateTime.UtcNow
                };

                var customerUser = new User
                {
                    UserId = Guid.NewGuid(),
                    Name = "Customer User",
                    Email = "customer@petshop.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Customer@123"),
                    Phone = "7778889999",
                    Role = "Customer",
                    CreatedAt = DateTime.UtcNow
                };

                _context.Users.AddRange(adminUser, employeeUser, customerUser);
            }

            if (!_context.Products.Any())
            {
                var products = new List<Product>
                {
                    new Product { ProductId = Guid.NewGuid(), Name = "Royal Canin Dog Food", Category = "Food", Price = 50.00m, Stock = 100, Description = "Premium dry dog food for all breeds.", ImageUrl = "royal_canin.jpg", CreatedAt = DateTime.UtcNow },
                    new Product { ProductId = Guid.NewGuid(), Name = "Whiskas Cat Food", Category = "Food", Price = 25.50m, Stock = 150, Description = "Delicious wet cat food for adult cats.", ImageUrl = "whiskas.jpg", CreatedAt = DateTime.UtcNow },
                    new Product { ProductId = Guid.NewGuid(), Name = "Pet Grooming Brush", Category = "Accessories", Price = 15.00m, Stock = 200, Description = "High-quality brush for pet grooming.", ImageUrl = "grooming_brush.jpg", CreatedAt = DateTime.UtcNow },
                    new Product { ProductId = Guid.NewGuid(), Name = "Dog Leash", Category = "Accessories", Price = 10.00m, Stock = 300, Description = "Durable dog leash for daily walks.", ImageUrl = "dog_leash.jpg", CreatedAt = DateTime.UtcNow }
                };
                _context.Products.AddRange(products);
            }

            if (!_context.Services.Any())
            {
                var services = new List<Service>
                {
                    new Service { ServiceId = Guid.NewGuid(), Name = "Full Grooming", Description = "Complete grooming service including bath, haircut, and nail trim.", Price = 75.00m, Type = "Spa", CreatedAt = DateTime.UtcNow },
                    new Service { ServiceId = Guid.NewGuid(), Name = "Basic Bath", Description = "Basic bath and dry for all pet sizes.", Price = 30.00m, Type = "Bath", CreatedAt = DateTime.UtcNow },
                    new Service { ServiceId = Guid.NewGuid(), Name = "Veterinary Check-up", Description = "General health check-up and consultation.", Price = 60.00m, Type = "Health", CreatedAt = DateTime.UtcNow }
                };
                _context.Services.AddRange(services);
            }

            _context.SaveChanges();
        }
    }
}
