using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Faly.Core;

public class AuthorizeCheckOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        // Controller veya Action seviyesinde [Authorize] attribüsü var mı kontrol edin
        var hasAuthorize =
            context
                .MethodInfo.DeclaringType.GetCustomAttributes(true)
                .OfType<AuthorizeAttribute>()
                .Any()
            || context.MethodInfo.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any();

        if (hasAuthorize)
        {
            // Güvenlik gereksinimini ekleyin
            operation.Security = new List<OpenApiSecurityRequirement>
            {
                new OpenApiSecurityRequirement
                {
                    {
                        // Tanımladığımız güvenlik şemasını referans alın
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                        },
                        new string[] { } // Scopes, gerekirse ekleyebilirsiniz
                    },
                },
            };
        }
    }
}
