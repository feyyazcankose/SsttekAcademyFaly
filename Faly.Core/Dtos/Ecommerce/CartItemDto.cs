using System.ComponentModel;
using Swashbuckle.AspNetCore.Annotations;

namespace Faly.Core.Dtos.Ecommerce;

public class CartItemDto
{
    [SwaggerSchema("Sepet öğesinin benzersiz tanımlayıcısı.")]
    [DefaultValue(1)]
    public int Id { get; set; }

    [SwaggerSchema("Kursun benzersiz tanımlayıcısı.")]
    [DefaultValue(1)]
    public int CourseId { get; set; }

    [SwaggerSchema("Kursun adı.")]
    [DefaultValue("Giriş Programlama")]
    public string CourseName { get; set; } = default!;

    [SwaggerSchema("Kursun fiyatı.")]
    [DefaultValue(29.99)]
    public decimal Price { get; set; }

    [SwaggerSchema("Sepetteki kurs miktarı.")]
    [DefaultValue(1)]
    public int Quantity { get; set; } = 1;
}
