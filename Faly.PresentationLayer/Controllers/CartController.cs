using Faly.Core.Dtos.Ecommerce;
using Faly.Models;
using Faly.PresentationLayer.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Faly.PresentationLayer.Controllers;

public class CartController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public CartController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public IActionResult Index()
    {
        var cart =
            HttpContext.Session.Get<List<CartItemViewModel>>("Cart")
            ?? new List<CartItemViewModel>();
        return View(cart);
    }

    [HttpPost]
    public async Task<IActionResult> AddToCart(int courseId)
    {
        var client = _httpClientFactory.CreateClient("ApiClient");

        var response = await client.GetAsync($"api/courses/{courseId}");

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<CourseDetailDto>();

            var cart =
                HttpContext.Session.Get<List<CartItemViewModel>>("Cart")
                ?? new List<CartItemViewModel>();

            cart.Add(
                new CartItemViewModel
                {
                    CourseId = result.Id,
                    CourseName = result.Name,
                    Price = result.Price,
                }
            );

            HttpContext.Session.Set("Cart", cart);

            return RedirectToAction("Index", "Cart");
        }
        else
        {
            // Hata yönetimi
            return RedirectToAction("Details", "Course", new { id = courseId });
        }
    }

    [HttpPost]
    public IActionResult RemoveFromCart(int courseId)
    {
        var cart =
            HttpContext.Session.Get<List<CartItemViewModel>>("Cart")
            ?? new List<CartItemViewModel>();

        var itemToRemove = cart.FirstOrDefault(c => c.CourseId == courseId);
        if (itemToRemove != null)
        {
            cart.Remove(itemToRemove);
            HttpContext.Session.Set("Cart", cart);
        }

        return RedirectToAction("Index", "Cart");
    }
}
