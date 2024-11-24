using System.Text;
using Faly.DataAccessLayer.Data;
using Faly.DataAccessLayer.Entities;
using Faly.DataAccessLayer.Interfaces;
using Faly.DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Faly.DataAccessLayer;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccessLayer(
        this IServiceCollection services,
        string connectionString,
        IConfigurationSection jwtSettings
    )
    {
        // PostgreSQL DbContext ayarı
        services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

        services
            .AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        // JWT ayarlarını yapılandırması
        services.Configure<JwtSettings>(jwtSettings);

        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings["SecretKey"])
                    ),
                };
            });

        // Repositories
        services.AddScoped<IAdminRepository<ApplicationUser>, AdminRepository<ApplicationUser>>();
        services.AddScoped<IAdminRepository<Order>, AdminRepository<Order>>();
        services.AddScoped<IAdminRepository<Course>, AdminRepository<Course>>();
        services.AddScoped<IAdminRepository<Payment>, AdminRepository<Payment>>();
        services.AddScoped<IAdminRepository<OrderDetail>, AdminRepository<OrderDetail>>();

        return services;
    }
}
