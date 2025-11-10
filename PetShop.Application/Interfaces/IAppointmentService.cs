using PetShop.Core.DTOs;

namespace PetShop.Application.Interfaces
{
    public interface IAppointmentService
    {
        Task<IEnumerable<AppointmentDto>> GetAllAppointmentsAsync();
        Task<AppointmentDto?> GetAppointmentByIdAsync(Guid id);
        Task<AppointmentDto> CreateAppointmentAsync(CreateAppointmentDto createAppointmentDto);
        Task<AppointmentDto?> UpdateAppointmentAsync(Guid id, UpdateAppointmentDto updateAppointmentDto);
        Task<bool> DeleteAppointmentAsync(Guid id);
        Task<IEnumerable<AppointmentDto>> GetAppointmentsByUserIdAsync(Guid userId);
        Task<IEnumerable<AppointmentDto>> GetAppointmentsByPetIdAsync(Guid petId);
    }
}
