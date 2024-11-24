using Faly.BussinessLogicLayer.Interfaces;
using Faly.Core;
using Faly.Core.Dtos.Ecommerce;
using Faly.DataAccessLayer.Entities;
using Faly.DataAccessLayer.Interfaces;

namespace Faly.BussinessLogicLayer.Services;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IOrderRepository _orderRepository;

    public PaymentService(IPaymentRepository paymentRepository, IOrderRepository orderRepository)
    {
        _paymentRepository = paymentRepository;
        _orderRepository = orderRepository;
    }

    public async Task<ServiceResult<string>> ProcessPaymentAsync(PaymentDto paymentDto)
    {
        // Ödeme işlemi simülasyonu
        var order = await _orderRepository.GetOrderByIdAsync(paymentDto.OrderId);
        if (order == null)
        {
            return ServiceResult<string>.ErrorResult("Order not found.");
        }

        // Ödeme başarılı varsayımı
        var payment = new Payment
        {
            OrderId = paymentDto.OrderId,
            Amount = paymentDto.Amount,
            PaymentStatus = "Success",
            PaymentDate = DateTime.UtcNow,
        };

        await _paymentRepository.CreatePaymentAsync(payment);

        return ServiceResult<string>.SuccessResult("Payment processed successfully.");
    }
}
