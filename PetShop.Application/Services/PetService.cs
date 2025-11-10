using AutoMapper;
using PetShop.Application.Interfaces;
using PetShop.Core.DTOs;
using PetShop.Core.Entities;
using PetShop.Core.Interfaces;

namespace PetShop.Application.Services
{
    public class PetService : IPetService
    {
        private readonly IPetRepository _petRepository;
        private readonly IMapper _mapper;

        public PetService(IPetRepository petRepository, IMapper mapper)
        {
            _petRepository = petRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PetDto>> GetAllPetsAsync()
        {
            var pets = await _petRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<PetDto>>(pets);
        }

        public async Task<PetDto?> GetPetByIdAsync(Guid id)
        {
            var pet = await _petRepository.GetByIdAsync(id);
            return _mapper.Map<PetDto>(pet);
        }

        public async Task<PetDto> CreatePetAsync(CreatePetDto createPetDto)
        {
            var pet = _mapper.Map<Pet>(createPetDto);
            pet.PetId = Guid.NewGuid(); // Assign a new Guid
            await _petRepository.AddAsync(pet);
            return _mapper.Map<PetDto>(pet);
        }

        public async Task<PetDto?> UpdatePetAsync(Guid id, UpdatePetDto updatePetDto)
        {
            var existingPet = await _petRepository.GetByIdAsync(id);
            if (existingPet == null)
            {
                return null;
            }

            _mapper.Map(updatePetDto, existingPet);
            await _petRepository.UpdateAsync(existingPet);
            return _mapper.Map<PetDto>(existingPet);
        }

        public async Task<bool> DeletePetAsync(Guid id)
        {
            var pet = await _petRepository.GetByIdAsync(id);
            if (pet == null)
            {
                return false;
            }

            await _petRepository.DeleteAsync(pet);
            return true;
        }

        public async Task<IEnumerable<PetDto>> GetPetsByUserIdAsync(Guid userId)
        {
            var pets = await _petRepository.GetPetsByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<PetDto>>(pets);
        }

        public async Task<PetDto?> GetPetDetailsAsync(Guid petId)
        {
            var pet = await _petRepository.GetPetWithAppointmentsAsync(petId);
            return _mapper.Map<PetDto>(pet);
        }
    }
}
