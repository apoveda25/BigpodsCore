namespace Bigpods.MonolithIaC.Factories;

public class ClientDefaultScopeFactory
{
    public static Pulumi.Keycloak.OpenId.ClientDefaultScopes Build(
        string name,
        Pulumi.Keycloak.OpenId.ClientDefaultScopesArgs args
    )
    {
        return new Pulumi.Keycloak.OpenId.ClientDefaultScopes(
            name,
            new()
            {
                RealmId = args.RealmId,
                ClientId = args.ClientId,
                DefaultScopes = args.DefaultScopes
            }
        );
    }
}
