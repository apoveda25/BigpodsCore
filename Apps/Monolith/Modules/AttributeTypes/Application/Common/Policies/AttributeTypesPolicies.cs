using Bigpods.Monolith.Modules.Shared.Application.Policies;
using Bigpods.Monolith.Modules.Shared.Domain.Policies;
using Keycloak.AuthServices.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace Bigpods.Monolith.Modules.AttributeTypes.Application.Common.Policies;

public sealed class AttributeTypesPolicies
{
    private const string Resource = "resources:attribute-types";

    public const string CreateOneAttributeTypesPolicy = "CreateOneAttributeTypesPolicy";
    public const string UpdateOneAttributeTypesPolicy = "UpdateOneAttributeTypesPolicy";
    public const string DeleteOneAttributeTypesPolicy = "DeleteOneAttributeTypesPolicy";
    public const string ReadOneAttributeTypesPolicy = "ReadOneAttributeTypesPolicy";
    public const string ReadManyAttributeTypesPolicy = "ReadManyAttributeTypesPolicy";

    private static readonly IBasePolicy CreateOneAttributeTypes = new CreateOnePolicy(
        Name: CreateOneAttributeTypesPolicy,
        Resource: Resource
    );
    private static readonly IBasePolicy UpdateOneAttributeTypes = new UpdateOnePolicy(
        Name: UpdateOneAttributeTypesPolicy,
        Resource: Resource
    );
    private static readonly IBasePolicy DeleteOneAttributeTypes = new DeleteOnePolicy(
        Name: DeleteOneAttributeTypesPolicy,
        Resource: Resource
    );
    private static readonly IBasePolicy ReadOneAttributeTypes = new ReadOnePolicy(
        Name: ReadOneAttributeTypesPolicy,
        Resource: Resource
    );
    private static readonly IBasePolicy ReadManyAttributeTypes = new ReadManyPolicy(
        Name: ReadManyAttributeTypesPolicy,
        Resource: Resource
    );

    public static readonly Action<AuthorizationOptions> ConfigurePolicies = (
        AuthorizationOptions options
    ) =>
    {
        options.AddPolicy(
            name: CreateOneAttributeTypesPolicy,
            configurePolicy: builder =>
                builder.RequireProtectedResource(
                    resource: CreateOneAttributeTypes.Resource,
                    scope: CreateOneAttributeTypes.Scope
                )
        );
        options.AddPolicy(
            name: UpdateOneAttributeTypesPolicy,
            configurePolicy: builder =>
                builder.RequireProtectedResource(
                    resource: UpdateOneAttributeTypes.Resource,
                    scope: UpdateOneAttributeTypes.Scope
                )
        );
        options.AddPolicy(
            name: DeleteOneAttributeTypesPolicy,
            configurePolicy: builder =>
                builder.RequireProtectedResource(
                    resource: DeleteOneAttributeTypes.Resource,
                    scope: DeleteOneAttributeTypes.Scope
                )
        );
        options.AddPolicy(
            name: ReadOneAttributeTypesPolicy,
            configurePolicy: builder =>
                builder.RequireProtectedResource(
                    resource: ReadOneAttributeTypes.Resource,
                    scope: ReadOneAttributeTypes.Scope
                )
        );
        options.AddPolicy(
            name: ReadManyAttributeTypesPolicy,
            configurePolicy: builder =>
                builder.RequireProtectedResource(
                    resource: ReadManyAttributeTypes.Resource,
                    scope: ReadManyAttributeTypes.Scope
                )
        );
    };
}
