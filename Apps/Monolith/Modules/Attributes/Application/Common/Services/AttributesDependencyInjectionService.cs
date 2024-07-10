using Bigpods.Monolith.Modules.Attributes.Application.CreateOne.Services;
using Bigpods.Monolith.Modules.Attributes.Application.DeleteOne.Services;
using Bigpods.Monolith.Modules.Attributes.Domain.CreateOne.Services;
using Bigpods.Monolith.Modules.Attributes.Domain.DeleteOne.Services;

namespace Bigpods.Monolith.Modules.Attributes.Application.Common.Services;

public static class AttributesDependencyInjectionService
{
    public static IServiceCollection AddAttributesDependenciesConfiguration(
        this IServiceCollection services
    )
    {
        services.AddScoped<ICreateOneAttributeService, CreateOneAttributeService>();
        services.AddScoped<IDeleteOneAttributeService, DeleteOneAttributeService>();

        return services;
    }
}
