using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace Faly.Core.Dtos.Admin;

public class CreateCourseDto
{
    [Required(ErrorMessage = "Course name is required")]
    [SwaggerSchema("Name of the course.")]
    [DefaultValue("Introduction to Programming")]
    public string Name { get; set; } = default!;

    [SwaggerSchema("Description of the course.")]
    [DefaultValue("Learn the basics of programming.")]
    public string Description { get; set; } = default!;

    [Required(ErrorMessage = "Price is required")]
    [Range(0, double.MaxValue, ErrorMessage = "Price must be non-negative")]
    [SwaggerSchema("Price of the course.")]
    [DefaultValue(29.99)]
    public decimal Price { get; set; }

    [SwaggerSchema("Categories associated with the course.")]
    [DefaultValue(new[] { "Programming", "Technology" })]
    public List<string> Categories { get; set; } = new();
}