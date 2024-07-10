using Bigpods.Monolith.Modules.Shared.Application.Policies;
using Bigpods.Monolith.Modules.Shared.Domain.Policies;

using Keycloak.AuthServices.Authorization;

using Microsoft.AspNetCore.Authorization;

namespace Bigpods.Monolith.Modules.InventoryOutputs.Application.Common.Policies;

public sealed class InventoryOutputsPolicies
{
    private const string Resource = "resources:inventory-outputs";

    public const string CreateOneInventoryOutputsPolicy = "CreateOneInventoryOutputsPolicy";
    public const string UpdateOneInventoryOutputsPolicy = "UpdateOneInventoryOutputsPolicy";
    public const string DeleteOneInventoryOutputsPolicy = "DeleteOneInventoryOutputsPolicy";
    public const string ReadOneInventoryOutputsPolicy = "ReadOneInventoryOutputsPolicy";
    public const string ReadManyInventoryOutputsPolicy = "ReadManyInventoryOutputsPolicy";

    private static readonly IBasePolicy CreateOneInventoryOutputs = new CreateOnePolicy(Name: CreateOneInventoryOutputsPolicy, Resource: Resource);
    private static readonly IBasePolicy UpdateOneInventoryOutputs = new UpdateOnePolicy(Name: UpdateOneInventoryOutputsPolicy, Resource: Resource);
    private static readonly IBasePolicy DeleteOneInventoryOutputs = new DeleteOnePolicy(Name: DeleteOneInventoryOutputsPolicy, Resource: Resource);
    private static readonly IBasePolicy ReadOneInventoryOutputs = new ReadOnePolicy(Name: ReadOneInventoryOutputsPolicy, Resource: Resource);
    private static readonly IBasePolicy ReadManyInventoryOutputs = new ReadManyPolicy(Name: ReadManyInventoryOutputsPolicy, Resource: Resource);

    public static readonly Action<AuthorizationOptions> ConfigurePolicies = (AuthorizationOptions options) =>
    {
        options.AddPolicy(
            name: CreateOneInventoryOutputs.Name,
            configurePolicy: builder => builder.RequireProtectedResource(resource: CreateOneInventoryOutputs.Resource, scope: CreateOneInventoryOutputs.Scope)
        );
        options.AddPolicy(
            name: UpdateOneInventoryOutputs.Name,
            configurePolicy: builder => builder.RequireProtectedResource(resource: UpdateOneInventoryOutputs.Resource, scope: UpdateOneInventoryOutputs.Scope)
        );
        options.AddPolicy(
            name: DeleteOneInventoryOutputs.Name,
            configurePolicy: builder => builder.RequireProtectedResource(resource: DeleteOneInventoryOutputs.Resource, scope: DeleteOneInventoryOutputs.Scope)
        );
        options.AddPolicy(
            name: ReadOneInventoryOutputs.Name,
            configurePolicy: builder => builder.RequireProtectedResource(resource: ReadOneInventoryOutputs.Resource, scope: ReadOneInventoryOutputs.Scope)
        );
        options.AddPolicy(
            name: ReadManyInventoryOutputs.Name,
            configurePolicy: builder => builder.RequireProtectedResource(resource: ReadManyInventoryOutputs.Resource, scope: ReadManyInventoryOutputs.Scope)
        );
    };
}
