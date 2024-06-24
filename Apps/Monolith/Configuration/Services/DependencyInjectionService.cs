using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;

using Bigpods.Monolith.Modules.Shared.Infrastructure.Database;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Bigpods.Monolith.Modules.Attributes.Application.Common.Policies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;
using OpenTelemetry.Logs;
using HotChocolate.Types.NodaTime;
using Bigpods.Monolith.Modules.Attributes.Infrastructure.CreateOne.Mutations;
using Bigpods.Monolith.Modules.Attributes.Infrastructure.DeleteOne.Mutations;
using Bigpods.Monolith.Modules.Shared.Domain.Database;
using Bigpods.Monolith.Modules.Variants.Infrastructure.CreateOne.Mutations;
using Bigpods.Monolith.Modules.Products.Infrastructure.CreateOne.Mutations;
using Bigpods.Monolith.Modules.Variants.Application.Common.Policies;
using Bigpods.Monolith.Modules.Products.Application.Common.Policies;
using System.Reflection;
using Bigpods.Monolith.Modules.Products.Infrastructure.UpdateOne.Mutations;
using Bigpods.Monolith.Modules.Products.Infrastructure.FindMany.Queries;
using Bigpods.Monolith.Modules.Products.Infrastructure.FindOne.Queries;
using Bigpods.Monolith.Modules.Variants.Infrastructure.UpdateOne.Mutations;
using Bigpods.Monolith.Modules.Variants.Infrastructure.FindMany.Queries;
using Bigpods.Monolith.Modules.Variants.Infrastructure.FindOne.Queries;
using Bigpods.Monolith.Modules.Variants.Infrastructure.DeleteOne.Mutations;
using Bigpods.Monolith.Modules.AttributeTypes.Infrastructure.CreateOne.Mutations;
using Bigpods.Monolith.Modules.AttributeTypes.Application.Common.Policies;
using Bigpods.Monolith.Modules.AttributeTypes.Infrastructure.FindMany.Queries;
using Bigpods.Monolith.Modules.AttributeTypes.Infrastructure.FindOne.Queries;
using Bigpods.Monolith.Modules.AttributeTypes.Infrastructure.UpdateOne.Mutations;
using Bigpods.Monolith.Modules.Attributes.Infrastructure.FindMany.Queries;
using Bigpods.Monolith.Modules.Attributes.Infrastructure.FindOne.Queries;


namespace Bigpods.Monolith.Config.Services;

public static class CoreDependencyInjectionService
{
    public static IServiceCollection AddLoggingConfiguration(this IServiceCollection services, ILoggingBuilder builder)
    {
        builder.AddOpenTelemetry(logging =>
        {
            logging.IncludeFormattedMessage = true;
            logging.IncludeScopes = true;
            logging.AddOtlpExporter();
            logging.AddConsoleExporter();
        });

        return services;
    }

    public static IServiceCollection AddOpenTelemetryConfiguration(this IServiceCollection services)
    {
        services
            .ConfigureOpenTelemetryMeterProvider(metrics =>
                metrics
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddKeycloakAuthServicesInstrumentation()
            )
            .ConfigureOpenTelemetryTracerProvider(tracing =>
                tracing
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddKeycloakAuthServicesInstrumentation()
            );

        return services;
    }

    public static IServiceCollection AddPersistenceConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var databaseConnectionString = configuration.GetConnectionString(name: "BigpodsConnectionUrl");

        services.AddDbContext<DatabaseService>(
            optionsAction: options => options.UseMySql(
                connectionString: databaseConnectionString,
                serverVersion: ServerVersion.AutoDetect(connectionString: databaseConnectionString),
                mySqlOptionsAction: x => x.MigrationsHistoryTable(tableName: HistoryRepository.DefaultTableName)
            )
        );

        services.AddScoped<DatabaseService, DatabaseService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }

    public static IServiceCollection AddGraphQLConfiguration(this IServiceCollection services)
    {
        services.AddSha256DocumentHashProvider();

        services
            .AddGraphQLServer()
            .AddAuthorization()
            .RegisterDbContext<DatabaseService>()
            .AddMaxExecutionDepthRule(maxAllowedExecutionDepth: 11)
            .AddMutationType()
            .AddTypeExtension<CreateOneAttributeTypesMutation>()
            .AddTypeExtension<UpdateOneAttributeTypeMutation>()
            .AddTypeExtension<CreateOneAttributesMutation>()
            .AddTypeExtension<DeleteOneAttributesMutation>()
            .AddTypeExtension<CreateOneVariantsMutation>()
            .AddTypeExtension<UpdateOneVariantsMutation>()
            .AddTypeExtension<DeleteOneVariantsMutation>()
            .AddTypeExtension<CreateOneProductsMutation>()
            .AddTypeExtension<UpdateOneProductsMutation>()
            .AddQueryType()
            .AddTypeExtension<FindManyAttributeTypesQuery>()
            .AddTypeExtension<FindOneAttributeTypesQuery>()
            .AddTypeExtension<FindManyAttributesQuery>()
            .AddTypeExtension<FindOneAttributesQuery>()
            .AddTypeExtension<FinaManyVariantsQuery>()
            .AddTypeExtension<FindOneVariantsQuery>()
            .AddTypeExtension<FindManyProductsQuery>()
            .AddTypeExtension<FindOneProductsQuery>()
            .AddType<DateTimeZoneType>()
            .AddProjections()
            .AddFiltering()
            .AddSorting()
            .InitializeOnStartup();

        return services;
    }

    public static IServiceCollection AddMappersConfiguration(this IServiceCollection services)
    {
        services.AddAutoMapper(assemblies: Assembly.GetExecutingAssembly());

        return services;
    }

    public static IServiceCollection AddMediatorConfiguration(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly: Assembly.GetExecutingAssembly()));

        return services;
    }

    public static IServiceCollection AddAuthenticationConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddKeycloakWebApi(
                configuration: configuration,
                configSectionName: KeycloakAuthenticationOptions.Section
            );

        return services;
    }

    public static IServiceCollection AddAuthorizationConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddAuthorization(configure: AttributeTypesPolicies.ConfigurePolicies)
            .AddAuthorization(configure: AttributesPolicies.ConfigurePolicies)
            .AddAuthorization(configure: VariantsPolicies.ConfigurePolicies)
            .AddAuthorization(configure: ProductsPolicies.ConfigurePolicies)
            .AddKeycloakAuthorization();

        services.AddAuthorizationServer(
            configuration: configuration,
            configSectionName: KeycloakAuthenticationOptions.Section
        );

        return services;
    }
}