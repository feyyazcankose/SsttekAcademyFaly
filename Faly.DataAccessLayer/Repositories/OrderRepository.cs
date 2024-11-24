using Faly.DataAccessLayer.Data;
using Faly.DataAccessLayer.Entities;
using Faly.DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Faly.DataAccessLayer.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;

    public OrderRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Order> CreateOrderAsync(Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        return order;
    }

    public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(string userId)
    {
        return await _context
            .Orders.Include(o => o.OrderDetails)
            .ThenInclude(od => od.Course)
            .Where(o => o.UserId == userId)
            .ToListAsync();
    }

    public async Task<Order> GetOrderByIdAsync(int orderId)
    {
        return await _context
            .Orders.Include(o => o.OrderDetails)
            .ThenInclude(od => od.Course)
            .FirstOrDefaultAsync(o => o.Id == orderId);
    }
}
