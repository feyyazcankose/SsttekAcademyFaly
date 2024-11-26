using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace Faly.Core.Dtos.Ecommerce;

public class AddToCartDto
{
    [Required(ErrorMessage = "Kurs ID'si gereklidir.")]
    [SwaggerSchema("Sepete eklemek istediğiniz kursun benzersiz tanımlayıcısı.")]
    [DefaultValue(1)]
    public int CourseId { get; set; }

    [SwaggerSchema("Sepete eklemek istediğiniz kurs miktarı.")]
    [DefaultValue(1)]
    public int Quantity { get; set; } = 1;
}
