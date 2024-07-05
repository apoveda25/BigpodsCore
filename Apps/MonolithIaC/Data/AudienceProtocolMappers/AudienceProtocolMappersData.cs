using System.Collections.Generic;

using Bigpods.MonolithIaC.Data.Clients;
using Bigpods.MonolithIaC.Data.ClientScopes;
using Bigpods.MonolithIaC.Data.Realms;
using Bigpods.MonolithIaC.Utils;

namespace Bigpods.MonolithIaC.Data.AudienceProtocolMappers;

public sealed record AudienceProtocolMapper(string Name, Pulumi.Keycloak.OpenId.AudienceProtocolMapperArgs Config);

public class AudienceProtocolMappersData
{
    public static string AudienceBigpodsGUIAPIClientName { get; } = $"{ClientScopesData.AudienceName}:{ClientsData.BigpodsMonolithAPIClientName}";

    public static Dictionary<string, AudienceProtocolMapper> GetAudienceProtocolMappers(
        Dictionary<string, Pulumi.Keycloak.Realm> realms,
        Dictionary<string, Pulumi.Keycloak.OpenId.Client> clients,
    Dictionary<string, Pulumi.Keycloak.OpenId.ClientScope> clientScopes
    )
    {
        var bigpodsRealm = realms[RealmsData.BigpodsName];
        var bigpodsMonolithAPIClient = clients[ClientsData.BigpodsMonolithAPIClientName];
        var bigpodsGUIAPIClient = clients[ClientsData.BigpodsGUIAPIClientName];
        var audienceBigpodsClientScope = clientScopes[ClientScopesData.AudienceName];

        string audienceBigpodsGUIAPIClientNameKebabCase = StringUtils.ToKebabCase(AudienceBigpodsGUIAPIClientName);

        return new Dictionary<string, AudienceProtocolMapper>()
        {
            [AudienceBigpodsGUIAPIClientName] = new AudienceProtocolMapper(Name: $"protocol-mappers:{audienceBigpodsGUIAPIClientNameKebabCase}", Config: new()
            {
                Name = $"protocol-mappers:{audienceBigpodsGUIAPIClientNameKebabCase}",
                RealmId = bigpodsRealm.Id.Apply(id => id),
                AddToAccessToken = true,
                AddToIdToken = true,
                ClientScopeId = audienceBigpodsClientScope.Id.Apply(id => id),
                IncludedClientAudience = bigpodsMonolithAPIClient.ClientId.Apply(id => id)
            }),
        };
    }
}
