namespace Bigpods.MonolithIaC.Factories;

public class ClientRolePolicyFactory
{
    public static Pulumi.Keycloak.OpenId.ClientRolePolicy Build(string name, Pulumi.Keycloak.OpenId.ClientRolePolicyArgs args)
    {
        return new Pulumi.Keycloak.OpenId.ClientRolePolicy(name, new()
        {
            RealmId = args.RealmId,
            ResourceServerId = args.ResourceServerId,
            Roles = args.Roles,
            Type = args.Type ?? "client",
            DecisionStrategy = args.DecisionStrategy ?? "UNANIMOUS",
            Description = args.Description,
            Logic = args.Logic ?? "POSITIVE",
            Name = args.Name ?? name,
        });
    }
}
