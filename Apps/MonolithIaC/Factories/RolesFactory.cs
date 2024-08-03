namespace Bigpods.MonolithIaC.Factories;

public static class RolesFactory
{
    public static Pulumi.Keycloak.Role Build(string name, Pulumi.Keycloak.RoleArgs args)
    {
        return new Pulumi.Keycloak.Role(
            name,
            new()
            {
                RealmId = args.RealmId,
                Attributes = args.Attributes,
                ClientId = args.ClientId,
                // CompositeRoles = args.CompositeRoles,
                Description = args.Description,
                Name = args.Name ?? name,
            }
        );
    }
}
