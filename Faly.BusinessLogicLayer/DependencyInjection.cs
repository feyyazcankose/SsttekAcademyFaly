using Faly.BussinessLogicLayer.Interfaces;
using Faly.BussinessLogicLayer.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Faly.BussinessLogicLayer;

public static class DependencyInjection
{
    public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services)
    {
        // Add Business Logic Services
        services.AddScoped<IAdminUserService, AdminUserService>();
        services.AddScoped<IAdminCourseService, AdminCourseService>();
        services.AddScoped<IAdminOrderService, AdminOrderService>();
        services.AddScoped<IAdminUserOrderCourseService, AdminUserOrderCourseService>();
        services.AddScoped<IAdminAccountService, AdminAccountService>();

        return services;
    }
}
