using System.Collections.Generic;
using System.Linq;

namespace Bigpods.MonolithIaC.Data.ClientRolePolicies;

public sealed record ClientRolePolicy(string Name, Pulumi.Keycloak.OpenId.ClientRolePolicyArgs Config);

public partial class ClientRolePoliciesData
{
    public static Dictionary<string, ClientRolePolicy> GetClientRolePolicies(
        Dictionary<string, Pulumi.Keycloak.Realm> realms,
        Dictionary<string, Pulumi.Keycloak.OpenId.Client> clients,
        Dictionary<string, Pulumi.Keycloak.Role> roles
    )
    {
        return GetClientRolePoliciesForWarehouses(realms, clients, roles)
            .Concat(GetClientRolePoliciesForInventories(realms, clients, roles))
            .Concat(GetClientRolePoliciesForProducts(realms, clients, roles))
            .Concat(GetClientRolePoliciesForVariants(realms, clients, roles))
            .Concat(GetClientRolePoliciesForAttributes(realms, clients, roles))
            .Concat(GetClientRolePoliciesForAttributeTypes(realms, clients, roles))
            .AsParallel()
            .ToDictionary(item => item.Key, item => item.Value);
    }
}