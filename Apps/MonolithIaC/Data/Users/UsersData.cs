using System.Collections.Generic;

using Bigpods.MonolithIaC.Data.Realms;

using Bigpods.MonolithIaC.Utils;

namespace Bigpods.MonolithIaC.Data.Users;

public sealed record User(string Name, Pulumi.Keycloak.UserArgs Config);

public static class UsersData
{
    public static string AdminName { get; } = "admin";

    public static Dictionary<string, User> GetUsers(Dictionary<string, Pulumi.Keycloak.Realm> realms)
    {
        var bigpodsRealm = realms[RealmsData.BigpodsName];

        string adminNameKebabCase = StringUtils.ToKebabCase(AdminName);

        return new Dictionary<string, User>
        {
            [AdminName] = new User(Name: $"users:{adminNameKebabCase}", Config: new()
            {
                RealmId = bigpodsRealm.Id.Apply(id => id),
                Username = AdminName,
                Email = $"{AdminName}@bigpods.com",
                EmailVerified = true,
                Enabled = true,
                FirstName = AdminName,
                LastName = "",
                InitialPassword = new Pulumi.Keycloak.Inputs.UserInitialPasswordArgs
                {
                    Value = "Secret123",
                    Temporary = false,
                },
            }),
        };
    }
}