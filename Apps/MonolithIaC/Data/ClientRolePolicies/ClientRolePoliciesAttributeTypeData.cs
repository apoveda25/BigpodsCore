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
    public static string AttributeTypesCreateOneName { get; } = $"{ClientAuthorizationResourcesData.AttributeTypesName}:{ClientAuthorizationScopesData.CreateOneName}";
    public static string AttributeTypesCreateManyName { get; } = $"{ClientAuthorizationResourcesData.AttributeTypesName}:{ClientAuthorizationScopesData.CreateManyName}";
    public static string AttributeTypesReadOneName { get; } = $"{ClientAuthorizationResourcesData.AttributeTypesName}:{ClientAuthorizationScopesData.ReadOneName}";
    public static string AttributeTypesReadManyName { get; } = $"{ClientAuthorizationResourcesData.AttributeTypesName}:{ClientAuthorizationScopesData.ReadManyName}";
    public static string AttributeTypesUpdateOneName { get; } = $"{ClientAuthorizationResourcesData.AttributeTypesName}:{ClientAuthorizationScopesData.UpdateOneName}";
    public static string AttributeTypesUpdateManyName { get; } = $"{ClientAuthorizationResourcesData.AttributeTypesName}:{ClientAuthorizationScopesData.UpdateManyName}";
    public static string AttributeTypesDeleteOneName { get; } = $"{ClientAuthorizationResourcesData.AttributeTypesName}:{ClientAuthorizationScopesData.DeleteOneName}";
    public static string AttributeTypesDeleteManyName { get; } = $"{ClientAuthorizationResourcesData.AttributeTypesName}:{ClientAuthorizationScopesData.DeleteManyName}";

    private static Dictionary<string, ClientRolePolicy> GetClientRolePoliciesForAttributeTypes(
        Dictionary<string, Pulumi.Keycloak.Realm> realms,
        Dictionary<string, Pulumi.Keycloak.OpenId.Client> clients,
        Dictionary<string, Pulumi.Keycloak.Role> roles
    )
    {
        var bigpodsRealm = realms[RealmsData.BigpodsName];
        var bigpodsMonolithAPIClient = clients[ClientsData.BigpodsMonolithAPIClientName];
        var bigpodsAdminRole = roles[RolesData.AdminName];
        var bigpodsProductTeamRole = roles[RolesData.ProductTeamName];

        string attributeTypesCreateOneNameKebabCase = StringUtils.ToKebabCase(AttributeTypesCreateOneName);
        string attributeTypesCreateManyNameKebabCase = StringUtils.ToKebabCase(AttributeTypesCreateManyName);
        string attributeTypesReadOneNameKebabCase = StringUtils.ToKebabCase(AttributeTypesReadOneName);
        string attributeTypesReadManyNameKebabCase = StringUtils.ToKebabCase(AttributeTypesReadManyName);
        string attributeTypesUpdateOneNameKebabCase = StringUtils.ToKebabCase(AttributeTypesUpdateOneName);
        string attributeTypesUpdateManyNameKebabCase = StringUtils.ToKebabCase(AttributeTypesUpdateManyName);
        string attributeTypesDeleteOneNameKebabCase = StringUtils.ToKebabCase(AttributeTypesDeleteOneName);
        string attributeTypesDeleteManyNameKebabCase = StringUtils.ToKebabCase(AttributeTypesDeleteManyName);

        return new Dictionary<string, ClientRolePolicy>
        {
            [AttributeTypesCreateOneName] = new ClientRolePolicy(Name: $"policies:{attributeTypesCreateOneNameKebabCase}", Config: new()
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
                Description = $"{AttributeTypesCreateOneName} Policy",
                Logic = "POSITIVE",
            }),
            [AttributeTypesCreateManyName] = new ClientRolePolicy(Name: $"policies:{attributeTypesCreateManyNameKebabCase}", Config: new()
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
                Description = $"{AttributeTypesCreateManyName} Policy",
                Logic = "POSITIVE",
            }),
            [AttributeTypesReadOneName] = new ClientRolePolicy(Name: $"policies:{attributeTypesReadOneNameKebabCase}", Config: new()
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
                Description = $"{AttributeTypesReadOneName} Policy",
                Logic = "POSITIVE",
            }),
            [AttributeTypesReadManyName] = new ClientRolePolicy(Name: $"policies:{attributeTypesReadManyNameKebabCase}", Config: new()
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
                Description = $"{AttributeTypesReadManyName} Policy",
                Logic = "POSITIVE",
            }),
            [AttributeTypesUpdateOneName] = new ClientRolePolicy(Name: $"policies:{attributeTypesUpdateOneNameKebabCase}", Config: new()
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
                Description = $"{AttributeTypesUpdateOneName} Policy",
                Logic = "POSITIVE",
            }),
            [AttributeTypesUpdateManyName] = new ClientRolePolicy(Name: $"policies:{attributeTypesUpdateManyNameKebabCase}", Config: new()
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
                Description = $"{AttributeTypesUpdateManyName} Policy",
                Logic = "POSITIVE",
            }),
            [AttributeTypesDeleteOneName] = new ClientRolePolicy(Name: $"policies:{attributeTypesDeleteOneNameKebabCase}", Config: new()
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
                Description = $"{AttributeTypesDeleteOneName} Policy",
                Logic = "POSITIVE",
            }),
            [AttributeTypesDeleteManyName] = new ClientRolePolicy(Name: $"policies:{attributeTypesDeleteManyNameKebabCase}", Config: new()
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
                Description = $"{AttributeTypesDeleteManyName} Policy",
                Logic = "POSITIVE",
            }),
        };
    }
}
