using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace Faly.Core.Dtos.Ecommerce;

public class PaymentDto
{
    [Required(ErrorMessage = "Order ID is required.")]
    [SwaggerSchema("Unique identifier of the order.")]
    [DefaultValue(1)]
    public int OrderId { get; set; }

    [Required(ErrorMessage = "Amount is required.")]
    [SwaggerSchema("Amount to be paid.")]
    [DefaultValue(59.98)]
    public decimal Amount { get; set; }

    [Required(ErrorMessage = "Payment method is required.")]
    [SwaggerSchema("Payment method used.")]
    [DefaultValue("CreditCard")]
    public string PaymentMethod { get; set; } = default!;

    [Required(ErrorMessage = "Card number is required.")]
    [SwaggerSchema("Credit card number.")]
    [DefaultValue("4111111111111111")]
    public string CardNumber { get; set; } = default!;

    [Required(ErrorMessage = "Expiry date is required.")]
    [SwaggerSchema("Expiry date of the card.")]
    [DefaultValue("12/25")]
    public string ExpiryDate { get; set; } = default!;

    [Required(ErrorMessage = "CVC is required.")]
    [SwaggerSchema("CVC code of the card.")]
    [DefaultValue("123")]
    public string CVC { get; set; } = default!;
}
