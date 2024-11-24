using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace Faly.Core.Dtos.Admin;

public class CreateCategoryDto
{
    [Required(ErrorMessage = "Category name is required")]
    [SwaggerSchema("Name of the category.")]
    [DefaultValue("Programming")]
    public string Name { get; set; } = default!;

    [SwaggerSchema("Description of the category.")]
    [DefaultValue("Courses related to programming and development.")]
    public string? Description { get; set; }

    [SwaggerSchema("Indicates whether the category is active.")]
    [DefaultValue(true)]
    public bool IsActive { get; set; } = true;
}