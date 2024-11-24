using Faly.Api.Middlewares;
using Faly.BussinessLogicLayer;
using Faly.Core;
using Faly.DataAccessLayer;
using Faly.DataAccessLayer.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Faly API", Version = "v1" });
    options.TagActionsBy(api => new[] { api.GroupName });
    options.DocInclusionPredicate((name, api) => true);
    options.EnableAnnotations();

    // Diğer Swagger yapılandırmaları...
});
builder.Services.AddDataAccessLayer(
    builder.Configuration.GetConnectionString("PostgreSql"),
    builder.Configuration.GetSection("JwtSettings")
);
builder.Services.AddBusinessLogicLayer();

//Model doğrulama hataları için
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var problemDetails = new ValidationProblemDetails(context.ModelState)
        {
            Type = "https://yourdomain.com/model-validation-error",
            Title = "Doğrulama hatası.",
            Status = StatusCodes.Status400BadRequest,
            Detail = "Lütfen gönderdiğiniz verileri kontrol edin.",
            Instance = context.HttpContext.Request.Path,
        };

        return new BadRequestObjectResult(problemDetails)
        {
            ContentTypes = { "application/problem+json" },
        };
    };
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition(
        "Bearer",
        new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description =
                "JWT Bearer Token ile yetkilendirme.\r\n\r\n"
                + "Token'ı 'Bearer {token}' formatında giriniz. Örneğin: 'Bearer eyJhbGciOi...'",
        }
    );

    options.OperationFilter<AuthorizeCheckOperationFilter>();
    options.EnableAnnotations();
});

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    await AppDbInitialize.InitializeRoles(serviceProvider);
    await AppDbInitialize.InitializeUser(serviceProvider);
    await AppDbInitialize.InitializeCourse(context);
}

app.Use(
    async (context, next) =>
    {
        var authorizationHeader = context.Request.Headers["Authorization"].FirstOrDefault();

        if (
            !string.IsNullOrEmpty(authorizationHeader)
            && !authorizationHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase)
        )
        {
            context.Request.Headers["Authorization"] = $"Bearer {authorizationHeader}";
        }

        await next();
    }
);

app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication(); // JWT Middleware önce çalıştırılmalı
app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();
app.UseHttpsRedirection();
app.Run();
