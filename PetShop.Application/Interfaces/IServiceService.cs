using PetShop.Core.DTOs;

namespace PetShop.Application.Interfaces
{
    public interface IServiceService
    {
        Task<IEnumerable<ServiceDto>> GetAllServicesAsync();
        Task<ServiceDto?> GetServiceByIdAsync(Guid id);
        Task<ServiceDto> CreateServiceAsync(CreateServiceDto createServiceDto);
        Task<ServiceDto?> UpdateServiceAsync(Guid id, UpdateServiceDto updateServiceDto);
        Task<bool> DeleteServiceAsync(Guid id);
        Task<IEnumerable<ServiceDto>> GetMostBookedServicesAsync(int count);
    }
}
