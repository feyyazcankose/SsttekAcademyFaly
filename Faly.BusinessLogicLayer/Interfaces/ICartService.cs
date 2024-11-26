using Faly.Core;
using Faly.Core.Dtos.Ecommerce;

namespace Faly.BussinessLogicLayer.Interfaces
{
    public interface ICartService
    {
        Task<ServiceResult<CartDto>> GetCartAsync(string userId);
        Task<ServiceResult<CartItemDto>> AddToCartAsync(
            string userId,
            int courseId,
            int quantity = 1
        );
        Task<ServiceResult<bool>> RemoveFromCartAsync(string userId, int courseId);
        Task<ServiceResult<bool>> ClearCartAsync(string userId);
    }
}
