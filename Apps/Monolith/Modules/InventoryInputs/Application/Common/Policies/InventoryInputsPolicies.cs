using Bigpods.Monolith.Modules.Shared.Application.Policies;
using Bigpods.Monolith.Modules.Shared.Domain.Policies;

using Keycloak.AuthServices.Authorization;

using Microsoft.AspNetCore.Authorization;

namespace Bigpods.Monolith.Modules.InventoryInputs.Application.Common.Policies;

public sealed class InventoryInputsPolicies
{
    private const string Resource = "resources:inventory-inputs";

    public const string CreateOneInventoryInputsPolicy = "CreateOneInventoryInputsPolicy";
    public const string UpdateOneInventoryInputsPolicy = "UpdateOneInventoryInputsPolicy";
    public const string DeleteOneInventoryInputsPolicy = "DeleteOneInventoryInputsPolicy";
    public const string ReadOneInventoryInputsPolicy = "ReadOneInventoryInputsPolicy";
    public const string ReadManyInventoryInputsPolicy = "ReadManyInventoryInputsPolicy";

    private static readonly IBasePolicy CreateOneInventoryInputs = new CreateOnePolicy(Name: CreateOneInventoryInputsPolicy, Resource: Resource);
    private static readonly IBasePolicy UpdateOneInventoryInputs = new UpdateOnePolicy(Name: UpdateOneInventoryInputsPolicy, Resource: Resource);
    private static readonly IBasePolicy DeleteOneInventoryInputs = new DeleteOnePolicy(Name: DeleteOneInventoryInputsPolicy, Resource: Resource);
    private static readonly IBasePolicy ReadOneInventoryInputs = new ReadOnePolicy(Name: ReadOneInventoryInputsPolicy, Resource: Resource);
    private static readonly IBasePolicy ReadManyInventoryInputs = new ReadManyPolicy(Name: ReadManyInventoryInputsPolicy, Resource: Resource);

    public static readonly Action<AuthorizationOptions> ConfigurePolicies = (AuthorizationOptions options) =>
    {
        options.AddPolicy(
            name: CreateOneInventoryInputs.Name,
            configurePolicy: builder => builder.RequireProtectedResource(resource: CreateOneInventoryInputs.Resource, scope: CreateOneInventoryInputs.Scope)
        );
        options.AddPolicy(
            name: UpdateOneInventoryInputs.Name,
            configurePolicy: builder => builder.RequireProtectedResource(resource: UpdateOneInventoryInputs.Resource, scope: UpdateOneInventoryInputs.Scope)
        );
        options.AddPolicy(
            name: DeleteOneInventoryInputs.Name,
            configurePolicy: builder => builder.RequireProtectedResource(resource: DeleteOneInventoryInputs.Resource, scope: DeleteOneInventoryInputs.Scope)
        );
        options.AddPolicy(
            name: ReadOneInventoryInputs.Name,
            configurePolicy: builder => builder.RequireProtectedResource(resource: ReadOneInventoryInputs.Resource, scope: ReadOneInventoryInputs.Scope)
        );
        options.AddPolicy(
            name: ReadManyInventoryInputs.Name,
            configurePolicy: builder => builder.RequireProtectedResource(resource: ReadManyInventoryInputs.Resource, scope: ReadManyInventoryInputs.Scope)
        );
    };
}
