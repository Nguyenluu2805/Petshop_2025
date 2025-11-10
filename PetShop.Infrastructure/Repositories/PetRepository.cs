using Microsoft.EntityFrameworkCore;
using PetShop.Core.Entities;
using PetShop.Core.Interfaces;
using PetShop.Infrastructure.Data;

namespace PetShop.Infrastructure.Repositories
{
    public class PetRepository : Repository<Pet>, IPetRepository
    {
        public PetRepository(PetShopDbContext context) : base(context) { }

        public async Task<IEnumerable<Pet>> GetPetsByUserIdAsync(Guid userId)
        {
            return await _dbSet.Where(p => p.UserId == userId).ToListAsync();
        }

        public async Task<Pet?> GetPetWithAppointmentsAsync(Guid petId)
        {
            return await _dbSet
                .Include(p => p.Appointments)!
                    .ThenInclude(a => a.Service)
                .FirstOrDefaultAsync(p => p.PetId == petId);
        }
    }
}
