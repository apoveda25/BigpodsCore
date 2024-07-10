using Bigpods.Monolith.Modules.InventoryOutputs.Application.CreateOne.Services;
using Bigpods.Monolith.Modules.InventoryOutputs.Domain.CreateOne.Services;

namespace Bigpods.Monolith.Modules.InventoryOutputs.Application.Common.Services;

public static class InventoryOutputsDependencyInjectionService
{
    public static IServiceCollection AddInventoryOutputsDependenciesConfiguration(
        this IServiceCollection services
    )
    {
        services.AddScoped<ICreateOneInventoryOutputService, CreateOneInventoryOutputService>();

        return services;
    }
}
