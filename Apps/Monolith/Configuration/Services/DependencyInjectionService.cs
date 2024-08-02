using System.Reflection;
using Bigpods.Monolith.Modules.Attributes.Application.Common.Policies;
using Bigpods.Monolith.Modules.Attributes.Application.Common.Services;
using Bigpods.Monolith.Modules.Attributes.Infrastructure.CreateOne.Mutations;
using Bigpods.Monolith.Modules.Attributes.Infrastructure.DeleteOne.Mutations;
using Bigpods.Monolith.Modules.Attributes.Infrastructure.FindMany.Queries;
using Bigpods.Monolith.Modules.Attributes.Infrastructure.FindOne.Queries;
using Bigpods.Monolith.Modules.AttributeTypes.Application.Common.Policies;
using Bigpods.Monolith.Modules.AttributeTypes.Application.Common.Services;
using Bigpods.Monolith.Modules.AttributeTypes.Infrastructure.CreateOne.Mutations;
using Bigpods.Monolith.Modules.AttributeTypes.Infrastructure.FindMany.Queries;
using Bigpods.Monolith.Modules.AttributeTypes.Infrastructure.FindOne.Queries;
using Bigpods.Monolith.Modules.AttributeTypes.Infrastructure.UpdateOne.Mutations;
using Bigpods.Monolith.Modules.Inventories.Application.Common.Policies;
using Bigpods.Monolith.Modules.Inventories.Application.Common.Services;
using Bigpods.Monolith.Modules.Inventories.Infrastructure.CreateOne.Mutations;
using Bigpods.Monolith.Modules.Inventories.Infrastructure.DeleteOne.Mutations;
using Bigpods.Monolith.Modules.Inventories.Infrastructure.FindMany.Queries;
using Bigpods.Monolith.Modules.Inventories.Infrastructure.FindOne.Queries;
using Bigpods.Monolith.Modules.InventoryInputs.Application.Common.Policies;
using Bigpods.Monolith.Modules.InventoryInputs.Application.Common.Services;
using Bigpods.Monolith.Modules.InventoryInputs.Infrastructure.CreateOne.Mutations;
using Bigpods.Monolith.Modules.InventoryInputs.Infrastructure.FindMany.Queries;
using Bigpods.Monolith.Modules.InventoryInputs.Infrastructure.FindOne.Queries;
using Bigpods.Monolith.Modules.InventoryOutputs.Application.Common.Policies;
using Bigpods.Monolith.Modules.InventoryOutputs.Application.Common.Services;
using Bigpods.Monolith.Modules.InventoryOutputs.Infrastructure.CreateOne.Mutations;
using Bigpods.Monolith.Modules.InventoryOutputs.Infrastructure.FindMany.Queries;
using Bigpods.Monolith.Modules.InventoryOutputs.Infrastructure.FindOne.Queries;
using Bigpods.Monolith.Modules.Products.Application.Common.Policies;
using Bigpods.Monolith.Modules.Products.Application.Common.Services;
using Bigpods.Monolith.Modules.Products.Infrastructure.CreateOne.Mutations;
using Bigpods.Monolith.Modules.Products.Infrastructure.FindMany.Queries;
using Bigpods.Monolith.Modules.Products.Infrastructure.FindOne.Queries;
using Bigpods.Monolith.Modules.Products.Infrastructure.UpdateOne.Mutations;
using Bigpods.Monolith.Modules.Shared.Domain.Database;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Database;
using Bigpods.Monolith.Modules.Variants.Application.Common.Policies;
using Bigpods.Monolith.Modules.Variants.Application.Common.Services;
using Bigpods.Monolith.Modules.Variants.Infrastructure.CreateOne.Mutations;
using Bigpods.Monolith.Modules.Variants.Infrastructure.DeleteOne.Mutations;
using Bigpods.Monolith.Modules.Variants.Infrastructure.FindMany.Queries;
using Bigpods.Monolith.Modules.Variants.Infrastructure.FindOne.Queries;
using Bigpods.Monolith.Modules.Variants.Infrastructure.UpdateOne.Mutations;
using Bigpods.Monolith.Modules.VariantsOnAttributes.Application.Common.Policies;
using Bigpods.Monolith.Modules.VariantsOnAttributes.Application.Common.Services;
using Bigpods.Monolith.Modules.VariantsOnAttributes.Infrastructure.AttachMany.Mutations;
using Bigpods.Monolith.Modules.VariantsOnAttributes.Infrastructure.DettachMany.Mutations;
using Bigpods.Monolith.Modules.VariantsOnAttributes.Infrastructure.FindMany.Queries;
using Bigpods.Monolith.Modules.VariantsOnAttributes.Infrastructure.FindOne.Queries;
using Bigpods.Monolith.Modules.Warehouses.Application.Common.Policies;
using Bigpods.Monolith.Modules.Warehouses.Application.Common.Services;
using Bigpods.Monolith.Modules.Warehouses.Infrastructure.CreateOne.Mutations;
using Bigpods.Monolith.Modules.Warehouses.Infrastructure.DeleteOne.Mutations;
using Bigpods.Monolith.Modules.Warehouses.Infrastructure.FindMany.Queries;
using Bigpods.Monolith.Modules.Warehouses.Infrastructure.FindOne.Queries;
using Bigpods.Monolith.Modules.Warehouses.Infrastructure.UpdateOne.Mutations;
using HotChocolate.Types.NodaTime;
using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;
using Keycloak.AuthServices.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;

