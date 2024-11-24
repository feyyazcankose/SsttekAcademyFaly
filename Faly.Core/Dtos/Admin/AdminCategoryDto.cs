using System.ComponentModel;
using Swashbuckle.AspNetCore.Annotations;

namespace Faly.Core.Dtos.Admin;

public class AdminCategoryDto
{
    [SwaggerSchema("Unique identifier of the category.")]
    [DefaultValue(1)]
    public int Id { get; set; }

    [SwaggerSchema("Name of the category.")]
    [DefaultValue("Programming")]
    public string Name { get; set; } = default!;

    [SwaggerSchema("Description of the category.")]
    [DefaultValue("Courses related to programming and development.")]
    public string? Description { get; set; }

    [SwaggerSchema("Indicates whether the category is active.")]
    [DefaultValue(true)]
    public bool IsActive { get; set; }
}
