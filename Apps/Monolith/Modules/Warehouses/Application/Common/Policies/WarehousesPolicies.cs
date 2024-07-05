using Bigpods.Monolith.Modules.Shared.Application.Policies;
using Bigpods.Monolith.Modules.Shared.Domain.Policies;

using Keycloak.AuthServices.Authorization;

using Microsoft.AspNetCore.Authorization;

namespace Bigpods.Monolith.Modules.Warehouses.Application.Common.Policies;

public sealed class WarehousesPolicies
{
    private const string Resource = "resources:warehouses";

    public const string CreateOneWarehousesPolicy = "CreateOneWarehousesPolicy";
    public const string UpdateOneWarehousesPolicy = "UpdateOneWarehousesPolicy";
    public const string DeleteOneWarehousesPolicy = "DeleteOneWarehousesPolicy";
    public const string ReadOneWarehousesPolicy = "ReadOneWarehousesPolicy";
    public const string ReadManyWarehousesPolicy = "ReadManyWarehousesPolicy";

    private static readonly IBasePolicy CreateOneWarehouses = new CreateOnePolicy(Name: CreateOneWarehousesPolicy, Resource: Resource);
    private static readonly IBasePolicy UpdateOneWarehouses = new UpdateOnePolicy(Name: UpdateOneWarehousesPolicy, Resource: Resource);
    private static readonly IBasePolicy DeleteOneWarehouses = new DeleteOnePolicy(Name: DeleteOneWarehousesPolicy, Resource: Resource);
    private static readonly IBasePolicy ReadOneWarehouses = new ReadOnePolicy(Name: ReadOneWarehousesPolicy, Resource: Resource);
    private static readonly IBasePolicy ReadManyWarehouses = new ReadManyPolicy(Name: ReadManyWarehousesPolicy, Resource: Resource);

    public static readonly Action<AuthorizationOptions> ConfigurePolicies = (AuthorizationOptions options) =>
    {
        options.AddPolicy(
            name: CreateOneWarehouses.Name,
            configurePolicy: builder => builder.RequireProtectedResource(resource: CreateOneWarehouses.Resource, scope: CreateOneWarehouses.Scope)
        );
        options.AddPolicy(
            name: UpdateOneWarehouses.Name,
            configurePolicy: builder => builder.RequireProtectedResource(resource: UpdateOneWarehouses.Resource, scope: UpdateOneWarehouses.Scope)
        );
        options.AddPolicy(
            name: DeleteOneWarehouses.Name,
            configurePolicy: builder => builder.RequireProtectedResource(resource: DeleteOneWarehouses.Resource, scope: DeleteOneWarehouses.Scope)
        );
        options.AddPolicy(
            name: ReadOneWarehouses.Name,
            configurePolicy: builder => builder.RequireProtectedResource(resource: ReadOneWarehouses.Resource, scope: ReadOneWarehouses.Scope)
        );
        options.AddPolicy(
            name: ReadManyWarehouses.Name,
            configurePolicy: builder => builder.RequireProtectedResource(resource: ReadManyWarehouses.Resource, scope: ReadManyWarehouses.Scope)
        );
    };
}
