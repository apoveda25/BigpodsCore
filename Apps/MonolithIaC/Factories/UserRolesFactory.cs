namespace Bigpods.MonolithIaC.Factories;

public class UserRolesFactory
{
    public static Pulumi.Keycloak.UserRoles Build(string name, Pulumi.Keycloak.UserRolesArgs args)
    {
        return new Pulumi.Keycloak.UserRoles(
            name,
            new()
            {
                RealmId = args.RealmId,
                RoleIds = args.RoleIds,
                UserId = args.UserId,
                Exhaustive = args.Exhaustive ?? false,
            }
        );
    }
}
