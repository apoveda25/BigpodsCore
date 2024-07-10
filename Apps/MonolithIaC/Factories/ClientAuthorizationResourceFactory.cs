namespace Bigpods.MonolithIaC.Factories;

public class ClientAuthorizationResourceFactory
{
    public static Pulumi.Keycloak.OpenId.ClientAuthorizationResource Build(
        string name,
        Pulumi.Keycloak.OpenId.ClientAuthorizationResourceArgs args
    )
    {
        return new Pulumi.Keycloak.OpenId.ClientAuthorizationResource(
            name,
            new()
            {
                RealmId = args.RealmId,
                ResourceServerId = args.ResourceServerId,
                Attributes = args.Attributes,
                DisplayName = args.DisplayName ?? name,
                IconUri = args.IconUri,
                Name = args.Name ?? name,
                OwnerManagedAccess = args.OwnerManagedAccess ?? false,
                Scopes = args.Scopes,
                Type = args.Type,
                Uris = args.Uris,
            }
        );
    }
}
