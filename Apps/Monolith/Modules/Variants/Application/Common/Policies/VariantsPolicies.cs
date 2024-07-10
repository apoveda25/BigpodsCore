using Bigpods.Monolith.Modules.Shared.Application.Policies;
using Bigpods.Monolith.Modules.Shared.Domain.Policies;
using Keycloak.AuthServices.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace Bigpods.Monolith.Modules.Variants.Application.Common.Policies;

public sealed class VariantsPolicies
{
    private const string Resource = "resources:variants";

    public const string CreateOneVariantsPolicy = "CreateOneVariantsPolicy";
    public const string UpdateOneVariantsPolicy = "UpdateOneVariantsPolicy";
    public const string DeleteOneVariantsPolicy = "DeleteOneVariantsPolicy";
    public const string ReadOneVariantsPolicy = "ReadOneVariantsPolicy";
    public const string ReadManyVariantsPolicy = "ReadManyVariantsPolicy";

    private static readonly IBasePolicy CreateOneVariants = new CreateOnePolicy(
        Name: CreateOneVariantsPolicy,
        Resource: Resource
    );
    private static readonly IBasePolicy UpdateOneVariants = new UpdateOnePolicy(
        Name: UpdateOneVariantsPolicy,
        Resource: Resource
    );
    private static readonly IBasePolicy DeleteOneVariants = new DeleteOnePolicy(
        Name: DeleteOneVariantsPolicy,
        Resource: Resource
    );
    private static readonly IBasePolicy ReadOneVariants = new ReadOnePolicy(
        Name: ReadOneVariantsPolicy,
        Resource: Resource
    );
    private static readonly IBasePolicy ReadManyVariants = new ReadManyPolicy(
        Name: ReadManyVariantsPolicy,
        Resource: Resource
    );

    public static readonly Action<AuthorizationOptions> ConfigurePolicies = (
        AuthorizationOptions options
    ) =>
    {
        options.AddPolicy(
            name: CreateOneVariants.Name,
            configurePolicy: builder =>
                builder.RequireProtectedResource(
                    resource: CreateOneVariants.Resource,
                    scope: CreateOneVariants.Scope
                )
        );
        options.AddPolicy(
            name: UpdateOneVariants.Name,
            configurePolicy: builder =>
                builder.RequireProtectedResource(
                    resource: UpdateOneVariants.Resource,
                    scope: UpdateOneVariants.Scope
                )
        );
        options.AddPolicy(
            name: DeleteOneVariants.Name,
            configurePolicy: builder =>
                builder.RequireProtectedResource(
                    resource: DeleteOneVariants.Resource,
                    scope: DeleteOneVariants.Scope
                )
        );
        options.AddPolicy(
            name: ReadOneVariants.Name,
            configurePolicy: builder =>
                builder.RequireProtectedResource(
                    resource: ReadOneVariants.Resource,
                    scope: ReadOneVariants.Scope
                )
        );
        options.AddPolicy(
            name: ReadManyVariants.Name,
            configurePolicy: builder =>
                builder.RequireProtectedResource(
                    resource: ReadManyVariants.Resource,
                    scope: ReadManyVariants.Scope
                )
        );
    };
}
