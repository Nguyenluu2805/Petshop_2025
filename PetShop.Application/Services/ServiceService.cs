using AutoMapper;
using PetShop.Application.Interfaces;
using PetShop.Core.DTOs;
using PetShop.Core.Entities;
using PetShop.Core.Interfaces;

namespace PetShop.Application.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IMapper _mapper;

        public ServiceService(IServiceRepository serviceRepository, IMapper mapper)
        {
            _serviceRepository = serviceRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ServiceDto>> GetAllServicesAsync()
        {
            var services = await _serviceRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ServiceDto>>(services);
        }

        public async Task<ServiceDto?> GetServiceByIdAsync(Guid id)
        {
            var service = await _serviceRepository.GetByIdAsync(id);
            return _mapper.Map<ServiceDto>(service);
        }

        public async Task<ServiceDto> CreateServiceAsync(CreateServiceDto createServiceDto)
        {
            var service = _mapper.Map<Service>(createServiceDto);
            service.ServiceId = Guid.NewGuid(); // Assign a new Guid
            await _serviceRepository.AddAsync(service);
            return _mapper.Map<ServiceDto>(service);
        }

        public async Task<ServiceDto?> UpdateServiceAsync(Guid id, UpdateServiceDto updateServiceDto)
        {
            var existingService = await _serviceRepository.GetByIdAsync(id);
            if (existingService == null)
            {
                return null;
            }

            _mapper.Map(updateServiceDto, existingService);
            await _serviceRepository.UpdateAsync(existingService);
            return _mapper.Map<ServiceDto>(existingService);
        }

        public async Task<bool> DeleteServiceAsync(Guid id)
        {
            var service = await _serviceRepository.GetByIdAsync(id);
            if (service == null)
            {
                return false;
            }

            await _serviceRepository.DeleteAsync(service);
            return true;
        }

        public async Task<IEnumerable<ServiceDto>> GetMostBookedServicesAsync(int count)
        {
            var services = await _serviceRepository.GetMostBookedServicesAsync(count);
            return _mapper.Map<IEnumerable<ServiceDto>>(services);
        }
    }
}
