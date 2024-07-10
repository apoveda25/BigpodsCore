using System.Collections.Generic;
using Bigpods.MonolithIaC.Data.Clients;
using Bigpods.MonolithIaC.Data.Realms;
using Bigpods.MonolithIaC.Utils;

namespace Bigpods.MonolithIaC.Data.Roles;

public sealed record Role(string Name, Pulumi.Keycloak.RoleArgs Config);

public static class RolesData
{
    public static string DefaultName { get; } = "Default roles bigpods";
    public static string AdminName { get; } = "Admin";
    public static string ProductTeamName { get; } = "Product team";

    public static Dictionary<string, Role> GetRoles(
        Dictionary<string, Pulumi.Keycloak.Realm> realms,
        Dictionary<string, Pulumi.Keycloak.OpenId.Client> clients,
        Dictionary<string, Pulumi.Output<Pulumi.Keycloak.GetRoleResult>> gettingRoles
    )
    {
        var bigpodsRealm = realms[RealmsData.BigpodsName];
        var bigpodsMonolithAPIClient = clients[ClientsData.BigpodsMonolithAPIClientName];

        string adminNameKebabCase = StringUtils.ToKebabCase(AdminName);
        string productTeamNameKebabCase = StringUtils.ToKebabCase(ProductTeamName);

        var defaultRole = gettingRoles[DefaultName];

        return new Dictionary<string, Role>
        {
            [AdminName] = new Role(
                Name: $"roles:{adminNameKebabCase}",
                Config: new()
                {
                    RealmId = bigpodsRealm.Id.Apply(id => id),
                    // ClientId = bigpodsMonolithAPIClient.Id.Apply(id => id),
                    Name = $"{AdminName} Role",
                    Description = $"{AdminName} Role",
                    // CompositeRoles = new[] { defaultRole.Apply(role => role.Id) },
                }
            ),
            [ProductTeamName] = new Role(
                Name: $"roles:{productTeamNameKebabCase}",
                Config: new()
                {
                    RealmId = bigpodsRealm.Id.Apply(id => id),
                    // ClientId = bigpodsMonolithAPIClient.Id.Apply(id => id),
                    Name = $"{ProductTeamName} Role",
                    Description = $"{ProductTeamName} Role",
                    // CompositeRoles = new[] { defaultRole.Apply(role => role.Id) },
                }
            ),
        };
    }

    public static Dictionary<string, Pulumi.Output<Pulumi.Keycloak.GetRoleResult>> InvokeRoles(
        Dictionary<string, Pulumi.Keycloak.Realm> realms
    )
    {
        var bigpodsRealm = realms[RealmsData.BigpodsName];

        string defaultNameKebabCase = StringUtils.ToKebabCase(DefaultName);

        return new Dictionary<string, Pulumi.Output<Pulumi.Keycloak.GetRoleResult>>()
        {
            [DefaultName] = Pulumi.Keycloak.GetRole.Invoke(
                new() { RealmId = bigpodsRealm.Id.Apply(id => id), Name = defaultNameKebabCase, }
            ),
        };
    }
}
