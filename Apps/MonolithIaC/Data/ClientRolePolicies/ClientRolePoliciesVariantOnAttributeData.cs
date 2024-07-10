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
    public static string VariantsOnAttributesAttachOneName { get; } =
        $"{ClientAuthorizationResourcesData.VariantsOnAttributesName}:{ClientAuthorizationScopesData.AttachOneName}";
    public static string VariantsOnAttributesAttachManyName { get; } =
        $"{ClientAuthorizationResourcesData.VariantsOnAttributesName}:{ClientAuthorizationScopesData.AttachManyName}";
    public static string VariantsOnAttributesDettachOneName { get; } =
        $"{ClientAuthorizationResourcesData.VariantsOnAttributesName}:{ClientAuthorizationScopesData.DettachOneName}";
    public static string VariantsOnAttributesDettachManyName { get; } =
        $"{ClientAuthorizationResourcesData.VariantsOnAttributesName}:{ClientAuthorizationScopesData.DettachManyName}";
    public static string VariantsOnAttributesReadOneName { get; } =
        $"{ClientAuthorizationResourcesData.VariantsOnAttributesName}:{ClientAuthorizationScopesData.ReadOneName}";
    public static string VariantsOnAttributesReadManyName { get; } =
        $"{ClientAuthorizationResourcesData.VariantsOnAttributesName}:{ClientAuthorizationScopesData.ReadManyName}";

    private static Dictionary<
        string,
        ClientRolePolicy
    > GetClientRolePoliciesForVariantsOnAttributes(
        Dictionary<string, Pulumi.Keycloak.Realm> realms,
        Dictionary<string, Pulumi.Keycloak.OpenId.Client> clients,
        Dictionary<string, Pulumi.Keycloak.Role> roles
    )
    {
        var bigpodsRealm = realms[RealmsData.BigpodsName];
        var bigpodsMonolithAPIClient = clients[ClientsData.BigpodsMonolithAPIClientName];
        var bigpodsAdminRole = roles[RolesData.AdminName];
        var bigpodsProductTeamRole = roles[RolesData.ProductTeamName];

        string variantsAttachOneNameKebabCase = StringUtils.ToKebabCase(
            VariantsOnAttributesAttachOneName
        );
        string variantsAttachManyNameKebabCase = StringUtils.ToKebabCase(
            VariantsOnAttributesAttachManyName
        );
        string variantsDettachOneNameKebabCase = StringUtils.ToKebabCase(
            VariantsOnAttributesDettachOneName
        );
        string variantsDettachManyNameKebabCase = StringUtils.ToKebabCase(
            VariantsOnAttributesDettachManyName
        );
        string variantsReadOneNameKebabCase = StringUtils.ToKebabCase(
            VariantsOnAttributesReadOneName
        );
        string variantsReadManyNameKebabCase = StringUtils.ToKebabCase(
            VariantsOnAttributesReadManyName
        );

        return new Dictionary<string, ClientRolePolicy>
        {
            [VariantsOnAttributesAttachOneName] = new ClientRolePolicy(
                Name: $"policies:{variantsAttachOneNameKebabCase}",
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
                    Description = $"{VariantsOnAttributesAttachOneName} Policy",
                    Logic = "POSITIVE",
                }
            ),
            [VariantsOnAttributesAttachManyName] = new ClientRolePolicy(
                Name: $"policies:{variantsAttachManyNameKebabCase}",
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
                    }
                }
            ),
            [VariantsOnAttributesDettachOneName] = new ClientRolePolicy(
                Name: $"policies:{variantsDettachOneNameKebabCase}",
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
                    Description = $"{VariantsOnAttributesDettachOneName} Policy",
                    Logic = "POSITIVE",
                }
            ),
            [VariantsOnAttributesDettachManyName] = new ClientRolePolicy(
                Name: $"policies:{variantsDettachManyNameKebabCase}",
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
                    Description = $"{VariantsOnAttributesDettachManyName} Policy",
                    Logic = "POSITIVE",
                }
            ),
            [VariantsOnAttributesReadOneName] = new ClientRolePolicy(
                Name: $"policies:{variantsReadOneNameKebabCase}",
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
                    Description = $"{VariantsOnAttributesReadOneName} Policy",
                    Logic = "POSITIVE",
                }
            ),
            [VariantsOnAttributesReadManyName] = new ClientRolePolicy(
                Name: $"policies:{variantsReadManyNameKebabCase}",
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
                    Description = $"{VariantsOnAttributesReadManyName} Policy",
                    Logic = "POSITIVE",
                }
            ),
        };
    }
}
