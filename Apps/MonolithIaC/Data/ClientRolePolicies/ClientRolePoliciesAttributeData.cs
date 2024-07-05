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
    public static string AttributesCreateOneName { get; } = $"{ClientAuthorizationResourcesData.AttributesName}:{ClientAuthorizationScopesData.CreateOneName}";
    public static string AttributesCreateManyName { get; } = $"{ClientAuthorizationResourcesData.AttributesName}:{ClientAuthorizationScopesData.CreateManyName}";
    public static string AttributesReadOneName { get; } = $"{ClientAuthorizationResourcesData.AttributesName}:{ClientAuthorizationScopesData.ReadOneName}";
    public static string AttributesReadManyName { get; } = $"{ClientAuthorizationResourcesData.AttributesName}:{ClientAuthorizationScopesData.ReadManyName}";
    public static string AttributesUpdateOneName { get; } = $"{ClientAuthorizationResourcesData.AttributesName}:{ClientAuthorizationScopesData.UpdateOneName}";
    public static string AttributesUpdateManyName { get; } = $"{ClientAuthorizationResourcesData.AttributesName}:{ClientAuthorizationScopesData.UpdateManyName}";
    public static string AttributesDeleteOneName { get; } = $"{ClientAuthorizationResourcesData.AttributesName}:{ClientAuthorizationScopesData.DeleteOneName}";
    public static string AttributesDeleteManyName { get; } = $"{ClientAuthorizationResourcesData.AttributesName}:{ClientAuthorizationScopesData.DeleteManyName}";

    private static Dictionary<string, ClientRolePolicy> GetClientRolePoliciesForAttributes(
        Dictionary<string, Pulumi.Keycloak.Realm> realms,
        Dictionary<string, Pulumi.Keycloak.OpenId.Client> clients,
        Dictionary<string, Pulumi.Keycloak.Role> roles
    )
    {
        var bigpodsRealm = realms[RealmsData.BigpodsName];
        var bigpodsMonolithAPIClient = clients[ClientsData.BigpodsMonolithAPIClientName];
        var bigpodsAdminRole = roles[RolesData.AdminName];
        var bigpodsProductTeamRole = roles[RolesData.ProductTeamName];

        string attributesCreateOneNameKebabCase = StringUtils.ToKebabCase(AttributesCreateOneName);
        string attributesCreateManyNameKebabCase = StringUtils.ToKebabCase(AttributesCreateManyName);
        string attributesReadOneNameKebabCase = StringUtils.ToKebabCase(AttributesReadOneName);
        string attributesReadManyNameKebabCase = StringUtils.ToKebabCase(AttributesReadManyName);
        string attributesUpdateOneNameKebabCase = StringUtils.ToKebabCase(AttributesUpdateOneName);
        string attributesUpdateManyNameKebabCase = StringUtils.ToKebabCase(AttributesUpdateManyName);
        string attributesDeleteOneNameKebabCase = StringUtils.ToKebabCase(AttributesDeleteOneName);
        string attributesDeleteManyNameKebabCase = StringUtils.ToKebabCase(AttributesDeleteManyName);

        return new Dictionary<string, ClientRolePolicy>
        {
            [AttributesCreateOneName] = new ClientRolePolicy(Name: $"policies:{attributesCreateOneNameKebabCase}", Config: new()
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
                Description = $"{AttributesCreateOneName} Policy",
                Logic = "POSITIVE",
            }),
            [AttributesCreateManyName] = new ClientRolePolicy(Name: $"policies:{attributesCreateManyNameKebabCase}", Config: new()
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
                Description = $"{AttributesCreateManyName} Policy",
                Logic = "POSITIVE",
            }),
            [AttributesReadOneName] = new ClientRolePolicy(Name: $"policies:{attributesReadOneNameKebabCase}", Config: new()
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
                Description = $"{AttributesReadOneName} Policy",
                Logic = "POSITIVE",
            }),
            [AttributesReadManyName] = new ClientRolePolicy(Name: $"policies:{attributesReadManyNameKebabCase}", Config: new()
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
                Description = $"{AttributesReadManyName} Policy",
                Logic = "POSITIVE",
            }),
            [AttributesUpdateOneName] = new ClientRolePolicy(Name: $"policies:{attributesUpdateOneNameKebabCase}", Config: new()
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
                Description = $"{AttributesUpdateOneName} Policy",
                Logic = "POSITIVE",
            }),
            [AttributesUpdateManyName] = new ClientRolePolicy(Name: $"policies:{attributesUpdateManyNameKebabCase}", Config: new()
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
                Description = $"{AttributesUpdateManyName} Policy",
                Logic = "POSITIVE",
            }),
            [AttributesDeleteOneName] = new ClientRolePolicy(Name: $"policies:{attributesDeleteOneNameKebabCase}", Config: new()
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
                Description = $"{AttributesDeleteOneName} Policy",
                Logic = "POSITIVE",
            }),
            [AttributesDeleteManyName] = new ClientRolePolicy(Name: $"policies:{attributesDeleteManyNameKebabCase}", Config: new()
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
                Description = $"{AttributesDeleteManyName} Policy",
                Logic = "POSITIVE",
            }),
        };
    }
}
