using System.Collections.Generic;

using Bigpods.MonolithIaC.Data.Realms;
using Bigpods.MonolithIaC.Utils;

namespace Bigpods.MonolithIaC.Data.ClientScopes;

public sealed record ClientScopeData(string Name, Pulumi.Keycloak.OpenId.ClientScopeArgs Config);

public class ClientScopesData
{
    public static string AudienceName { get; } = "Audience";

    public static Dictionary<string, ClientScopeData> GetClientScopes(
        Dictionary<string, Pulumi.Keycloak.Realm> realms
    )
    {
        var bigpodsRealm = realms[RealmsData.BigpodsName];

        string audienceNameKebabCase = StringUtils.ToKebabCase(AudienceName);

        return new Dictionary<string, ClientScopeData>
        {
            [AudienceName] = new ClientScopeData(Name: $"client-scopes:{audienceNameKebabCase}", Config: new()
            {
                Name = $"client-scopes:{audienceNameKebabCase}",
                RealmId = bigpodsRealm.Id.Apply(id => id),
                Description = $"{AudienceName} Client Scope",
                IncludeInTokenScope = true,
            }),
        };
    }
}
