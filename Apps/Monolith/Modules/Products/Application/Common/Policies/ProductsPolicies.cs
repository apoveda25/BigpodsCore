using Bigpods.Monolith.Modules.Shared.Application.Policies;
using Bigpods.Monolith.Modules.Shared.Domain.Policies;
using Keycloak.AuthServices.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace Bigpods.Monolith.Modules.Products.Application.Common.Policies;

public sealed class ProductsPolicies
{
    private const string Resource = "resources:products";

    public const string CreateOneProductsPolicy = "CreateOneProductsPolicy";
    public const string UpdateOneProductsPolicy = "UpdateOneProductsPolicy";
    public const string DeleteOneProductsPolicy = "DeleteOneProductsPolicy";
    public const string ReadOneProductsPolicy = "ReadOneProductsPolicy";
    public const string ReadManyProductsPolicy = "ReadManyProductsPolicy";

    private static readonly IBasePolicy CreateOneProducts = new CreateOnePolicy(
        Name: CreateOneProductsPolicy,
        Resource: Resource
    );
    private static readonly IBasePolicy UpdateOneProducts = new UpdateOnePolicy(
        Name: UpdateOneProductsPolicy,
        Resource: Resource
    );
    private static readonly IBasePolicy DeleteOneProducts = new DeleteOnePolicy(
        Name: DeleteOneProductsPolicy,
        Resource: Resource
    );
    private static readonly IBasePolicy ReadOneProducts = new ReadOnePolicy(
        Name: ReadOneProductsPolicy,
        Resource: Resource
    );
    private static readonly IBasePolicy ReadManyProducts = new ReadManyPolicy(
        Name: ReadManyProductsPolicy,
        Resource: Resource
    );

    public static readonly Action<AuthorizationOptions> ConfigurePolicies = (
        AuthorizationOptions options
    ) =>
    {
        options.AddPolicy(
            name: CreateOneProducts.Name,
            configurePolicy: builder =>
                builder.RequireProtectedResource(
                    resource: CreateOneProducts.Resource,
                    scope: CreateOneProducts.Scope
                )
        );
        options.AddPolicy(
            name: UpdateOneProducts.Name,
            configurePolicy: builder =>
                builder.RequireProtectedResource(
                    resource: UpdateOneProducts.Resource,
                    scope: UpdateOneProducts.Scope
                )
        );
        options.AddPolicy(
            name: DeleteOneProducts.Name,
            configurePolicy: builder =>
                builder.RequireProtectedResource(
                    resource: DeleteOneProducts.Resource,
                    scope: DeleteOneProducts.Scope
                )
        );
        options.AddPolicy(
            name: ReadOneProducts.Name,
            configurePolicy: builder =>
                builder.RequireProtectedResource(
                    resource: ReadOneProducts.Resource,
                    scope: ReadOneProducts.Scope
                )
        );
        options.AddPolicy(
            name: ReadManyProducts.Name,
            configurePolicy: builder =>
                builder.RequireProtectedResource(
                    resource: ReadManyProducts.Resource,
                    scope: ReadManyProducts.Scope
                )
        );
    };
}
