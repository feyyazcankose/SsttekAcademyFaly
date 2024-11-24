using Faly.Core.Dtos.Ecommerce;
using Faly.Models;
using Microsoft.AspNetCore.Mvc;

namespace Faly.PresentationLayer.Controllers;

public class CourseController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public CourseController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> Details(int id)
    {
        var client = _httpClientFactory.CreateClient("ApiClient");

        var response = await client.GetAsync($"api/courses/{id}");

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<CourseDetailDto>();

            var course = new CourseDetailViewModel
            {
                Id = result.Id,
                Name = result.Name,
                Description = result.Description,
                Price = result.Price,
                Categories = result.Categories,
                Sections = result
                    .Sections.Select(sectionDto => new CourseSectionViewModel
                    {
                        Id = sectionDto.Id,
                        Title = sectionDto.Title,
                        Videos = sectionDto
                            .Videos.Select(videoDto => new VideoViewModel
                            {
                                Id = videoDto.Id,
                                Title = videoDto.Title,
                            })
                            .ToList(),
                    })
                    .ToList(),
            };

            return View(course);
        }
        else
        {
            // Hata yönetimi
            return RedirectToAction("Index", "Home");
        }
    }
}
