using Faly.DataAccessLayer.Entities;

namespace Faly.DataAccessLayer.Interfaces;

public interface IOrderRepository
{
    Task<Order> CreateOrderAsync(Order order);
    Task<IEnumerable<Order>> GetOrdersByUserIdAsync(string userId);
    Task<Order> GetOrderByIdAsync(int orderId);
}
