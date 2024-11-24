using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace Faly.Core.Dtos.Admin;

public class UpdateCourseDto
{
    [Required(ErrorMessage = "Course ID is required")]
    [SwaggerSchema("Unique identifier of the course.")]
    [DefaultValue(1)]
    public int Id { get; set; }

    [SwaggerSchema("Updated name of the course.")]
    [DefaultValue("Introduction to Advanced Programming")]
    public string Name { get; set; } = default!;

    [SwaggerSchema("Updated description of the course.")]
    [DefaultValue("Learn advanced programming concepts.")]
    public string Description { get; set; } = default!;

    [SwaggerSchema("Updated price of the course.")]
    [DefaultValue(39.99)]
    public decimal Price { get; set; }
}