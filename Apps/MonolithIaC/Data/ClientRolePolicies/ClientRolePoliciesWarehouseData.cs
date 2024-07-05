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
    public static string WarehousesCreateOneName { get; } = $"{ClientAuthorizationResourcesData.WarehousesName}:{ClientAuthorizationScopesData.CreateOneName}";
    public static string WarehousesCreateManyName { get; } = $"{ClientAuthorizationResourcesData.WarehousesName}:{ClientAuthorizationScopesData.CreateManyName}";
    public static string WarehousesReadOneName { get; } = $"{ClientAuthorizationResourcesData.WarehousesName}:{ClientAuthorizationScopesData.ReadOneName}";
    public static string WarehousesReadManyName { get; } = $"{ClientAuthorizationResourcesData.WarehousesName}:{ClientAuthorizationScopesData.ReadManyName}";
    public static string WarehousesUpdateOneName { get; } = $"{ClientAuthorizationResourcesData.WarehousesName}:{ClientAuthorizationScopesData.UpdateOneName}";
    public static string WarehousesUpdateManyName { get; } = $"{ClientAuthorizationResourcesData.WarehousesName}:{ClientAuthorizationScopesData.UpdateManyName}";
    public static string WarehousesDeleteOneName { get; } = $"{ClientAuthorizationResourcesData.WarehousesName}:{ClientAuthorizationScopesData.DeleteOneName}";
    public static string WarehousesDeleteManyName { get; } = $"{ClientAuthorizationResourcesData.WarehousesName}:{ClientAuthorizationScopesData.DeleteManyName}";

    private static Dictionary<string, ClientRolePolicy> GetClientRolePoliciesForWarehouses(
        Dictionary<string, Pulumi.Keycloak.Realm> realms,
        Dictionary<string, Pulumi.Keycloak.OpenId.Client> clients,
        Dictionary<string, Pulumi.Keycloak.Role> roles
    )
    {
        var bigpodsRealm = realms[RealmsData.BigpodsName];
        var bigpodsMonolithAPIClient = clients[ClientsData.BigpodsMonolithAPIClientName];
        var bigpodsAdminRole = roles[RolesData.AdminName];
        var bigpodsProductTeamRole = roles[RolesData.ProductTeamName];

        string warehousesCreateOneNameKebabCase = StringUtils.ToKebabCase(WarehousesCreateOneName);
        string warehousesCreateManyNameKebabCase = StringUtils.ToKebabCase(WarehousesCreateManyName);
        string warehousesReadOneNameKebabCase = StringUtils.ToKebabCase(WarehousesReadOneName);
        string warehousesReadManyNameKebabCase = StringUtils.ToKebabCase(WarehousesReadManyName);
        string warehousesUpdateOneNameKebabCase = StringUtils.ToKebabCase(WarehousesUpdateOneName);
        string warehousesUpdateManyNameKebabCase = StringUtils.ToKebabCase(WarehousesUpdateManyName);
        string warehousesDeleteOneNameKebabCase = StringUtils.ToKebabCase(WarehousesDeleteOneName);
        string warehousesDeleteManyNameKebabCase = StringUtils.ToKebabCase(WarehousesDeleteManyName);

        return new Dictionary<string, ClientRolePolicy>
        {
            [WarehousesCreateOneName] = new ClientRolePolicy(Name: $"policies:{warehousesCreateOneNameKebabCase}", Config: new()
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
                Description = $"{WarehousesCreateOneName} Policy",
                Logic = "POSITIVE",
            }),
            [WarehousesCreateManyName] = new ClientRolePolicy(Name: $"policies:{warehousesCreateManyNameKebabCase}", Config: new()
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
                Description = $"{WarehousesCreateManyName} Policy",
                Logic = "POSITIVE",
            }),
            [WarehousesReadOneName] = new ClientRolePolicy(Name: $"policies:{warehousesReadOneNameKebabCase}", Config: new()
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
                Description = $"{WarehousesReadOneName} Policy",
                Logic = "POSITIVE",
            }),
            [WarehousesReadManyName] = new ClientRolePolicy(Name: $"policies:{warehousesReadManyNameKebabCase}", Config: new()
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
                Description = $"{WarehousesReadManyName} Policy",
                Logic = "POSITIVE",
            }),
            [WarehousesUpdateOneName] = new ClientRolePolicy(Name: $"policies:{warehousesUpdateOneNameKebabCase}", Config: new()
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
                Description = $"{WarehousesUpdateOneName} Policy",
                Logic = "POSITIVE",
            }),
            [WarehousesUpdateManyName] = new ClientRolePolicy(Name: $"policies:{warehousesUpdateManyNameKebabCase}", Config: new()
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
                Description = $"{WarehousesUpdateManyName} Policy",
                Logic = "POSITIVE",
            }),
            [WarehousesDeleteOneName] = new ClientRolePolicy(Name: $"policies:{warehousesDeleteOneNameKebabCase}", Config: new()
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
                Description = $"{WarehousesDeleteOneName} Policy",
                Logic = "POSITIVE",
            }),
            [WarehousesDeleteManyName] = new ClientRolePolicy(Name: $"policies:{warehousesDeleteManyNameKebabCase}", Config: new()
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
                Description = $"{WarehousesDeleteManyName} Policy",
                Logic = "POSITIVE",
            }),
        };
    }
}
