using Microsoft.EntityFrameworkCore;
using PetShop.Core.Entities;
using PetShop.Core.Interfaces;
using PetShop.Infrastructure.Data;

namespace PetShop.Infrastructure.Repositories
{
    public class AppointmentRepository : Repository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(PetShopDbContext context) : base(context) { }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByUserIdAsync(Guid userId)
        {
            return await _dbSet.Where(a => a.UserId == userId).ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByPetIdAsync(Guid petId)
        {
            return await _dbSet.Where(a => a.PetId == petId).ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByServiceIdAsync(Guid serviceId)
        {
            return await _dbSet.Where(a => a.ServiceId == serviceId).ToListAsync();
        }
    }
}
