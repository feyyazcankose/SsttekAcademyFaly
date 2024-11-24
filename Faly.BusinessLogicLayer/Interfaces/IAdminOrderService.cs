using Faly.Core;
using Faly.Core.Dtos.Admin;
using Faly.DataAccessLayer.Entities;

namespace Faly.BussinessLogicLayer.Interfaces;

using System.Collections.Generic;
using System.Threading.Tasks;

public interface IAdminOrderService
{
    Task<ServiceResult<IEnumerable<AdminOrderDto>>> GetAllOrdersAsync(); // Tüm siparişler
    Task<ServiceResult<AdminOrderDto>> GetOrderByIdAsync(int orderId); // Sipariş detayı
    Task<ServiceResult<IEnumerable<AdminOrderDto>>> GetOrdersByUserIdAsync(string userId); // Kullanıcıya ait siparişler

}

