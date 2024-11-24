using System.ComponentModel;
using Swashbuckle.AspNetCore.Annotations;

namespace Faly.Core.Dtos.Admin;

public class AdminOrderDto
{
    [SwaggerSchema("Unique identifier of the order.")]
    [DefaultValue(123)]
    public int Id { get; set; }

    [SwaggerSchema("User ID associated with the order.")]
    [DefaultValue("abc-123")]
    public string UserId { get; set; } = default!;

    [SwaggerSchema("Full name of the user who placed the order.")]
    [DefaultValue("John Doe")]
    public string UserFullName { get; set; } = default!;

    [SwaggerSchema("Total price of the order.")]
    [DefaultValue(59.99)]
    public decimal TotalPrice { get; set; }

    [SwaggerSchema("Date and time when the order was placed.")]
    [DefaultValue("2023-11-24T15:30:00Z")]
    public DateTime OrderDate { get; set; }

    [SwaggerSchema("Status of the payment (e.g., Success, Failed).")]
    [DefaultValue("Success")]
    public string PaymentStatus { get; set; } = default!;
}