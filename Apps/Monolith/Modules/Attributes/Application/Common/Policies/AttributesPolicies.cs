using Bigpods.Monolith.Modules.Shared.Application.Policies;
using Bigpods.Monolith.Modules.Shared.Domain.Policies;
using Keycloak.AuthServices.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace Bigpods.Monolith.Modules.Attributes.Application.Common.Policies;

public static class AttributesPolicies
{
    private const string Resource = "resources:attributes";

    public const string CreateOneAttributesPolicy = "CreateOneAttributesPolicy";
    public const string DeleteOneAttributesPolicy = "DeleteOneAttributesPolicy";
    public const string ReadOneAttributesPolicy = "ReadOneAttributesPolicy";
    public const string ReadManyAttributesPolicy = "ReadManyAttributesPolicy";

    private static readonly IBasePolicy CreateOneAttributes = new CreateOnePolicy(
        Name: CreateOneAttributesPolicy,
        Resource: Resource
    );
    private static readonly IBasePolicy DeleteOneAttributes = new DeleteOnePolicy(
        Name: DeleteOneAttributesPolicy,
        Resource: Resource
    );
    private static readonly IBasePolicy ReadOneAttributes = new ReadOnePolicy(
        Name: ReadOneAttributesPolicy,
        Resource: Resource
    );
    private static readonly IBasePolicy ReadManyAttributes = new ReadManyPolicy(
        Name: ReadManyAttributesPolicy,
        Resource: Resource
    );

    public static readonly Action<AuthorizationOptions> ConfigurePolicies = (
        AuthorizationOptions options
    ) =>
    {
        options.AddPolicy(
            name: CreateOneAttributes.Name,
            configurePolicy: builder =>
                builder.RequireProtectedResource(
                    resource: CreateOneAttributes.Resource,
                    scope: CreateOneAttributes.Scope
                )
        );
        options.AddPolicy(
            name: DeleteOneAttributes.Name,
            configurePolicy: builder =>
                builder.RequireProtectedResource(
                    resource: DeleteOneAttributes.Resource,
                    scope: DeleteOneAttributes.Scope
                )
        );
        options.AddPolicy(
            name: ReadOneAttributes.Name,
            configurePolicy: builder =>
                builder.RequireProtectedResource(
                    resource: ReadOneAttributes.Resource,
                    scope: ReadOneAttributes.Scope
                )
        );
        options.AddPolicy(
            name: ReadManyAttributes.Name,
            configurePolicy: builder =>
                builder.RequireProtectedResource(
                    resource: ReadManyAttributes.Resource,
                    scope: ReadManyAttributes.Scope
                )
        );
    };
}
