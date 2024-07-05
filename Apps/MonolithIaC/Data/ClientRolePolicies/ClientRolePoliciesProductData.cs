using System.Collections.Generic;

using Bigpods.MonolithIaC.Data.ClientAuthorizationResources;
using Bigpods.MonolithIaC.Data.ClientAuthorizationScopes;
using Bigpods.MonolithIaC.Data.Clients;
using Bigpods.MonolithIaC.Data.Realms;
using Bigpods.MonolithIaC.Data.Roles;

using Bigpods.MonolithIaC.Utils;

namespace Bigpods.MonolithIaC.Data.ClientRolePolicies;

public partial class ClientRolePoliciesData
{
    public static string ProductsCreateOneName { get; } = $"{ClientAuthorizationResourcesData.ProductsName}:{ClientAuthorizationScopesData.CreateOneName}";
    public static string ProductsCreateManyName { get; } = $"{ClientAuthorizationResourcesData.ProductsName}:{ClientAuthorizationScopesData.CreateManyName}";
    public static string ProductsReadOneName { get; } = $"{ClientAuthorizationResourcesData.ProductsName}:{ClientAuthorizationScopesData.ReadOneName}";
    public static string ProductsReadManyName { get; } = $"{ClientAuthorizationResourcesData.ProductsName}:{ClientAuthorizationScopesData.ReadManyName}";
    public static string ProductsUpdateOneName { get; } = $"{ClientAuthorizationResourcesData.ProductsName}:{ClientAuthorizationScopesData.UpdateOneName}";
    public static string ProductsUpdateManyName { get; } = $"{ClientAuthorizationResourcesData.ProductsName}:{ClientAuthorizationScopesData.UpdateManyName}";
    public static string ProductsDeleteOneName { get; } = $"{ClientAuthorizationResourcesData.ProductsName}:{ClientAuthorizationScopesData.DeleteOneName}";
    public static string ProductsDeleteManyName { get; } = $"{ClientAuthorizationResourcesData.ProductsName}:{ClientAuthorizationScopesData.DeleteManyName}";

