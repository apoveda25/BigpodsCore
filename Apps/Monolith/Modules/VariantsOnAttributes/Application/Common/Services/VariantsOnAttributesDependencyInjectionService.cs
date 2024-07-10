using Bigpods.Monolith.Modules.VariantsOnAttributes.Application.AttachMany.Services;
using Bigpods.Monolith.Modules.VariantsOnAttributes.Application.DettachMany.Services;
using Bigpods.Monolith.Modules.VariantsOnAttributes.Domain.AttachMany.Services;
using Bigpods.Monolith.Modules.VariantsOnAttributes.Domain.DettachMany.Services;

namespace Bigpods.Monolith.Modules.VariantsOnAttributes.Application.Common.Services;

public static class VariantsOnAttributesDependencyInjectionService
{
    public static IServiceCollection AddVariantsOnAttributesDependenciesConfiguration(
        this IServiceCollection services
    )
    {
        services.AddScoped<
            IAttachManyVariantOnAttributeService,
            AttachManyVariantOnAttributeService
        >();
        services.AddScoped<
            IDettachManyVariantOnAttributeService,
            DettachManyVariantOnAttributeService
        >();

        return services;
    }
}
