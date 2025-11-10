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
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        private Guid GetUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null) throw new UnauthorizedAccessException("User ID not found.");
            return Guid.Parse(userIdClaim.Value);
        }

        [HttpGet]
        [Authorize(Policy = "EmployeeOnly")]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "CustomerOnly")]
        public async Task<IActionResult> GetOrderById(Guid id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            // Ensure customers can only view their own orders
            if (User.IsInRole("Customer") && order.UserId != GetUserId())
            {
                return Forbid();
            }

            return Ok(order);
        }

        [HttpGet("user/{userId}")]
        [Authorize(Policy = "CustomerOnly")]
        public async Task<IActionResult> GetOrdersByUserId(Guid userId)
        {
            // Ensure customers can only view their own orders
            if (User.IsInRole("Customer") && userId != GetUserId())
            {
                return Forbid();
            }

            var orders = await _orderService.GetOrdersByUserIdAsync(userId);
            return Ok(orders);
        }

        [HttpPost]
        [Authorize(Policy = "CustomerOnly")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto createOrderDto)
        {
            if (User.IsInRole("Customer") && createOrderDto.UserId != GetUserId())
            {
                return Forbid();
            }

            try
            {
                var order = await _orderService.CreateOrderAsync(createOrderDto);
                return CreatedAtAction(nameof(GetOrderById), new { id = order.OrderId }, order);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/status")]
        [Authorize(Policy = "EmployeeOnly")]
        public async Task<IActionResult> UpdateOrderStatus(Guid id, [FromBody] string status)
        {
            var result = await _orderService.UpdateOrderStatusAsync(id, status);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            var result = await _orderService.DeleteOrderAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpGet("revenue")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> GetTotalRevenue()
        {
            var totalRevenue = await _orderService.GetTotalRevenueAsync();
            return Ok(new { TotalRevenue = totalRevenue });
        }
    }
}
