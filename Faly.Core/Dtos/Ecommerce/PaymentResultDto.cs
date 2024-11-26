using System.ComponentModel;
using Swashbuckle.AspNetCore.Annotations;

namespace Faly.Core.Dtos.Ecommerce;

public class PaymentResultDto
{
    [SwaggerSchema("Ödemenin benzersiz tanımlayıcısı.")]
    [DefaultValue(1)]
    public int PaymentId { get; set; }

    [SwaggerSchema("Ödeme durumu.")]
    [DefaultValue("Success")]
    public string Status { get; set; } = default!;

    [SwaggerSchema("İşlem referansı.")]
    [DefaultValue("ABC123XYZ")]
    public string TransactionReference { get; set; } = default!;
}
