using PetShop.Core.DTOs;

namespace PetShop.Application.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
        Task<OrderDto?> GetOrderByIdAsync(Guid id);
        Task<OrderDto> CreateOrderAsync(CreateOrderDto createOrderDto);
        Task<bool> UpdateOrderStatusAsync(Guid orderId, string status);
        Task<bool> DeleteOrderAsync(Guid id);
        Task<IEnumerable<OrderDto>> GetOrdersByUserIdAsync(Guid userId);
        Task<decimal> GetTotalRevenueAsync();
    }
}
