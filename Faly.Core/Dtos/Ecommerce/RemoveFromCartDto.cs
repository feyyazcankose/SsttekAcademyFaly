using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace Faly.Core.Dtos.Ecommerce;

public class RemoveFromCartDto
{
    [Required(ErrorMessage = "Kurs ID'si gereklidir.")]
    [SwaggerSchema("Sepetten çıkarmak istediğiniz kursun benzersiz tanımlayıcısı.")]
    [DefaultValue(1)]
    public int CourseId { get; set; }
}
