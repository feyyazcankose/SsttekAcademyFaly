using System.ComponentModel;
using Swashbuckle.AspNetCore.Annotations;

namespace Faly.Core.Dtos.Ecommerce;

public class AuthResponseDto
{
    [SwaggerSchema("Unique identifier of the access token.")]
    [DefaultValue("123e4567-e89b-12d3-a456-426614174000")]
    public string AccessToken { get; set; } = default!;
}
