using Bigpods.Monolith.Modules.Products.Application.CreateOne.Services;
using Bigpods.Monolith.Modules.Products.Application.UpdateOne.Services;
using Bigpods.Monolith.Modules.Products.Domain.CreateOne.Services;
using Bigpods.Monolith.Modules.Products.Domain.UpdateOne.Services;

namespace Bigpods.Monolith.Modules.Products.Application.Common.Services;

public static class ProductsDependencyInjectionService
{
    public static IServiceCollection AddProductsDependenciesConfiguration(this IServiceCollection services)
    {
        services.AddScoped<ICreateOneProductService, CreateOneProductService>();
        services.AddScoped<IUpdateOneProductService, UpdateOneProductService>();

        return services;
    }
}
