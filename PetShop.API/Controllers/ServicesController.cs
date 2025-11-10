using Microsoft.AspNetCore.Mvc;
using PetShop.Application.Interfaces;
using PetShop.Core.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace PetShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "EmployeeOnly")] // Only Employees and Admins can manage services
    public class ServicesController : ControllerBase
    {
        private readonly IServiceService _serviceService;

        public ServicesController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        [HttpGet]
        [AllowAnonymous] // Public access to view all services
        public async Task<IActionResult> GetAllServices()
        {
            var services = await _serviceService.GetAllServicesAsync();
            return Ok(services);
        }

        [HttpGet("{id}")]
        [AllowAnonymous] // Public access to view a single service
        public async Task<IActionResult> GetServiceById(Guid id)
        {
            var service = await _serviceService.GetServiceByIdAsync(id);
            if (service == null)
            {
                return NotFound();
            }
            return Ok(service);
        }

        [HttpPost]
        [Authorize(Policy = "EmployeeOnly")]
        public async Task<IActionResult> CreateService([FromBody] CreateServiceDto createServiceDto)
        {
            var service = await _serviceService.CreateServiceAsync(createServiceDto);
            return CreatedAtAction(nameof(GetServiceById), new { id = service.ServiceId }, service);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "EmployeeOnly")]
        public async Task<IActionResult> UpdateService(Guid id, [FromBody] UpdateServiceDto updateServiceDto)
        {
            var updatedService = await _serviceService.UpdateServiceAsync(id, updateServiceDto);
            if (updatedService == null)
            {
                return NotFound();
            }
            return Ok(updatedService);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> DeleteService(Guid id)
        {
            var result = await _serviceService.DeleteServiceAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpGet("most-booked/{count}")]
        [Authorize(Policy = "AdminOnly")] // Only Admin can view most booked services
        public async Task<IActionResult> GetMostBookedServices(int count)
        {
            var services = await _serviceService.GetMostBookedServicesAsync(count);
            return Ok(services);
        }
    }
}
