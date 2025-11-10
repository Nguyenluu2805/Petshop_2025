using PetShop.Core.Entities;

namespace PetShop.Core.Interfaces
{
    public interface IOrderItemRepository : IRepository<OrderItem>
    {
        // Additional methods specific to OrderItem can be added here if needed
    }
}
