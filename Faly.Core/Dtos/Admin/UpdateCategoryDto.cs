using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace Faly.Core.Dtos.Admin;

public class UpdateCategoryDto
{
    [Required(ErrorMessage = "Category ID is required")]
    [SwaggerSchema("Unique identifier of the category.")]
    [DefaultValue(1)]
    public int Id { get; set; }

    [SwaggerSchema("Updated name of the category.")]
    [DefaultValue("Advanced Programming")]
    public string? Name { get; set; }

    [SwaggerSchema("Updated description of the category.")]
    [DefaultValue("Courses focusing on advanced programming topics.")]
    public string? Description { get; set; }

    [SwaggerSchema("Updated status indicating whether the category is active.")]
    [DefaultValue(true)]
    public bool IsActive { get; set; } = true;
}