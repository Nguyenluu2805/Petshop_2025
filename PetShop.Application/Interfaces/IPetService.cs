using PetShop.Core.DTOs;

namespace PetShop.Application.Interfaces
{
    public interface IPetService
    {
        Task<IEnumerable<PetDto>> GetAllPetsAsync();
        Task<PetDto?> GetPetByIdAsync(Guid id);
        Task<PetDto> CreatePetAsync(CreatePetDto createPetDto);
        Task<PetDto?> UpdatePetAsync(Guid id, UpdatePetDto updatePetDto);
        Task<bool> DeletePetAsync(Guid id);
        Task<IEnumerable<PetDto>> GetPetsByUserIdAsync(Guid userId);
        Task<PetDto?> GetPetDetailsAsync(Guid petId);
    }
}
