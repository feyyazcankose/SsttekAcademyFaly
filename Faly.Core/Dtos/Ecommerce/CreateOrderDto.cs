using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace Faly.Core.Dtos.Ecommerce;

public class CreateOrderDto
{
    [Required(ErrorMessage = "At least one course must be selected.")]
    [SwaggerSchema("List of course IDs to order.")]
    [DefaultValue(new[] { 1, 2 })]
    public List<int> CourseIds { get; set; } = new();
}
