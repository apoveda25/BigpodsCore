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
    public static string VariantsCreateOneName { get; } =
        $"{ClientAuthorizationResourcesData.VariantsName}:{ClientAuthorizationScopesData.CreateOneName}";
    public static string VariantsCreateManyName { get; } =
        $"{ClientAuthorizationResourcesData.VariantsName}:{ClientAuthorizationScopesData.CreateManyName}";
    public static string VariantsReadOneName { get; } =
        $"{ClientAuthorizationResourcesData.VariantsName}:{ClientAuthorizationScopesData.ReadOneName}";
    public static string VariantsReadManyName { get; } =
        $"{ClientAuthorizationResourcesData.VariantsName}:{ClientAuthorizationScopesData.ReadManyName}";
    public static string VariantsUpdateOneName { get; } =
        $"{ClientAuthorizationResourcesData.VariantsName}:{ClientAuthorizationScopesData.UpdateOneName}";
    public static string VariantsUpdateManyName { get; } =
        $"{ClientAuthorizationResourcesData.VariantsName}:{ClientAuthorizationScopesData.UpdateManyName}";
    public static string VariantsDeleteOneName { get; } =
        $"{ClientAuthorizationResourcesData.VariantsName}:{ClientAuthorizationScopesData.DeleteOneName}";
    public static string VariantsDeleteManyName { get; } =
        $"{ClientAuthorizationResourcesData.VariantsName}:{ClientAuthorizationScopesData.DeleteManyName}";

    private static Dictionary<string, ClientRolePolicy> GetClientRolePoliciesForVariants(
        Dictionary<string, Pulumi.Keycloak.Realm> realms,
        Dictionary<string, Pulumi.Keycloak.OpenId.Client> clients,
        Dictionary<string, Pulumi.Keycloak.Role> roles
    )
    {
        var bigpodsRealm = realms[RealmsData.BigpodsName];
        var bigpodsMonolithAPIClient = clients[ClientsData.BigpodsMonolithAPIClientName];
        var bigpodsAdminRole = roles[RolesData.AdminName];
        var bigpodsProductTeamRole = roles[RolesData.ProductTeamName];

        string variantsCreateOneNameKebabCase = StringUtils.ToKebabCase(VariantsCreateOneName);
        string variantsCreateManyNameKebabCase = StringUtils.ToKebabCase(VariantsCreateManyName);
        string variantsReadOneNameKebabCase = StringUtils.ToKebabCase(VariantsReadOneName);
        string variantsReadManyNameKebabCase = StringUtils.ToKebabCase(VariantsReadManyName);
        string variantsUpdateOneNameKebabCase = StringUtils.ToKebabCase(VariantsUpdateOneName);
        string variantsUpdateManyNameKebabCase = StringUtils.ToKebabCase(VariantsUpdateManyName);
        string variantsDeleteOneNameKebabCase = StringUtils.ToKebabCase(VariantsDeleteOneName);
        string variantsDeleteManyNameKebabCase = StringUtils.ToKebabCase(VariantsDeleteManyName);

        return new Dictionary<string, ClientRolePolicy>
        {
            [VariantsCreateOneName] = new ClientRolePolicy(
                Name: $"policies:{variantsCreateOneNameKebabCase}",
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
                    Description = $"{VariantsCreateOneName} Policy",
                    Logic = "POSITIVE",
                }
            ),
            [VariantsCreateManyName] = new ClientRolePolicy(
                Name: $"policies:{variantsCreateManyNameKebabCase}",
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
                    }
                }
            ),
            [VariantsReadOneName] = new ClientRolePolicy(
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
                    Description = $"{VariantsReadOneName} Policy",
                    Logic = "POSITIVE",
                }
            ),
            [VariantsReadManyName] = new ClientRolePolicy(
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
                    Description = $"{VariantsReadManyName} Policy",
                    Logic = "POSITIVE",
                }
            ),
            [VariantsUpdateOneName] = new ClientRolePolicy(
                Name: $"policies:{variantsUpdateOneNameKebabCase}",
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
                    Description = $"{VariantsUpdateOneName} Policy",
                    Logic = "POSITIVE",
                }
            ),
            [VariantsUpdateManyName] = new ClientRolePolicy(
                Name: $"policies:{variantsUpdateManyNameKebabCase}",
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
                    Description = $"{VariantsUpdateManyName} Policy",
                    Logic = "POSITIVE",
                }
            ),
            [VariantsDeleteOneName] = new ClientRolePolicy(
                Name: $"policies:{variantsDeleteOneNameKebabCase}",
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
                    Description = $"{VariantsDeleteOneName} Policy",
                    Logic = "POSITIVE",
                }
            ),
            [VariantsDeleteManyName] = new ClientRolePolicy(
                Name: $"policies:{variantsDeleteManyNameKebabCase}",
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
                    Description = $"{VariantsDeleteManyName} Policy",
                    Logic = "POSITIVE",
                }
            ),
        };
    }
}
