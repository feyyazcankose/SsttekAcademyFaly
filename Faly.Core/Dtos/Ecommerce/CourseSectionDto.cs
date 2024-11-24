using System.ComponentModel;
using Swashbuckle.AspNetCore.Annotations;

namespace Faly.Core.Dtos.Ecommerce;

public class CourseSectionDto
{
    [SwaggerSchema("Unique identifier of the course section.")]
    [DefaultValue(1)]
    public int Id { get; set; }

    [SwaggerSchema("Title of the course section.")]
    [DefaultValue("Getting Started")]
    public string Title { get; set; } = default!;

    [SwaggerSchema("Description of the course section.")]
    [DefaultValue("Introduction to the course.")]
    public string Description { get; set; } = default!;

    [SwaggerSchema("List of videos in the section.")]
    public List<VideoDto> Videos { get; set; } = new();
}
