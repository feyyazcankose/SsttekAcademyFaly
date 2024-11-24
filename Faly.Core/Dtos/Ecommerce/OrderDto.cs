using System.ComponentModel;
using Swashbuckle.AspNetCore.Annotations;

namespace Faly.Core.Dtos.Ecommerce;

public class OrderDto
{
    [SwaggerSchema("Unique identifier of the order.")]
    [DefaultValue(1)]
    public int Id { get; set; }

    [SwaggerSchema("Date when the order was placed.")]
    [DefaultValue("2023-10-01T12:00:00Z")]
    public DateTime OrderDate { get; set; }

    [SwaggerSchema("Total price of the order.")]
    [DefaultValue(59.98)]
    public decimal TotalPrice { get; set; }

    [SwaggerSchema("List of order details.")]
    public List<OrderDetailDto> OrderDetails { get; set; } = new();
}
