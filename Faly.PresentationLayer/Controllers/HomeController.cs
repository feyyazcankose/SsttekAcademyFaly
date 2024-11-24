using System.Diagnostics;
using Faly.Core.Dtos.Ecommerce;
using Faly.Models;
using Microsoft.AspNetCore.Mvc;

namespace Faly.PresentationLayer.Controllers;

public class HomeController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public HomeController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> Index()
    {
        var client = _httpClientFactory.CreateClient("ApiClient");
        var response = await client.GetAsync("api/courses");

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<List<CourseDto>>();

            // DTO'dan ViewModel'e manuel mapping
            var viewModel = result
                .Select(dto => new CourseViewModel
                {
                    Id = dto.Id,
                    Name = dto.Name,
                    Description = dto.Description,
                    Price = dto.Price,
                })
                .ToList();

            return View(viewModel);
        }
        else
        {
            return View(new List<CourseViewModel>());
        }
    }
}
