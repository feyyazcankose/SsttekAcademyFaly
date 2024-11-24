using Faly.Core;
using Faly.Core.Dtos.Ecommerce;

namespace Faly.BussinessLogicLayer.Interfaces;

public interface IPaymentService
{
    Task<ServiceResult<string>> ProcessPaymentAsync(PaymentDto paymentDto);
}
