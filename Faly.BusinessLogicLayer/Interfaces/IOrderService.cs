using Faly.Core;
using Faly.Core.Dtos.Ecommerce;

namespace Faly.BussinessLogicLayer.Interfaces;

public interface IOrderService
{
    Task<ServiceResult<OrderDto>> CreateOrderAsync(string userId, CreateOrderDto createOrderDto);
    Task<ServiceResult<IEnumerable<OrderDto>>> GetUserOrdersAsync(string userId);
}
