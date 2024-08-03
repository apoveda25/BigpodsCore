namespace Bigpods.MonolithIaC.Factories;

public static class ClientAuthorizationScopeFactory
{
    public static Pulumi.Keycloak.OpenId.ClientAuthorizationScope Build(
        string name,
        Pulumi.Keycloak.OpenId.ClientAuthorizationScopeArgs args
    )
    {
        return new Pulumi.Keycloak.OpenId.ClientAuthorizationScope(
            name,
            new()
            {
                RealmId = args.RealmId,
                ResourceServerId = args.ResourceServerId,
                DisplayName = args.DisplayName ?? name,
                IconUri = args.IconUri,
                Name = args.Name ?? name,
            }
        );
    }
}