    private static Dictionary<string, ClientRolePolicy> GetClientRolePoliciesForProducts(
        Dictionary<string, Pulumi.Keycloak.Realm> realms,
        Dictionary<string, Pulumi.Keycloak.OpenId.Client> clients,
        Dictionary<string, Pulumi.Keycloak.Role> roles
    )
    {
        var bigpodsRealm = realms[RealmsData.BigpodsName];
        var bigpodsMonolithAPIClient = clients[ClientsData.BigpodsMonolithAPIClientName];
        var bigpodsAdminRole = roles[RolesData.AdminName];
        var bigpodsProductTeamRole = roles[RolesData.ProductTeamName];

        string productsCreateOneNameKebabCase = StringUtils.ToKebabCase(ProductsCreateOneName);
        string productsCreateManyNameKebabCase = StringUtils.ToKebabCase(ProductsCreateManyName);
        string productsReadOneNameKebabCase = StringUtils.ToKebabCase(ProductsReadOneName);
        string productsReadManyNameKebabCase = StringUtils.ToKebabCase(ProductsReadManyName);
        string productsUpdateOneNameKebabCase = StringUtils.ToKebabCase(ProductsUpdateOneName);
        string productsUpdateManyNameKebabCase = StringUtils.ToKebabCase(ProductsUpdateManyName);
        string productsDeleteOneNameKebabCase = StringUtils.ToKebabCase(ProductsDeleteOneName);
        string productsDeleteManyNameKebabCase = StringUtils.ToKebabCase(ProductsDeleteManyName);

        return new Dictionary<string, ClientRolePolicy>
        {
            [ProductsCreateOneName] = new ClientRolePolicy(Name: $"policies:{productsCreateOneNameKebabCase}", Config: new()
            {
                RealmId = bigpodsRealm.Id.Apply(id => id),
                ResourceServerId = bigpodsMonolithAPIClient.Id.Apply(id => id),
                Roles = new[] {
                    new Pulumi.Keycloak.OpenId.Inputs.ClientRolePolicyRoleArgs
                    {
                        Id = bigpodsAdminRole.Id.Apply(id => id),
                        Required = false,
                    },
                    new Pulumi.Keycloak.OpenId.Inputs.ClientRolePolicyRoleArgs
                    {
                        Id = bigpodsProductTeamRole.Id.Apply(id => id),
                        Required = false,
                    }
                },
                Type = "role",
                DecisionStrategy = "UNANIMOUS",
                Description = $"{ProductsCreateOneName} Policy",
                Logic = "POSITIVE",
            }),
            [ProductsCreateManyName] = new ClientRolePolicy(Name: $"policies:{productsCreateManyNameKebabCase}", Config: new()
            {
                RealmId = bigpodsRealm.Id.Apply(id => id),
                ResourceServerId = bigpodsMonolithAPIClient.Id.Apply(id => id),
                Roles = new[] {
                    new Pulumi.Keycloak.OpenId.Inputs.ClientRolePolicyRoleArgs
                    {
                        Id = bigpodsAdminRole.Id.Apply(id => id),
                        Required = false,
                    },
                },
                Type = "role",
                DecisionStrategy = "UNANIMOUS",
                Description = $"{ProductsCreateManyName} Policy",
                Logic = "POSITIVE",
            }),
            [ProductsReadOneName] = new ClientRolePolicy(Name: $"policies:{productsReadOneNameKebabCase}", Config: new()
            {
                RealmId = bigpodsRealm.Id.Apply(id => id),
                ResourceServerId = bigpodsMonolithAPIClient.Id.Apply(id => id),
                Roles = new[] {
                    new Pulumi.Keycloak.OpenId.Inputs.ClientRolePolicyRoleArgs
                    {
                        Id = bigpodsAdminRole.Id.Apply(id => id),
                        Required = false,
                    },
                    new Pulumi.Keycloak.OpenId.Inputs.ClientRolePolicyRoleArgs
                    {
                        Id = bigpodsProductTeamRole.Id.Apply(id => id),
                        Required = false,
                    }
                }
            }),
            [ProductsReadManyName] = new ClientRolePolicy(Name: $"policies:{productsReadManyNameKebabCase}", Config: new()
            {
                RealmId = bigpodsRealm.Id.Apply(id => id),
                ResourceServerId = bigpodsMonolithAPIClient.Id.Apply(id => id),
                Roles = new[] {
                    new Pulumi.Keycloak.OpenId.Inputs.ClientRolePolicyRoleArgs
                    {
                        Id = bigpodsAdminRole.Id.Apply(id => id),
                        Required = false,
                    },
                    new Pulumi.Keycloak.OpenId.Inputs.ClientRolePolicyRoleArgs
                    {
                        Id = bigpodsProductTeamRole.Id.Apply(id => id),
                        Required = false,
                    }
                },
                Type = "role",
                DecisionStrategy = "UNANIMOUS",
                Description = $"{ProductsReadManyName} Policy",
                Logic = "POSITIVE",
            }),
            [ProductsUpdateOneName] = new ClientRolePolicy(Name: $"policies:{productsUpdateOneNameKebabCase}", Config: new()
            {
                RealmId = bigpodsRealm.Id.Apply(id => id),
                ResourceServerId = bigpodsMonolithAPIClient.Id.Apply(id => id),
                Roles = new[] {
                    new Pulumi.Keycloak.OpenId.Inputs.ClientRolePolicyRoleArgs
                    {
                        Id = bigpodsAdminRole.Id.Apply(id => id),
                        Required = false,
                    },
                    new Pulumi.Keycloak.OpenId.Inputs.ClientRolePolicyRoleArgs
                    {
                        Id = bigpodsProductTeamRole.Id.Apply(id => id),
                        Required = false,
                    }
                },
                Type = "role",
                DecisionStrategy = "UNANIMOUS",
                Description = $"{ProductsUpdateOneName} Policy",
                Logic = "POSITIVE",
            }),
            [ProductsUpdateManyName] = new ClientRolePolicy(Name: $"policies:{productsUpdateManyNameKebabCase}", Config: new()
            {
                RealmId = bigpodsRealm.Id.Apply(id => id),
                ResourceServerId = bigpodsMonolithAPIClient.Id.Apply(id => id),
                Roles = new[] {
                    new Pulumi.Keycloak.OpenId.Inputs.ClientRolePolicyRoleArgs
                    {
                        Id = bigpodsAdminRole.Id.Apply(id => id),
                        Required = false,
                    },
                },
                Type = "role",
                DecisionStrategy = "UNANIMOUS",
                Description = $"{ProductsUpdateManyName} Policy",
                Logic = "POSITIVE",
            }),
            [ProductsDeleteOneName] = new ClientRolePolicy(Name: $"policies:{productsDeleteOneNameKebabCase}", Config: new()
            {
                RealmId = bigpodsRealm.Id.Apply(id => id),
                ResourceServerId = bigpodsMonolithAPIClient.Id.Apply(id => id),
                Roles = new[] {
                    new Pulumi.Keycloak.OpenId.Inputs.ClientRolePolicyRoleArgs
                    {
                        Id = bigpodsAdminRole.Id.Apply(id => id),
                        Required = false,
                    },
                    new Pulumi.Keycloak.OpenId.Inputs.ClientRolePolicyRoleArgs
                    {
                        Id = bigpodsProductTeamRole.Id.Apply(id => id),
                        Required = false,
                    }
                },
                Type = "role",
                DecisionStrategy = "UNANIMOUS",
                Description = $"{ProductsDeleteOneName} Policy",
                Logic = "POSITIVE",
            }),
            [ProductsDeleteManyName] = new ClientRolePolicy(Name: $"policies:{productsDeleteManyNameKebabCase}", Config: new()
            {
                RealmId = bigpodsRealm.Id.Apply(id => id),
                ResourceServerId = bigpodsMonolithAPIClient.Id.Apply(id => id),
                Roles = new[] {
                    new Pulumi.Keycloak.OpenId.Inputs.ClientRolePolicyRoleArgs
                    {
                        Id = bigpodsAdminRole.Id.Apply(id => id),
                        Required = false,
                    }
                }
            }),
        };
    }
}
