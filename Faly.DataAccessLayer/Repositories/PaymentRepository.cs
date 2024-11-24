using Faly.DataAccessLayer.Data;
using Faly.DataAccessLayer.Entities;
using Faly.DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Faly.DataAccessLayer.Repositories;

public class PaymentRepository : IPaymentRepository
{
    private readonly AppDbContext _context;

    public PaymentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Payment> CreatePaymentAsync(Payment payment)
    {
        _context.Payments.Add(payment);
        await _context.SaveChangesAsync();
        return payment;
    }

    public async Task<Payment> GetPaymentByOrderIdAsync(int orderId)
    {
        return await _context.Payments.FirstOrDefaultAsync(p => p.OrderId == orderId);
    }
}
