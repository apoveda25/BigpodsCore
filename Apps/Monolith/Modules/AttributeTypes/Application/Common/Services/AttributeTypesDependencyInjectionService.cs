using Bigpods.Monolith.Modules.AttributeTypes.Application.CreateOne.Services;
using Bigpods.Monolith.Modules.AttributeTypes.Application.UpdateOne.Services;
using Bigpods.Monolith.Modules.AttributeTypes.Domain.CreateOne.Services;
using Bigpods.Monolith.Modules.AttributeTypes.Domain.UpdateOne.Services;

namespace Bigpods.Monolith.Modules.AttributeTypes.Application.Common.Services;

public static class AttributeTypesDependencyInjectionService
{
    public static IServiceCollection AddAttributeTypesDependenciesConfiguration(
        this IServiceCollection services
    )
    {
        services.AddScoped<ICreateOneAttributeTypeService, CreateOneAttributeTypeService>();
        services.AddScoped<IUpdateOneAttributeTypeService, UpdateOneAttributeTypeService>();

        return services;
    }
}
