using Faly.BussinessLogicLayer.Interfaces;
using Faly.Core.Dtos.Ecommerce;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Faly.Api.Controllers;

[ApiController]
[Route("api/courses")]
[ApiExplorerSettings(GroupName = "Ecommerce > Course")]
public class CourseController : CustomControllerBase
{
    private readonly ICourseService _courseService;

    public CourseController(ICourseService courseService)
    {
        _courseService = courseService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<CourseDto>), StatusCodes.Status200OK)]
    [SwaggerOperation(Summary = "Get All Courses", Description = "Retrieve all available courses.")]
    public async Task<IActionResult> GetAllCourses()
    {
        return HandleServiceResult(await _courseService.GetAllCoursesAsync());
    }

    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Get Course Details",
        Description = "Retrieve details of a specific course."
    )]
    [ProducesResponseType(typeof(CourseDetailDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCourseDetails(int id)
    {
        return HandleServiceResult(await _courseService.GetCourseByIdAsync(id));
    }

    [HttpGet("search")]
    [ProducesResponseType(typeof(List<CourseDto>), StatusCodes.Status200OK)]
    [SwaggerOperation(Summary = "Search Courses", Description = "Search courses by name.")]
    public async Task<IActionResult> SearchCourses([FromQuery] string name)
    {
        return HandleServiceResult(await _courseService.SearchCoursesByNameAsync(name));
    }
}
