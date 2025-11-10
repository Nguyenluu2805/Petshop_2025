using PetShop.Core.Entities;

namespace PetShop.Core.Interfaces
{
    public interface IServiceRepository : IRepository<Service>
    {
        // Additional methods specific to Service can be added here if needed
        Task<IEnumerable<Service>> GetMostBookedServicesAsync(int count);
    }
}
