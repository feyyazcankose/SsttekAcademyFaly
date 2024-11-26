using System.ComponentModel;
using Swashbuckle.AspNetCore.Annotations;

namespace Faly.Core.Dtos.Ecommerce;

public class CartDto
{
    [SwaggerSchema("Sepetin benzersiz tanımlayıcısı.")]
    [DefaultValue(1)]
    public int Id { get; set; }

    [SwaggerSchema("Kullanıcı kimliği.")]
    [DefaultValue("user-123")]
    public string UserId { get; set; } = default!;

    [SwaggerSchema("Sepetteki öğelerin listesi.")]
    public List<CartItemDto> CartItems { get; set; } = new();

    [SwaggerSchema("Sepetin toplam tutarı.")]
    [DefaultValue(59.98)]
    public decimal TotalAmount { get; set; }
}
