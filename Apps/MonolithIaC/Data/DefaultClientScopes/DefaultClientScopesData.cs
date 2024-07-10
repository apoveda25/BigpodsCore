using System.Collections.Generic;
using System.Linq;
using Bigpods.MonolithIaC.Data.Clients;
using Bigpods.MonolithIaC.Data.ClientScopes;
using Bigpods.MonolithIaC.Data.Realms;
using Bigpods.MonolithIaC.Utils;
using Pulumi;
using Pulumi.Keycloak.OpenId;

namespace Bigpods.MonolithIaC.Data.DefaultClientScopes;

public sealed record ClientDefaultScope(
    string Name,
    Pulumi.Keycloak.OpenId.ClientDefaultScopesArgs Config
);

public class ClientDefaultScopesData
{
    public static string ClientDefaultScopesName
    {
        get => string.Join(":", Scopes);
    }
    private static string[] Scopes { get; } =
        new[] { "profile", "email", "roles", "web-origins", "acr" };

    public static Dictionary<string, ClientDefaultScope> GetClientDefaultScopes(
        Dictionary<string, Pulumi.Keycloak.Realm> realms,
        Dictionary<string, Pulumi.Keycloak.OpenId.Client> clients,
        Dictionary<string, Pulumi.Keycloak.OpenId.ClientScope> clientScopes
    )
    {
        var bigpodsRealm = realms[RealmsData.BigpodsName];
        var bigpodsGUIAPIClient = clients[ClientsData.BigpodsGUIAPIClientName];
        var audienceClientScope = clientScopes[ClientScopesData.AudienceName];

        string defaultScopesNameKebabCase = StringUtils.ToKebabCase(ClientDefaultScopesName);

        return new Dictionary<string, ClientDefaultScope>
        {
            [ClientDefaultScopesName] = new ClientDefaultScope(
                Name: $"client-default-scopes:{defaultScopesNameKebabCase}",
                Config: new()
                {
                    RealmId = bigpodsRealm.Id.Apply(id => id),
                    ClientId = bigpodsGUIAPIClient.Id.Apply(id => id),
                    DefaultScopes = audienceClientScope.Name.Apply(name => Scopes.Append(name))
                }
            )
        };
    }
}
