using Bigpods.Monolith.Modules.Warehouses.Application.CreateOne.Services;
using Bigpods.Monolith.Modules.Warehouses.Application.DeleteOne.Services;
using Bigpods.Monolith.Modules.Warehouses.Application.UpdateOne.Services;
using Bigpods.Monolith.Modules.Warehouses.Domain.CreateOne.Services;
using Bigpods.Monolith.Modules.Warehouses.Domain.DeleteOne.Services;
using Bigpods.Monolith.Modules.Warehouses.Domain.UpdateOne.Services;

namespace Bigpods.Monolith.Modules.Warehouses.Application.Common.Services;

public static class WarehousesDependencyInjectionService
{
    public static IServiceCollection AddWarehousesDependenciesConfiguration(this IServiceCollection services)
    {
        services.AddScoped<ICreateOneWarehouseService, CreateOneWarehouseService>();
        services.AddScoped<IUpdateOneWarehouseService, UpdateOneWarehouseService>();
        services.AddScoped<IDeleteOneWarehouseService, DeleteOneWarehouseService>();

        return services;
    }
}
