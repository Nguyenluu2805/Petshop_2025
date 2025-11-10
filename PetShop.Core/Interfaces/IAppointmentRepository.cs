using PetShop.Core.Entities;

namespace PetShop.Core.Interfaces
{
    public interface IAppointmentRepository : IRepository<Appointment>
    {
        Task<IEnumerable<Appointment>> GetAppointmentsByUserIdAsync(Guid userId);
        Task<IEnumerable<Appointment>> GetAppointmentsByPetIdAsync(Guid petId);
        Task<IEnumerable<Appointment>> GetAppointmentsByServiceIdAsync(Guid serviceId);
    }
}