namespace Bigpods.Monolith.Configuration.Services;

public static class CoreDependencyInjectionService
{
    public static IServiceCollection AddLoggingConfiguration(
        this IServiceCollection services,
        ILoggingBuilder builder
    )
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

    public static IServiceCollection AddPersistenceConfiguration(this IServiceCollection services)
    {
        string databaseConnectionString = DotNetEnv.Env.GetString("DATABASE_CONNECTION_URL");

        services.AddDbContext<DatabaseService>(optionsAction: options =>
            options.UseMySql(
                connectionString: databaseConnectionString,
                serverVersion: ServerVersion.AutoDetect(connectionString: databaseConnectionString),
                mySqlOptionsAction: x =>
                    x.MigrationsHistoryTable(tableName: HistoryRepository.DefaultTableName)
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
            .AddTypeExtension<CreateOneAttributeTypeMutation>()
            .AddTypeExtension<UpdateOneAttributeTypeMutation>()
            .AddTypeExtension<CreateOneAttributeMutation>()
            .AddTypeExtension<DeleteOneAttributeMutation>()
            .AddTypeExtension<AttachManyVariantOnAttributeMutation>()
            .AddTypeExtension<DettachManyVariantOnAttributeMutation>()
            .AddTypeExtension<CreateOneVariantMutation>()
            .AddTypeExtension<UpdateOneVariantMutation>()
            .AddTypeExtension<DeleteOneVariantMutation>()
            .AddTypeExtension<CreateOneProductMutation>()
            .AddTypeExtension<UpdateOneProductMutation>()
            .AddTypeExtension<CreateOneInventoryInputMutation>()
            .AddTypeExtension<CreateOneInventoryOutputMutation>()
            .AddTypeExtension<CreateOneInventoryMutation>()
            .AddTypeExtension<DeleteOneInventoryMutation>()
            .AddTypeExtension<CreateOneWarehouseMutation>()
            .AddTypeExtension<UpdateOneWarehouseMutation>()
            .AddTypeExtension<DeleteOneWarehouseMutation>()
            .AddQueryType()
            .AddTypeExtension<FindManyAttributeTypesQuery>()
            .AddTypeExtension<FindOneAttributeTypeQuery>()
            .AddTypeExtension<FindManyAttributesQuery>()
            .AddTypeExtension<FindOneAttributeQuery>()
            .AddTypeExtension<FinaManyVariantsOnAttributesQuery>()
            .AddTypeExtension<FindOneVariantOnAttributeQuery>()
            .AddTypeExtension<FinaManyVariantsQuery>()
            .AddTypeExtension<FindOneVariantQuery>()
            .AddTypeExtension<FindManyProductsQuery>()
            .AddTypeExtension<FindOneProductQuery>()
            .AddTypeExtension<FindManyInventoryInputsQuery>()
            .AddTypeExtension<FindOneInventoryInputQuery>()
            .AddTypeExtension<FindManyInventoryOutputsQuery>()
            .AddTypeExtension<FindOneInventoryOutputQuery>()
            .AddTypeExtension<FindManyInventoriesQuery>()
            .AddTypeExtension<FindOneInventoryQuery>()
            .AddTypeExtension<FindManyWarehousesQuery>()
            .AddTypeExtension<FindOneWarehouseQuery>()
            .AddType<DateTimeZoneType>()
            .AddProjections()
            .AddFiltering()
            .AddSorting()
            .AddMutationConventions(
                new MutationConventionOptions
                {
                    InputArgumentName = "input",
                    InputTypeNamePattern = "{MutationName}Input",
                    PayloadTypeNamePattern = "{MutationName}Payload",
                    PayloadErrorTypeNamePattern = "{MutationName}Error",
                    PayloadErrorsFieldName = "errors",
                    ApplyToAllMutations = true,
                }
            )
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
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(assembly: Assembly.GetExecutingAssembly())
        );

        return services;
    }

    public static IServiceCollection AddAuthenticationConfiguration(
        this IServiceCollection services
    )
    {
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddKeycloakWebApi(options =>
            {
                options.Realm = DotNetEnv.Env.GetString("OIDC_REALM");
                options.AuthServerUrl = DotNetEnv.Env.GetString("OIDC_URL");
                options.SslRequired = DotNetEnv.Env.GetString("OIDC_SSL");
                options.Resource = DotNetEnv.Env.GetString("OIDC_CLIENT_ID");
                options.VerifyTokenAudience = true;
                options.Credentials = new KeycloakClientInstallationCredentials
                {
                    Secret = DotNetEnv.Env.GetString("OIDC_SECRET")
                };
            });

        return services;
    }

    public static IServiceCollection AddAuthorizationConfiguration(this IServiceCollection services)
    {
        services
            .AddAuthorization(configure: AttributeTypesPolicies.ConfigurePolicies)
            .AddAuthorization(configure: AttributesPolicies.ConfigurePolicies)
            .AddAuthorization(configure: VariantsOnAttributesPolicies.ConfigurePolicies)
            .AddAuthorization(configure: VariantsPolicies.ConfigurePolicies)
            .AddAuthorization(configure: ProductsPolicies.ConfigurePolicies)
            .AddAuthorization(configure: InventoryInputsPolicies.ConfigurePolicies)
            .AddAuthorization(configure: InventoryOutputsPolicies.ConfigurePolicies)
            .AddAuthorization(configure: InventoriesPolicies.ConfigurePolicies)
            .AddAuthorization(configure: WarehousesPolicies.ConfigurePolicies)
            .AddKeycloakAuthorization();

        services.AddAuthorizationServer(
            (options) =>
            {
                options.Realm = DotNetEnv.Env.GetString("OIDC_REALM");
                options.AuthServerUrl = DotNetEnv.Env.GetString("OIDC_URL");
                options.SslRequired = DotNetEnv.Env.GetString("OIDC_SSL");
                options.Resource = DotNetEnv.Env.GetString("OIDC_CLIENT_ID");
                options.VerifyTokenAudience = true;
                options.Credentials = new KeycloakClientInstallationCredentials
                {
                    Secret = DotNetEnv.Env.GetString("OIDC_SECRET")
                };
            }
        );

        return services;
    }

    public static IServiceCollection AddModulesConfiguration(this IServiceCollection services)
    {
        return services
            .AddAttributeTypesDependenciesConfiguration()
            .AddAttributesDependenciesConfiguration()
            .AddVariantsOnAttributesDependenciesConfiguration()
            .AddVariantsDependenciesConfiguration()
            .AddProductsDependenciesConfiguration()
            .AddInventoryInputsDependenciesConfiguration()
            .AddInventoryOutputsDependenciesConfiguration()
            .AddInventoriesDependenciesConfiguration()
            .AddWarehousesDependenciesConfiguration();
    }
}
