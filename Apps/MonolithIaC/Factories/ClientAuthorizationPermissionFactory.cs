namespace Bigpods.MonolithIaC.Factories;

public static class ClientAuthorizationPermissionFactory
{
    public static Pulumi.Keycloak.OpenId.ClientAuthorizationPermission Build(
        string name,
        Pulumi.Keycloak.OpenId.ClientAuthorizationPermissionArgs args
    )
    {
        return new Pulumi.Keycloak.OpenId.ClientAuthorizationPermission(
            name,
            new()
            {
                RealmId = args.RealmId,
                ResourceServerId = args.ResourceServerId,
                DecisionStrategy = args.DecisionStrategy,
                Description = args.Description,
                Name = args.Name ?? name,
                Policies = args.Policies,
                ResourceType = args.ResourceType,
                Resources = args.Resources,
                Scopes = args.Scopes,
                Type = args.Type,
            }
        );
    }
}
