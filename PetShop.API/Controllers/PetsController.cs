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
    public class PetsController : ControllerBase
    {
        private readonly IPetService _petService;

        public PetsController(IPetService petService)
        {
            _petService = petService;
        }

        private Guid GetUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null) throw new UnauthorizedAccessException("User ID not found.");
            return Guid.Parse(userIdClaim.Value);
        }

        [HttpGet]
        [Authorize(Policy = "EmployeeOnly")]
        public async Task<IActionResult> GetAllPets()
        {
            var pets = await _petService.GetAllPetsAsync();
            return Ok(pets);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "CustomerOnly")]
        public async Task<IActionResult> GetPetById(Guid id)
        {
            var pet = await _petService.GetPetByIdAsync(id);
            if (pet == null)
            {
                return NotFound();
            }

            // Ensure customers can only view their own pets
            if (User.IsInRole("Customer") && pet.UserId != GetUserId())
            {
                return Forbid();
            }

            return Ok(pet);
        }

        [HttpGet("user/{userId}")]
        [Authorize(Policy = "CustomerOnly")]
        public async Task<IActionResult> GetPetsByUserId(Guid userId)
        {
            // Ensure customers can only view their own pets
            if (User.IsInRole("Customer") && userId != GetUserId())
            {
                return Forbid();
            }

            var pets = await _petService.GetPetsByUserIdAsync(userId);
            return Ok(pets);
        }

        [HttpPost]
        [Authorize(Policy = "CustomerOnly")]
        public async Task<IActionResult> CreatePet([FromBody] CreatePetDto createPetDto)
        {
            if (User.IsInRole("Customer") && createPetDto.UserId != GetUserId())
            {
                return Forbid();
            }
            var pet = await _petService.CreatePetAsync(createPetDto);
            return CreatedAtAction(nameof(GetPetById), new { id = pet.PetId }, pet);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "CustomerOnly")]
        public async Task<IActionResult> UpdatePet(Guid id, [FromBody] UpdatePetDto updatePetDto)
        {
            var existingPet = await _petService.GetPetByIdAsync(id);
            if (existingPet == null)
            {
                return NotFound();
            }

            // Ensure customers can only update their own pets
            if (User.IsInRole("Customer") && existingPet.UserId != GetUserId())
            {
                return Forbid();
            }

            var updatedPet = await _petService.UpdatePetAsync(id, updatePetDto);
            if (updatedPet == null)
            {
                return NotFound();
            }
            return Ok(updatedPet);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> DeletePet(Guid id)
        {
            var result = await _petService.DeletePetAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
