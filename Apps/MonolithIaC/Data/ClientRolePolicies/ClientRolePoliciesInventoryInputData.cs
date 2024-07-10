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
    public static string InventoryInputsCreateOneName { get; } =
        $"{ClientAuthorizationResourcesData.InventoryInputsName}:{ClientAuthorizationScopesData.CreateOneName}";
    public static string InventoryInputsCreateManyName { get; } =
        $"{ClientAuthorizationResourcesData.InventoryInputsName}:{ClientAuthorizationScopesData.CreateManyName}";
    public static string InventoryInputsReadOneName { get; } =
        $"{ClientAuthorizationResourcesData.InventoryInputsName}:{ClientAuthorizationScopesData.ReadOneName}";
    public static string InventoryInputsReadManyName { get; } =
        $"{ClientAuthorizationResourcesData.InventoryInputsName}:{ClientAuthorizationScopesData.ReadManyName}";
    public static string InventoryInputsUpdateOneName { get; } =
        $"{ClientAuthorizationResourcesData.InventoryInputsName}:{ClientAuthorizationScopesData.UpdateOneName}";
    public static string InventoryInputsUpdateManyName { get; } =
        $"{ClientAuthorizationResourcesData.InventoryInputsName}:{ClientAuthorizationScopesData.UpdateManyName}";
    public static string InventoryInputsDeleteOneName { get; } =
        $"{ClientAuthorizationResourcesData.InventoryInputsName}:{ClientAuthorizationScopesData.DeleteOneName}";
    public static string InventoryInputsDeleteManyName { get; } =
        $"{ClientAuthorizationResourcesData.InventoryInputsName}:{ClientAuthorizationScopesData.DeleteManyName}";

    private static Dictionary<string, ClientRolePolicy> GetClientRolePoliciesForInventoryInputs(
        Dictionary<string, Pulumi.Keycloak.Realm> realms,
        Dictionary<string, Pulumi.Keycloak.OpenId.Client> clients,
        Dictionary<string, Pulumi.Keycloak.Role> roles
    )
    {
        var bigpodsRealm = realms[RealmsData.BigpodsName];
        var bigpodsMonolithAPIClient = clients[ClientsData.BigpodsMonolithAPIClientName];
        var bigpodsAdminRole = roles[RolesData.AdminName];
        var bigpodsProductTeamRole = roles[RolesData.ProductTeamName];

        string inventoryInputsCreateOneNameKebabCase = StringUtils.ToKebabCase(
            InventoryInputsCreateOneName
        );
        string inventoryInputsCreateManyNameKebabCase = StringUtils.ToKebabCase(
            InventoryInputsCreateManyName
        );
        string inventoryInputsReadOneNameKebabCase = StringUtils.ToKebabCase(
            InventoryInputsReadOneName
        );
        string inventoryInputsReadManyNameKebabCase = StringUtils.ToKebabCase(
            InventoryInputsReadManyName
        );
        string inventoryInputsUpdateOneNameKebabCase = StringUtils.ToKebabCase(
            InventoryInputsUpdateOneName
        );
        string inventoryInputsUpdateManyNameKebabCase = StringUtils.ToKebabCase(
            InventoryInputsUpdateManyName
        );
        string inventoryInputsDeleteOneNameKebabCase = StringUtils.ToKebabCase(
            InventoryInputsDeleteOneName
        );
        string inventoryInputsDeleteManyNameKebabCase = StringUtils.ToKebabCase(
            InventoryInputsDeleteManyName
        );

        return new Dictionary<string, ClientRolePolicy>
        {
            [InventoryInputsCreateOneName] = new ClientRolePolicy(
                Name: $"policies:{inventoryInputsCreateOneNameKebabCase}",
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
                    Description = $"{InventoryInputsCreateOneName} Policy",
                    Logic = "POSITIVE",
                }
            ),
            [InventoryInputsCreateManyName] = new ClientRolePolicy(
                Name: $"policies:{inventoryInputsCreateManyNameKebabCase}",
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
                    Description = $"{InventoryInputsCreateManyName} Policy",
                    Logic = "POSITIVE",
                }
            ),
            [InventoryInputsReadOneName] = new ClientRolePolicy(
                Name: $"policies:{inventoryInputsReadOneNameKebabCase}",
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
                    Description = $"{InventoryInputsReadOneName} Policy",
                    Logic = "POSITIVE",
                }
            ),
            [InventoryInputsReadManyName] = new ClientRolePolicy(
                Name: $"policies:{inventoryInputsReadManyNameKebabCase}",
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
                    Description = $"{InventoryInputsReadManyName} Policy",
                    Logic = "POSITIVE",
                }
            ),
            [InventoryInputsUpdateOneName] = new ClientRolePolicy(
                Name: $"policies:{inventoryInputsUpdateOneNameKebabCase}",
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
                    Description = $"{InventoryInputsUpdateOneName} Policy",
                    Logic = "POSITIVE",
                }
            ),
            [InventoryInputsUpdateManyName] = new ClientRolePolicy(
                Name: $"policies:{inventoryInputsUpdateManyNameKebabCase}",
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
                    Description = $"{InventoryInputsUpdateManyName} Policy",
                    Logic = "POSITIVE",
                }
            ),
            [InventoryInputsDeleteOneName] = new ClientRolePolicy(
                Name: $"policies:{inventoryInputsDeleteOneNameKebabCase}",
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
                    Description = $"{InventoryInputsDeleteOneName} Policy",
                    Logic = "POSITIVE",
                }
            ),
            [InventoryInputsDeleteManyName] = new ClientRolePolicy(
                Name: $"policies:{inventoryInputsDeleteManyNameKebabCase}",
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
                    Description = $"{InventoryInputsDeleteManyName} Policy",
                    Logic = "POSITIVE",
                }
            ),
        };
    }
}
