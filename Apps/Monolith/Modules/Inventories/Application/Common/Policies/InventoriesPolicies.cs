using Bigpods.Monolith.Modules.Shared.Application.Policies;
using Bigpods.Monolith.Modules.Shared.Domain.Policies;

using Keycloak.AuthServices.Authorization;

using Microsoft.AspNetCore.Authorization;

namespace Bigpods.Monolith.Modules.Inventories.Application.Common.Policies;

public sealed class InventoriesPolicies
{
    private const string Resource = "resources:inventories";

    public const string CreateOneInventoriesPolicy = "CreateOneInventoriesPolicy";
    public const string UpdateOneInventoriesPolicy = "UpdateOneInventoriesPolicy";
    public const string DeleteOneInventoriesPolicy = "DeleteOneInventoriesPolicy";
    public const string ReadOneInventoriesPolicy = "ReadOneInventoriesPolicy";
    public const string ReadManyInventoriesPolicy = "ReadManyInventoriesPolicy";

    private static readonly IBasePolicy CreateOneInventories = new CreateOnePolicy(Name: CreateOneInventoriesPolicy, Resource: Resource);
    private static readonly IBasePolicy UpdateOneInventories = new UpdateOnePolicy(Name: UpdateOneInventoriesPolicy, Resource: Resource);
    private static readonly IBasePolicy DeleteOneInventories = new DeleteOnePolicy(Name: DeleteOneInventoriesPolicy, Resource: Resource);
    private static readonly IBasePolicy ReadOneInventories = new ReadOnePolicy(Name: ReadOneInventoriesPolicy, Resource: Resource);
    private static readonly IBasePolicy ReadManyInventories = new ReadManyPolicy(Name: ReadManyInventoriesPolicy, Resource: Resource);

    public static readonly Action<AuthorizationOptions> ConfigurePolicies = (AuthorizationOptions options) =>
    {
        options.AddPolicy(
            name: CreateOneInventories.Name,
            configurePolicy: builder => builder.RequireProtectedResource(resource: CreateOneInventories.Resource, scope: CreateOneInventories.Scope)
        );
        options.AddPolicy(
            name: UpdateOneInventories.Name,
            configurePolicy: builder => builder.RequireProtectedResource(resource: UpdateOneInventories.Resource, scope: UpdateOneInventories.Scope)
        );
        options.AddPolicy(
            name: DeleteOneInventories.Name,
            configurePolicy: builder => builder.RequireProtectedResource(resource: DeleteOneInventories.Resource, scope: DeleteOneInventories.Scope)
        );
        options.AddPolicy(
            name: ReadOneInventories.Name,
            configurePolicy: builder => builder.RequireProtectedResource(resource: ReadOneInventories.Resource, scope: ReadOneInventories.Scope)
        );
        options.AddPolicy(
            name: ReadManyInventories.Name,
            configurePolicy: builder => builder.RequireProtectedResource(resource: ReadManyInventories.Resource, scope: ReadManyInventories.Scope)
        );
    };
}
