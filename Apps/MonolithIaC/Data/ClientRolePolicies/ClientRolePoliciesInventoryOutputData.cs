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
    public static string InventoryOutputsCreateOneName { get; } =
        $"{ClientAuthorizationResourcesData.InventoryOutputsName}:{ClientAuthorizationScopesData.CreateOneName}";
    public static string InventoryOutputsCreateManyName { get; } =
        $"{ClientAuthorizationResourcesData.InventoryOutputsName}:{ClientAuthorizationScopesData.CreateManyName}";
    public static string InventoryOutputsReadOneName { get; } =
        $"{ClientAuthorizationResourcesData.InventoryOutputsName}:{ClientAuthorizationScopesData.ReadOneName}";
    public static string InventoryOutputsReadManyName { get; } =
        $"{ClientAuthorizationResourcesData.InventoryOutputsName}:{ClientAuthorizationScopesData.ReadManyName}";
    public static string InventoryOutputsUpdateOneName { get; } =
        $"{ClientAuthorizationResourcesData.InventoryOutputsName}:{ClientAuthorizationScopesData.UpdateOneName}";
    public static string InventoryOutputsUpdateManyName { get; } =
        $"{ClientAuthorizationResourcesData.InventoryOutputsName}:{ClientAuthorizationScopesData.UpdateManyName}";
    public static string InventoryOutputsDeleteOneName { get; } =
        $"{ClientAuthorizationResourcesData.InventoryOutputsName}:{ClientAuthorizationScopesData.DeleteOneName}";
    public static string InventoryOutputsDeleteManyName { get; } =
        $"{ClientAuthorizationResourcesData.InventoryOutputsName}:{ClientAuthorizationScopesData.DeleteManyName}";

    private static Dictionary<string, ClientRolePolicy> GetClientRolePoliciesForInventoryOutputs(
        Dictionary<string, Pulumi.Keycloak.Realm> realms,
        Dictionary<string, Pulumi.Keycloak.OpenId.Client> clients,
        Dictionary<string, Pulumi.Keycloak.Role> roles
    )
    {
        var bigpodsRealm = realms[RealmsData.BigpodsName];
        var bigpodsMonolithAPIClient = clients[ClientsData.BigpodsMonolithAPIClientName];
        var bigpodsAdminRole = roles[RolesData.AdminName];
        var bigpodsProductTeamRole = roles[RolesData.ProductTeamName];

        string inventoryOutputsCreateOneNameKebabCase = StringUtils.ToKebabCase(
            InventoryOutputsCreateOneName
        );
        string inventoryOutputsCreateManyNameKebabCase = StringUtils.ToKebabCase(
            InventoryOutputsCreateManyName
        );
        string inventoryOutputsReadOneNameKebabCase = StringUtils.ToKebabCase(
            InventoryOutputsReadOneName
        );
        string inventoryOutputsReadManyNameKebabCase = StringUtils.ToKebabCase(
            InventoryOutputsReadManyName
        );
        string inventoryOutputsUpdateOneNameKebabCase = StringUtils.ToKebabCase(
            InventoryOutputsUpdateOneName
        );
        string inventoryOutputsUpdateManyNameKebabCase = StringUtils.ToKebabCase(
            InventoryOutputsUpdateManyName
        );
        string inventoryOutputsDeleteOneNameKebabCase = StringUtils.ToKebabCase(
            InventoryOutputsDeleteOneName
        );
        string inventoryOutputsDeleteManyNameKebabCase = StringUtils.ToKebabCase(
            InventoryOutputsDeleteManyName
        );

        return new Dictionary<string, ClientRolePolicy>
        {
            [InventoryOutputsCreateOneName] = new ClientRolePolicy(
                Name: $"policies:{inventoryOutputsCreateOneNameKebabCase}",
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
                    Description = $"{InventoryOutputsCreateOneName} Policy",
                    Logic = "POSITIVE",
                }
            ),
            [InventoryOutputsCreateManyName] = new ClientRolePolicy(
                Name: $"policies:{inventoryOutputsCreateManyNameKebabCase}",
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
                    Description = $"{InventoryOutputsCreateManyName} Policy",
                    Logic = "POSITIVE",
                }
            ),
            [InventoryOutputsReadOneName] = new ClientRolePolicy(
                Name: $"policies:{inventoryOutputsReadOneNameKebabCase}",
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
                    Description = $"{InventoryOutputsReadOneName} Policy",
                    Logic = "POSITIVE",
                }
            ),
            [InventoryOutputsReadManyName] = new ClientRolePolicy(
                Name: $"policies:{inventoryOutputsReadManyNameKebabCase}",
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
                    Description = $"{InventoryOutputsReadManyName} Policy",
                    Logic = "POSITIVE",
                }
            ),
            [InventoryOutputsUpdateOneName] = new ClientRolePolicy(
                Name: $"policies:{inventoryOutputsUpdateOneNameKebabCase}",
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
                    Description = $"{InventoryOutputsUpdateOneName} Policy",
                    Logic = "POSITIVE",
                }
            ),
            [InventoryOutputsUpdateManyName] = new ClientRolePolicy(
                Name: $"policies:{inventoryOutputsUpdateManyNameKebabCase}",
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
                    Description = $"{InventoryOutputsUpdateManyName} Policy",
                    Logic = "POSITIVE",
                }
            ),
            [InventoryOutputsDeleteOneName] = new ClientRolePolicy(
                Name: $"policies:{inventoryOutputsDeleteOneNameKebabCase}",
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
                    Description = $"{InventoryOutputsDeleteOneName} Policy",
                    Logic = "POSITIVE",
                }
            ),
            [InventoryOutputsDeleteManyName] = new ClientRolePolicy(
                Name: $"policies:{inventoryOutputsDeleteManyNameKebabCase}",
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
                    Description = $"{InventoryOutputsDeleteManyName} Policy",
                    Logic = "POSITIVE",
                }
            ),
        };
    }
}
