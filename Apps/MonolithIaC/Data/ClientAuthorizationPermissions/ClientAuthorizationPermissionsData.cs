using System.Collections.Generic;
using System.Linq;

namespace Bigpods.MonolithIaC.Data.ClientAuthorizationPermissions;

public sealed record ClientAuthorizationPermission(
    string Name,
    Pulumi.Keycloak.OpenId.ClientAuthorizationPermissionArgs Config
);

public partial class ClientAuthorizationPermissionsData
{
    public static Dictionary<
        string,
        ClientAuthorizationPermission
    > GetClientAuthorizationPermissions(
        Dictionary<string, Pulumi.Keycloak.Realm> realms,
        Dictionary<string, Pulumi.Keycloak.OpenId.Client> clients,
        Dictionary<string, Pulumi.Keycloak.OpenId.ClientRolePolicy> policies,
        Dictionary<string, Pulumi.Keycloak.OpenId.ClientAuthorizationResource> resources,
        Dictionary<string, Pulumi.Keycloak.OpenId.ClientAuthorizationScope> scopes
    )
    {
        return GetClientAuthorizationPermissionsTypeResource(realms, clients, policies, resources)
            .Concat(GetClientAuthorizationPermissionsTypeScope(realms, clients, policies, scopes))
            .AsParallel()
            .ToDictionary(item => item.Key, item => item.Value);
    }
}
