using Faly.Api.Middlewares;
using Faly.BussinessLogicLayer;
using Faly.Core;
using Faly.DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseAuthorization();
app.MapControllers();
app.UseHttpsRedirection();
app.Run();
