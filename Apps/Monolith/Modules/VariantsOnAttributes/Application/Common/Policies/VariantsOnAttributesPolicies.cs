using Bigpods.Monolith.Modules.Shared.Application.Policies;
using Bigpods.Monolith.Modules.Shared.Domain.Policies;
using Keycloak.AuthServices.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace Bigpods.Monolith.Modules.VariantsOnAttributes.Application.Common.Policies;

public sealed class VariantsOnAttributesPolicies
{
    private const string Resource = "resources:variants-on-attributes";

    public const string AttachManyVariantsOnAttributesPolicy =
        "AttachManyVariantsOnAttributesPolicy";
    public const string DettachManyVariantsOnAttributesPolicy =
        "DettachManyVariantsOnAttributesPolicy";
    public const string ReadOneVariantsOnAttributesPolicy = "ReadOneVariantsOnAttributesPolicy";
    public const string ReadManyVariantsOnAttributesPolicy = "ReadManyVariantsOnAttributesPolicy";

    private static readonly IBasePolicy AttachManyVariantsOnAttributes = new AttachManyPolicy(
        Name: AttachManyVariantsOnAttributesPolicy,
        Resource: Resource
    );
    private static readonly IBasePolicy DettachManyVariantsOnAttributes = new DettachManyPolicy(
        Name: DettachManyVariantsOnAttributesPolicy,
        Resource: Resource
    );
    private static readonly IBasePolicy ReadOneVariantsOnAttributes = new ReadOnePolicy(
        Name: ReadOneVariantsOnAttributesPolicy,
        Resource: Resource
    );
    private static readonly IBasePolicy ReadManyVariantsOnAttributes = new ReadManyPolicy(
        Name: ReadManyVariantsOnAttributesPolicy,
        Resource: Resource
    );

    public static readonly Action<AuthorizationOptions> ConfigurePolicies = (
        AuthorizationOptions options
    ) =>
    {
        options.AddPolicy(
            name: AttachManyVariantsOnAttributes.Name,
            configurePolicy: builder =>
                builder.RequireProtectedResource(
                    resource: AttachManyVariantsOnAttributes.Resource,
                    scope: AttachManyVariantsOnAttributes.Scope
                )
        );
        options.AddPolicy(
            name: DettachManyVariantsOnAttributes.Name,
            configurePolicy: builder =>
                builder.RequireProtectedResource(
                    resource: DettachManyVariantsOnAttributes.Resource,
                    scope: DettachManyVariantsOnAttributes.Scope
                )
        );
        options.AddPolicy(
            name: ReadOneVariantsOnAttributes.Name,
            configurePolicy: builder =>
                builder.RequireProtectedResource(
                    resource: ReadOneVariantsOnAttributes.Resource,
                    scope: ReadOneVariantsOnAttributes.Scope
                )
        );
        options.AddPolicy(
            name: ReadManyVariantsOnAttributes.Name,
            configurePolicy: builder =>
                builder.RequireProtectedResource(
                    resource: ReadManyVariantsOnAttributes.Resource,
                    scope: ReadManyVariantsOnAttributes.Scope
                )
        );
    };
}
