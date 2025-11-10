using AutoMapper;
using PetShop.Application.Interfaces;
using PetShop.Core.DTOs;
using PetShop.Core.Entities;
using PetShop.Core.Interfaces;

namespace PetShop.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IProductRepository productRepository, IOrderItemRepository orderItemRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _orderItemRepository = orderItemRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public async Task<OrderDto?> GetOrderByIdAsync(Guid id)
        {
            var order = await _orderRepository.GetOrderWithItemsAsync(id);
            return _mapper.Map<OrderDto>(order);
        }

        public async Task<OrderDto> CreateOrderAsync(CreateOrderDto createOrderDto)
        {
            var order = _mapper.Map<Order>(createOrderDto);
            order.OrderId = Guid.NewGuid();
            order.CreatedAt = DateTime.UtcNow;
            order.Status = "Pending"; // Initial status

            decimal totalPrice = 0;
            foreach (var itemDto in createOrderDto.OrderItems)
            {
                var product = await _productRepository.GetByIdAsync(itemDto.ProductId);
                if (product == null || product.Stock < itemDto.Quantity)
                {
                    throw new InvalidOperationException($"Product {itemDto.ProductId} not found or insufficient stock.");
                }
                totalPrice += itemDto.Quantity * product.Price;
                product.Stock -= itemDto.Quantity; // Decrease stock
                await _productRepository.UpdateAsync(product); // Update product stock
            }

            order.TotalPrice = totalPrice;

            await _orderRepository.AddAsync(order);

            foreach (var itemDto in createOrderDto.OrderItems)
            {
                var orderItem = _mapper.Map<OrderItem>(itemDto);
                orderItem.OrderId = order.OrderId;
                var product = await _productRepository.GetByIdAsync(itemDto.ProductId); // Re-fetch product to get current price
                orderItem.Price = product!.Price;
                await _orderItemRepository.AddAsync(orderItem);
            }

            var createdOrder = await _orderRepository.GetOrderWithItemsAsync(order.OrderId);
            return _mapper.Map<OrderDto>(createdOrder);
        }

        public async Task<bool> UpdateOrderStatusAsync(Guid orderId, string status)
        {
            var existingOrder = await _orderRepository.GetByIdAsync(orderId);
            if (existingOrder == null)
            {
                return false;
            }

            existingOrder.Status = status;
            await _orderRepository.UpdateAsync(existingOrder);
            return true;
        }

        public async Task<bool> DeleteOrderAsync(Guid id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
            {
                return false;
            }

            await _orderRepository.DeleteAsync(order);
            return true;
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersByUserIdAsync(Guid userId)
        {
            var orders = await _orderRepository.GetOrdersByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public async Task<decimal> GetTotalRevenueAsync()
        {
            var orders = await _orderRepository.GetAllAsync();
            return orders.Sum(o => o.TotalPrice);
        }
    }
}
