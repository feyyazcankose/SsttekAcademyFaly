using Faly.DataAccessLayer.Entities;

namespace Faly.DataAccessLayer.Interfaces;

public interface IPaymentRepository
{
    Task<Payment> CreatePaymentAsync(Payment payment);
    Task<Payment> GetPaymentByOrderIdAsync(int orderId);
}
