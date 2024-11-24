using System.ComponentModel;
using Swashbuckle.AspNetCore.Annotations;

namespace Faly.Core.Dtos.Admin;

public class AdminCourseDto
{
    [SwaggerSchema("Unique identifier of the course.")]
    [DefaultValue(1)]
    public int Id { get; set; }

    [SwaggerSchema("Name of the course.")]
    [DefaultValue("Introduction to Programming")]
    public string Name { get; set; } = default!;

    [SwaggerSchema("Description of the course.")]
    [DefaultValue("Learn the basics of programming.")]
    public string Description { get; set; } = default!;

    [SwaggerSchema("Price of the course.")]
    [DefaultValue(29.99)]
    public decimal Price { get; set; }

    [SwaggerSchema("List of categories associated with the course.")]
    [DefaultValue(new[] { "Programming", "Technology" })]
    public List<string> Categories { get; set; } = new();
}