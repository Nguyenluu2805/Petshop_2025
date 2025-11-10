using PetShop.Core.Entities;

namespace PetShop.Core.Interfaces
{
    public interface IPetRepository : IRepository<Pet>
    {
        Task<IEnumerable<Pet>> GetPetsByUserIdAsync(Guid userId);
        Task<Pet?> GetPetWithAppointmentsAsync(Guid petId);
    }
}
