namespace Bigpods.MonolithIaC.Factories;

public class AudienceProtocolMapperFactory
{
    public static Pulumi.Keycloak.OpenId.AudienceProtocolMapper Build(string name, Pulumi.Keycloak.OpenId.AudienceProtocolMapperArgs args)
    {
        return new Pulumi.Keycloak.OpenId.AudienceProtocolMapper(name, new()
        {
            RealmId = args.RealmId,
            AddToAccessToken = args.AddToAccessToken ?? false,
            AddToIdToken = args.AddToIdToken ?? false,
            ClientId = args.ClientId,
            ClientScopeId = args.ClientScopeId,
            IncludedClientAudience = args.IncludedClientAudience,
            IncludedCustomAudience = args.IncludedCustomAudience,
            Name = args.Name ?? name,
        });
    }
}
