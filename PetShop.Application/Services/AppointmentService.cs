using AutoMapper;
using PetShop.Application.Interfaces;
using PetShop.Core.DTOs;
using PetShop.Core.Entities;
using PetShop.Core.Interfaces;

namespace PetShop.Application.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMapper _mapper;

        public AppointmentService(IAppointmentRepository appointmentRepository, IMapper mapper)
        {
            _appointmentRepository = appointmentRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AppointmentDto>> GetAllAppointmentsAsync()
        {
            var appointments = await _appointmentRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<AppointmentDto>>(appointments);
        }

        public async Task<AppointmentDto?> GetAppointmentByIdAsync(Guid id)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(id);
            return _mapper.Map<AppointmentDto>(appointment);
        }

        public async Task<AppointmentDto> CreateAppointmentAsync(CreateAppointmentDto createAppointmentDto)
        {
            var appointment = _mapper.Map<Appointment>(createAppointmentDto);
            appointment.AppointId = Guid.NewGuid(); // Assign a new Guid
            await _appointmentRepository.AddAsync(appointment);
            return _mapper.Map<AppointmentDto>(appointment);
        }

        public async Task<AppointmentDto?> UpdateAppointmentAsync(Guid id, UpdateAppointmentDto updateAppointmentDto)
        {
            var existingAppointment = await _appointmentRepository.GetByIdAsync(id);
            if (existingAppointment == null)
            {
                return null;
            }

            _mapper.Map(updateAppointmentDto, existingAppointment);
            await _appointmentRepository.UpdateAsync(existingAppointment);
            return _mapper.Map<AppointmentDto>(existingAppointment);
        }

        public async Task<bool> DeleteAppointmentAsync(Guid id)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(id);
            if (appointment == null)
            {
                return false;
            }

            await _appointmentRepository.DeleteAsync(appointment);
            return true;
        }

        public async Task<IEnumerable<AppointmentDto>> GetAppointmentsByUserIdAsync(Guid userId)
        {
            var appointments = await _appointmentRepository.GetAppointmentsByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<AppointmentDto>>(appointments);
        }

        public async Task<IEnumerable<AppointmentDto>> GetAppointmentsByPetIdAsync(Guid petId)
        {
            var appointments = await _appointmentRepository.GetAppointmentsByPetIdAsync(petId);
            return _mapper.Map<IEnumerable<AppointmentDto>>(appointments);
        }
    }
}
