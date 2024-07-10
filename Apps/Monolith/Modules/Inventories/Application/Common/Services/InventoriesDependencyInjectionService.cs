using Bigpods.Monolith.Modules.Inventories.Application.CreateOne.Services;
using Bigpods.Monolith.Modules.Inventories.Application.DeleteOne.Services;
using Bigpods.Monolith.Modules.Inventories.Domain.CreateOne.Services;
using Bigpods.Monolith.Modules.Inventories.Domain.DeleteOne.Services;

namespace Bigpods.Monolith.Modules.Inventories.Application.Common.Services;

public static class InventoriesDependencyInjectionService
{
    public static IServiceCollection AddInventoriesDependenciesConfiguration(
        this IServiceCollection services
    )
    {
        services.AddScoped<ICreateOneInventoryService, CreateOneInventoryService>();
        services.AddScoped<IDeleteOneInventoryService, DeleteOneInventoryService>();

        return services;
    }
}
