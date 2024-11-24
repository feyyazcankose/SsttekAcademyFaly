using Faly.BussinessLogicLayer.Interfaces;
using Faly.Core;
using Faly.Core.Dtos.Admin;
using Faly.DataAccessLayer.Entities;
using Faly.DataAccessLayer.Interfaces;

namespace Faly.BussinessLogicLayer.Services;

public class AdminOrderService : IAdminOrderService
{
    private readonly IAdminRepository<Order> _orderRepository;
    private readonly IAdminRepository<Payment> _paymentRepository;

    public AdminOrderService(IAdminRepository<Order> orderRepository, IAdminRepository<Payment> paymentRepository)
    {
        _orderRepository = orderRepository;
        _paymentRepository = paymentRepository;
    }

    public async Task<ServiceResult<IEnumerable<AdminOrderDto>>> GetAllOrdersAsync()
    {
        var orders = await _orderRepository.GetAllAsync();

        var orderDtos = orders.Select(order =>
        {
            var payment = order.Payment; // Payment navigation property üzerinden alınıyor
            return new AdminOrderDto
            {
                Id = order.Id,
                UserId = order.UserId,
                UserFullName = $"{order.User.FirstName} {order.User.LastName}",
                TotalPrice = order.TotalPrice,
                OrderDate = order.OrderDate,
                PaymentStatus = payment?.PaymentStatus ?? "Unknown", // Payment varsa al, yoksa 'Unknown'
            };
        });

        return ServiceResult<IEnumerable<AdminOrderDto>>.SuccessResult(orderDtos, "Orders retrieved successfully.");
    }

    public async Task<ServiceResult<AdminOrderDto>> GetOrderByIdAsync(int orderId)
    {
        var order = await _orderRepository.GetByIdAsync(orderId);
        if (order == null)
            return ServiceResult<AdminOrderDto>.ErrorResult("Order not found.");

        var payment = order.Payment; // Payment navigation property üzerinden alınıyor
        var orderDto = new AdminOrderDto
        {
            Id = order.Id,
            UserId = order.UserId,
            UserFullName = $"{order.User.FirstName} {order.User.LastName}",
            TotalPrice = order.TotalPrice,
            OrderDate = order.OrderDate,
            PaymentStatus = payment?.PaymentStatus ?? "Unknown",
        };

        return ServiceResult<AdminOrderDto>.SuccessResult(orderDto, "Order retrieved successfully.");
    }

    public async Task<ServiceResult<IEnumerable<AdminOrderDto>>> GetOrdersByUserIdAsync(string userId)
    {
        var orders = await _orderRepository.GetByConditionAsync(o => o.UserId == userId);

        if (!orders.Any())
            return ServiceResult<IEnumerable<AdminOrderDto>>.ErrorResult("No orders found for this user.");

        var orderDtos = orders.Select(order =>
        {
            var payment = order.Payment;
            return new AdminOrderDto
            {
                Id = order.Id,
                UserId = order.UserId,
                UserFullName = $"{order.User.FirstName} {order.User.LastName}",
                TotalPrice = order.TotalPrice,
                OrderDate = order.OrderDate,
                PaymentStatus = payment?.PaymentStatus ?? "Unknown",
            };
        });

        return ServiceResult<IEnumerable<AdminOrderDto>>.SuccessResult(orderDtos, "Orders retrieved successfully.");
    }
}
