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
    public static string InventoriesCreateOneName { get; } =
        $"{ClientAuthorizationResourcesData.InventoriesName}:{ClientAuthorizationScopesData.CreateOneName}";
    public static string InventoriesCreateManyName { get; } =
        $"{ClientAuthorizationResourcesData.InventoriesName}:{ClientAuthorizationScopesData.CreateManyName}";
    public static string InventoriesReadOneName { get; } =
        $"{ClientAuthorizationResourcesData.InventoriesName}:{ClientAuthorizationScopesData.ReadOneName}";
    public static string InventoriesReadManyName { get; } =
        $"{ClientAuthorizationResourcesData.InventoriesName}:{ClientAuthorizationScopesData.ReadManyName}";
    public static string InventoriesUpdateOneName { get; } =
        $"{ClientAuthorizationResourcesData.InventoriesName}:{ClientAuthorizationScopesData.UpdateOneName}";
    public static string InventoriesUpdateManyName { get; } =
        $"{ClientAuthorizationResourcesData.InventoriesName}:{ClientAuthorizationScopesData.UpdateManyName}";
    public static string InventoriesDeleteOneName { get; } =
        $"{ClientAuthorizationResourcesData.InventoriesName}:{ClientAuthorizationScopesData.DeleteOneName}";
    public static string InventoriesDeleteManyName { get; } =
        $"{ClientAuthorizationResourcesData.InventoriesName}:{ClientAuthorizationScopesData.DeleteManyName}";

    private static Dictionary<string, ClientRolePolicy> GetClientRolePoliciesForInventories(
        Dictionary<string, Pulumi.Keycloak.Realm> realms,
        Dictionary<string, Pulumi.Keycloak.OpenId.Client> clients,
        Dictionary<string, Pulumi.Keycloak.Role> roles
    )
    {
        var bigpodsRealm = realms[RealmsData.BigpodsName];
        var bigpodsMonolithAPIClient = clients[ClientsData.BigpodsMonolithAPIClientName];
        var bigpodsAdminRole = roles[RolesData.AdminName];
        var bigpodsProductTeamRole = roles[RolesData.ProductTeamName];

        string inventoriesCreateOneNameKebabCase = StringUtils.ToKebabCase(
            InventoriesCreateOneName
        );
        string inventoriesCreateManyNameKebabCase = StringUtils.ToKebabCase(
            InventoriesCreateManyName
        );
        string inventoriesReadOneNameKebabCase = StringUtils.ToKebabCase(InventoriesReadOneName);
        string inventoriesReadManyNameKebabCase = StringUtils.ToKebabCase(InventoriesReadManyName);
        string inventoriesUpdateOneNameKebabCase = StringUtils.ToKebabCase(
            InventoriesUpdateOneName
        );
        string inventoriesUpdateManyNameKebabCase = StringUtils.ToKebabCase(
            InventoriesUpdateManyName
        );
        string inventoriesDeleteOneNameKebabCase = StringUtils.ToKebabCase(
            InventoriesDeleteOneName
        );
        string inventoriesDeleteManyNameKebabCase = StringUtils.ToKebabCase(
            InventoriesDeleteManyName
        );

        return new Dictionary<string, ClientRolePolicy>
        {
            [InventoriesCreateOneName] = new ClientRolePolicy(
                Name: $"policies:{inventoriesCreateOneNameKebabCase}",
                Config: new()
                {
                    RealmId = bigpodsRealm.Id.Apply(id => id),
                    ResourceServerId = bigpodsMonolithAPIClient.Id.Apply(id => id),
                    Roles = new[]
                    {
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
                    Description = $"{InventoriesCreateOneName} Policy",
                    Logic = "POSITIVE",
                }
            ),
            [InventoriesCreateManyName] = new ClientRolePolicy(
                Name: $"policies:{inventoriesCreateManyNameKebabCase}",
                Config: new()
                {
                    RealmId = bigpodsRealm.Id.Apply(id => id),
                    ResourceServerId = bigpodsMonolithAPIClient.Id.Apply(id => id),
                    Roles = new[]
                    {
                        new Pulumi.Keycloak.OpenId.Inputs.ClientRolePolicyRoleArgs
                        {
                            Id = bigpodsAdminRole.Id.Apply(id => id),
                            Required = false,
                        },
                    },
                    Type = "role",
                    DecisionStrategy = "UNANIMOUS",
                    Description = $"{InventoriesCreateManyName} Policy",
                    Logic = "POSITIVE",
                }
            ),
            [InventoriesReadOneName] = new ClientRolePolicy(
                Name: $"policies:{inventoriesReadOneNameKebabCase}",
                Config: new()
                {
                    RealmId = bigpodsRealm.Id.Apply(id => id),
                    ResourceServerId = bigpodsMonolithAPIClient.Id.Apply(id => id),
                    Roles = new[]
                    {
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
                    Description = $"{InventoriesReadOneName} Policy",
                    Logic = "POSITIVE",
                }
            ),
            [InventoriesReadManyName] = new ClientRolePolicy(
                Name: $"policies:{inventoriesReadManyNameKebabCase}",
                Config: new()
                {
                    RealmId = bigpodsRealm.Id.Apply(id => id),
                    ResourceServerId = bigpodsMonolithAPIClient.Id.Apply(id => id),
                    Roles = new[]
                    {
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
                    Description = $"{InventoriesReadManyName} Policy",
                    Logic = "POSITIVE",
                }
            ),
            [InventoriesUpdateOneName] = new ClientRolePolicy(
                Name: $"policies:{inventoriesUpdateOneNameKebabCase}",
                Config: new()
                {
                    RealmId = bigpodsRealm.Id.Apply(id => id),
                    ResourceServerId = bigpodsMonolithAPIClient.Id.Apply(id => id),
                    Roles = new[]
                    {
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
                    Description = $"{InventoriesUpdateOneName} Policy",
                    Logic = "POSITIVE",
                }
            ),
            [InventoriesUpdateManyName] = new ClientRolePolicy(
                Name: $"policies:{inventoriesUpdateManyNameKebabCase}",
                Config: new()
                {
                    RealmId = bigpodsRealm.Id.Apply(id => id),
                    ResourceServerId = bigpodsMonolithAPIClient.Id.Apply(id => id),
                    Roles = new[]
                    {
                        new Pulumi.Keycloak.OpenId.Inputs.ClientRolePolicyRoleArgs
                        {
                            Id = bigpodsAdminRole.Id.Apply(id => id),
                            Required = false,
                        },
                    },
                    Type = "role",
                    DecisionStrategy = "UNANIMOUS",
                    Description = $"{InventoriesUpdateManyName} Policy",
                    Logic = "POSITIVE",
                }
            ),
            [InventoriesDeleteOneName] = new ClientRolePolicy(
                Name: $"policies:{inventoriesDeleteOneNameKebabCase}",
                Config: new()
                {
                    RealmId = bigpodsRealm.Id.Apply(id => id),
                    ResourceServerId = bigpodsMonolithAPIClient.Id.Apply(id => id),
                    Roles = new[]
                    {
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
                    Description = $"{InventoriesDeleteOneName} Policy",
                    Logic = "POSITIVE",
                }
            ),
            [InventoriesDeleteManyName] = new ClientRolePolicy(
                Name: $"policies:{inventoriesDeleteManyNameKebabCase}",
                Config: new()
                {
                    RealmId = bigpodsRealm.Id.Apply(id => id),
                    ResourceServerId = bigpodsMonolithAPIClient.Id.Apply(id => id),
                    Roles = new[]
                    {
                        new Pulumi.Keycloak.OpenId.Inputs.ClientRolePolicyRoleArgs
                        {
                            Id = bigpodsAdminRole.Id.Apply(id => id),
                            Required = false,
                        },
                    },
                    Type = "role",
                    DecisionStrategy = "UNANIMOUS",
                    Description = $"{InventoriesDeleteManyName} Policy",
                    Logic = "POSITIVE",
                }
            ),
        };
    }
}
