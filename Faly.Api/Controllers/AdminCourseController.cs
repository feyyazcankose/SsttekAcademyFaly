using Faly.BussinessLogicLayer.Interfaces;
using Faly.Core.Dtos;
using Faly.Core.Dtos.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Faly.Api.Controllers;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/admin/courses")]
[ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
[ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status500InternalServerError)]
public class AdminCourseController : CustomControllerBase
{
    private readonly IAdminCourseService _adminCourseService;

    public AdminCourseController(IAdminCourseService adminCourseService)
    {
        _adminCourseService = adminCourseService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<AdminCourseDto>), StatusCodes.Status200OK)]
    [SwaggerOperation(Summary = "Get All Courses", Description = "Retrieve a list of all courses.")]
    public async Task<IActionResult> GetAllCourses()
    {
        return HandleServiceResult(await _adminCourseService.GetAllCoursesAsync());
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(AdminCourseDto), StatusCodes.Status200OK)]
    [SwaggerOperation(
        Summary = "Get Course Details",
        Description = "Retrieve details about a specific course."
    )]
    public async Task<IActionResult> GetCourseDetails(int id)
    {
        return HandleServiceResult(await _adminCourseService.GetCourseByIdAsync(id));
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create Course", Description = "Create a new course.")]
    public async Task<IActionResult> CreateCourse([FromBody] CreateCourseDto createCourseDto)
    {
        return HandleServiceResult(await _adminCourseService.AddCourseAsync(createCourseDto));
    }

    [HttpPut]
    [SwaggerOperation(Summary = "Update Course", Description = "Update an existing course.")]
    public async Task<IActionResult> UpdateCourse([FromBody] UpdateCourseDto updateCourseDto)
    {
        return HandleServiceResult(await _adminCourseService.UpdateCourseAsync(updateCourseDto));
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Delete Course", Description = "Delete a course by its ID.")]
    public async Task<IActionResult> DeleteCourse(int id)
    {
        return HandleServiceResult(await _adminCourseService.DeleteCourseAsync(id));
    }
}
