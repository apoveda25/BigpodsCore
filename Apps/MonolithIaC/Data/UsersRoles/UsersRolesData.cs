using System.Collections.Generic;
using Bigpods.MonolithIaC.Data.Realms;
using Bigpods.MonolithIaC.Data.Roles;
using Bigpods.MonolithIaC.Data.Users;
using Bigpods.MonolithIaC.Utils;

namespace Bigpods.MonolithIaC.Data.UsersRoles;

public sealed record UserRoles(string Name, Pulumi.Keycloak.UserRolesArgs Config);

public static class UsersRolesData
{
    public static string AdminUserToAdminRoleName { get; } =
        $"{UsersData.AdminName}:{RolesData.AdminName}";

    public static Dictionary<string, UserRoles> GetUsersRoles(
        Dictionary<string, Pulumi.Keycloak.Realm> realms,
        Dictionary<string, Pulumi.Keycloak.User> users,
        Dictionary<string, Pulumi.Keycloak.Role> roles,
        Dictionary<string, Pulumi.Output<Pulumi.Keycloak.GetRoleResult>> gettingRoles
    )
    {
        var bigpodsRealm = realms[RealmsData.BigpodsName];
        var adminUser = users[UsersData.AdminName];
        var adminRole = roles[RolesData.AdminName];
        var defaultRole = gettingRoles[RolesData.DefaultName];

        string adminUserToAdminRoleNameKebabCase = StringUtils.ToKebabCase(
            AdminUserToAdminRoleName
        );

        return new Dictionary<string, UserRoles>
        {
            [AdminUserToAdminRoleName] = new UserRoles(
                Name: $"user-roles:{adminUserToAdminRoleNameKebabCase}",
                Config: new()
                {
                    RealmId = bigpodsRealm.Id.Apply(id => id),
                    UserId = adminUser.Id.Apply(id => id),
                    RoleIds = new[] { adminRole.Id, defaultRole.Apply(role => role.Id) },
                }
            ),
        };
    }
}
