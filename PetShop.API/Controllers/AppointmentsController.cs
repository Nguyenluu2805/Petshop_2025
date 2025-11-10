using Microsoft.AspNetCore.Mvc;
using PetShop.Application.Interfaces;
using PetShop.Core.DTOs;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace PetShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentsController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        private Guid GetUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null) throw new UnauthorizedAccessException("User ID not found.");
            return Guid.Parse(userIdClaim.Value);
        }

        [HttpGet]
        [Authorize(Policy = "EmployeeOnly")]
        public async Task<IActionResult> GetAllAppointments()
        {
            var appointments = await _appointmentService.GetAllAppointmentsAsync();
            return Ok(appointments);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "CustomerOnly")]
        public async Task<IActionResult> GetAppointmentById(Guid id)
        {
            var appointment = await _appointmentService.GetAppointmentByIdAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }

            // Ensure customers can only view their own appointments
            if (User.IsInRole("Customer") && appointment.UserId != GetUserId())
            {
                return Forbid();
            }

            return Ok(appointment);
        }

        [HttpGet("user/{userId}")]
        [Authorize(Policy = "CustomerOnly")]
        public async Task<IActionResult> GetAppointmentsByUserId(Guid userId)
        {
            // Ensure customers can only view their own appointments
            if (User.IsInRole("Customer") && userId != GetUserId())
            {
                return Forbid();
            }

            var appointments = await _appointmentService.GetAppointmentsByUserIdAsync(userId);
            return Ok(appointments);
        }

        [HttpGet("pet/{petId}")]
        [Authorize(Policy = "CustomerOnly")]
        public async Task<IActionResult> GetAppointmentsByPetId(Guid petId)
        {
            // Additional check to ensure the pet belongs to the current user if role is Customer
            // This would require a PetService call to verify pet ownership.
            // For now, assuming GetAppointmentsByPetIdAsync handles authorization internally or relies on parent GetAppointmentsByUserId

            var appointments = await _appointmentService.GetAppointmentsByPetIdAsync(petId);
            return Ok(appointments);
        }

        [HttpPost]
        [Authorize(Policy = "CustomerOnly")]
        public async Task<IActionResult> CreateAppointment([FromBody] CreateAppointmentDto createAppointmentDto)
        {
            // Ensure customers can only create appointments for themselves
            if (User.IsInRole("Customer") && createAppointmentDto.UserId != GetUserId())
            {
                return Forbid();
            }

            var appointment = await _appointmentService.CreateAppointmentAsync(createAppointmentDto);
            return CreatedAtAction(nameof(GetAppointmentById), new { id = appointment.AppointId }, appointment);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "CustomerOnly")]
        public async Task<IActionResult> UpdateAppointment(Guid id, [FromBody] UpdateAppointmentDto updateAppointmentDto)
        {
            var existingAppointment = await _appointmentService.GetAppointmentByIdAsync(id);
            if (existingAppointment == null)
            {
                return NotFound();
            }

            // Ensure customers can only update their own appointments
            if (User.IsInRole("Customer") && existingAppointment.UserId != GetUserId())
            {
                return Forbid();
            }

            var updatedAppointment = await _appointmentService.UpdateAppointmentAsync(id, updateAppointmentDto);
            if (updatedAppointment == null)
            {
                return NotFound();
            }
            return Ok(updatedAppointment);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> DeleteAppointment(Guid id)
        {
            var result = await _appointmentService.DeleteAppointmentAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
