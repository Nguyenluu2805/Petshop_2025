using PetShop.Core.Entities;

namespace PetShop.Core.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(Guid userId);
        Task<Order?> GetOrderWithItemsAsync(Guid orderId);
    }
}
