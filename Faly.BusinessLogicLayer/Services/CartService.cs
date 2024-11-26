using Faly.BussinessLogicLayer.Interfaces;
using Faly.Core;
using Faly.Core.Dtos.Ecommerce;
using Faly.DataAccessLayer.Entities;
using Faly.DataAccessLayer.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Faly.BussinessLogicLayer.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IConfiguration _configuration;

        public CartService(
            ICartRepository cartRepository,
            ICourseRepository courseRepository,
            IConfiguration configuration
        )
        {
            _cartRepository = cartRepository;
            _courseRepository = courseRepository;
            _configuration = configuration;
        }

        public async Task<ServiceResult<CartDto>> GetCartAsync(string userId)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);
            if (cart == null)
            {
                var newCart = new Cart { UserId = userId, CartItems = new List<CartItem>() };
                await _cartRepository.AddCartAsync(newCart);
                await _cartRepository.SaveChangesAsync();
                cart = newCart;
            }

            var cdnBaseUrl = _configuration["CDN:BaseUrl"] ?? string.Empty;

            var cartDto = new CartDto
            {
                Id = cart.Id,
                UserId = cart.UserId,
                CartItems = cart
                    .CartItems.Select(ci => new CartItemDto
                    {
                        Id = ci.Id,
                        CourseId = ci.CourseId,
                        CourseName = ci.CourseName,
                        Price = ci.Price,
                        Quantity = 1,
                    })
                    .ToList(),
                TotalAmount = cart.CartItems.Sum(ci => ci.Price * 1),
            };

            return ServiceResult<CartDto>.SuccessResult(cartDto, "Sepet başarıyla alındı.");
        }

        public async Task<ServiceResult<CartItemDto>> AddToCartAsync(
            string userId,
            int courseId,
            int quantity = 1
        )
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);
            if (cart == null)
            {
                cart = new Cart { UserId = userId, CartItems = new List<CartItem>() };
                await _cartRepository.AddCartAsync(cart);
                await _cartRepository.SaveChangesAsync();
            }

            var course = await _courseRepository.GetCourseByIdAsync(courseId);
            if (course == null)
            {
                return ServiceResult<CartItemDto>.ErrorResult("Kurs bulunamadı.");
            }

            var newCartItem = new CartItem
            {
                CartId = cart.Id,
                CourseId = courseId,
                CourseName = course.Name,
                Price = course.Price,
            };
            await _cartRepository.AddCartItemAsync(newCartItem);

            await _cartRepository.SaveChangesAsync();
            return ServiceResult<CartItemDto>.SuccessResult(
                new CartItemDto
                {
                    CourseId = course.Id,
                    CourseName = course.Name,
                    Price = course.Price,
                    Quantity = quantity,
                },
                "Kurs başarıyla sepete eklendi."
            );
        }

        public async Task<ServiceResult<bool>> RemoveFromCartAsync(string userId, int courseId)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);
            if (cart == null)
            {
                return ServiceResult<bool>.ErrorResult("Sepet bulunamadı.");
            }

            var cartItem = await _cartRepository.GetCartItemAsync(cart.Id, courseId);
            if (cartItem == null)
            {
                return ServiceResult<bool>.ErrorResult("Sepet öğesi bulunamadı.");
            }

            await _cartRepository.RemoveCartItemAsync(cartItem);
            await _cartRepository.SaveChangesAsync();

            return ServiceResult<bool>.SuccessResult(true, "Kurs başarıyla sepetten kaldırıldı.");
        }

        public async Task<ServiceResult<bool>> ClearCartAsync(string userId)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);
            if (cart == null)
            {
                return ServiceResult<bool>.ErrorResult("Sepet bulunamadı.");
            }

            await _cartRepository.ClearCartAsync(userId);
            await _cartRepository.SaveChangesAsync();

            return ServiceResult<bool>.SuccessResult(true, "Sepet başarıyla temizlendi.");
        }
    }
}
