using Bigpods.Monolith.Modules.Variants.Application.CreateOne.Services;
using Bigpods.Monolith.Modules.Variants.Application.DeleteOne.Services;
using Bigpods.Monolith.Modules.Variants.Application.UpdateOne.Services;
using Bigpods.Monolith.Modules.Variants.Domain.CreateOne.Services;
using Bigpods.Monolith.Modules.Variants.Domain.DeleteOne.Services;
using Bigpods.Monolith.Modules.Variants.Domain.UpdateOne.Services;

namespace Bigpods.Monolith.Modules.Variants.Application.Common.Services;

public static class VariantsDependencyInjectionService
{
    public static IServiceCollection AddVariantsDependenciesConfiguration(this IServiceCollection services)
    {
        services.AddScoped<ICreateOneVariantService, CreateOneVariantService>();
        services.AddScoped<IUpdateOneVariantService, UpdateOneVariantService>();
        services.AddScoped<IDeleteOneVariantService, DeleteOneVariantService>();

        return services;
    }
}
