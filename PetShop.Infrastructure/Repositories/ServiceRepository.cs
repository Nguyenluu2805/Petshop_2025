using Microsoft.EntityFrameworkCore;
using PetShop.Core.Entities;
using PetShop.Core.Interfaces;
using PetShop.Infrastructure.Data;

namespace PetShop.Infrastructure.Repositories
{
    public class ServiceRepository : Repository<Service>, IServiceRepository
    {
        public ServiceRepository(PetShopDbContext context) : base(context) { }

        public async Task<IEnumerable<Service>> GetMostBookedServicesAsync(int count)
        {
            return await _context.Appointments
                .GroupBy(a => a.ServiceId)
                .OrderByDescending(g => g.Count())
                .Take(count)
                .Join(_context.Services, // Join with Services table
                      appointmentGroup => appointmentGroup.Key,
                      service => service.ServiceId,
                      (appointmentGroup, service) => service)
                .ToListAsync();
        }
    }
}
