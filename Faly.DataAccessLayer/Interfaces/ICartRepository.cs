using Faly.DataAccessLayer.Entities;

namespace Faly.DataAccessLayer.Interfaces;

public interface ICartRepository
{
    Task<Cart?> GetCartByUserIdAsync(string userId);
    Task<CartItem?> GetCartItemAsync(int cartId, int courseId);
    Task AddCartAsync(Cart cart);
    Task AddCartItemAsync(CartItem cartItem);
    Task RemoveCartItemAsync(CartItem cartItem);
    Task ClearCartAsync(string userId);
    Task SaveChangesAsync();
}
