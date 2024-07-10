using Bigpods.Monolith.Modules.InventoryInputs.Application.CreateOne.Services;
using Bigpods.Monolith.Modules.InventoryInputs.Domain.CreateOne.Services;

namespace Bigpods.Monolith.Modules.InventoryInputs.Application.Common.Services;

public static class InventoryInputsDependencyInjectionService
{
    public static IServiceCollection AddInventoryInputsDependenciesConfiguration(this IServiceCollection services)
    {
        services.AddScoped<ICreateOneInventoryInputService, CreateOneInventoryInputService>();

        return services;
    }
}
