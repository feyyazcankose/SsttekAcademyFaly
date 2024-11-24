using System.ComponentModel;
using Swashbuckle.AspNetCore.Annotations;

namespace Faly.Core.Dtos.Ecommerce;

public class OrderDetailDto
{
    [SwaggerSchema("Unique identifier of the course.")]
    [DefaultValue(1)]
    public int CourseId { get; set; }

    [SwaggerSchema("Name of the course.")]
    [DefaultValue("Introduction to Programming")]
    public string CourseName { get; set; } = default!;

    [SwaggerSchema("Price of the course at the time of order.")]
    [DefaultValue(29.99)]
    public decimal Price { get; set; }

    [SwaggerSchema("Quantity ordered.")]
    [DefaultValue(1)]
    public int Quantity { get; set; }
}
