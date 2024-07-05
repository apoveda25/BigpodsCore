using System.Collections.Generic;
using System.Linq;

using Bigpods.MonolithIaC.Data.ClientAuthorizationResources;

using Bigpods.MonolithIaC.Data.Clients;
using Bigpods.MonolithIaC.Data.Realms;

using Bigpods.MonolithIaC.Utils;

using Pulumi;

namespace Bigpods.MonolithIaC.Data.ClientAuthorizationPermissions;

public partial class ClientAuthorizationPermissionsData
{
    public static string ResourceAttributeTypesName { get; } = ClientAuthorizationResourcesData.AttributeTypesName;
    public static string ResourceAttributesName { get; } = ClientAuthorizationResourcesData.AttributesName;
    public static string ResourceVariantsName { get; } = ClientAuthorizationResourcesData.VariantsName;
    public static string ResourceProductsName { get; } = ClientAuthorizationResourcesData.ProductsName;
    public static string ResourceWarehousesName { get; } = ClientAuthorizationResourcesData.WarehousesName;

    private static Dictionary<string, ClientAuthorizationPermission> GetClientAuthorizationPermissionsTypeResource(
        Dictionary<string, Pulumi.Keycloak.Realm> realms,
        Dictionary<string, Pulumi.Keycloak.OpenId.Client> clients,
        Dictionary<string, Pulumi.Keycloak.OpenId.ClientRolePolicy> policies,
        Dictionary<string, Pulumi.Keycloak.OpenId.ClientAuthorizationResource> resources
    )
    {
        var bigpodsRealm = realms[RealmsData.BigpodsName];
        var bigpodsMonolithAPIClient = clients[ClientsData.BigpodsMonolithAPIClientName];

        var attributeTypesPolicies = policies.Where(policy => policy.Key.Contains(ClientAuthorizationResourcesData.AttributeTypesName)).Select(policy => policy.Value.Name);
        var attributesPolicies = policies.Where(policy => policy.Key.Contains(ClientAuthorizationResourcesData.AttributesName)).Select(policy => policy.Value.Name);
        var variantsPolicies = policies.Where(policy => policy.Key.Contains(ClientAuthorizationResourcesData.VariantsName)).Select(policy => policy.Value.Name);
        var productsPolicies = policies.Where(policy => policy.Key.Contains(ClientAuthorizationResourcesData.ProductsName)).Select(policy => policy.Value.Name);
        var warehousesPolicies = policies.Where(policy => policy.Key.Contains(ClientAuthorizationResourcesData.WarehousesName)).Select(policy => policy.Value.Name);

        string resourceAttributeTypesNameKebabCase = StringUtils.ToKebabCase(ResourceAttributeTypesName);
        string resourceAttributesNameKebabCase = StringUtils.ToKebabCase(ResourceAttributesName);
        string resourceVariantsNameKebabCase = StringUtils.ToKebabCase(ResourceVariantsName);
        string resourceProductsNameKebabCase = StringUtils.ToKebabCase(ResourceProductsName);
        string resourceWarehousesNameKebabCase = StringUtils.ToKebabCase(ResourceWarehousesName);

        return new Dictionary<string, ClientAuthorizationPermission>
        {
            [ResourceAttributeTypesName] = new ClientAuthorizationPermission(Name: $"permissions:{resourceAttributeTypesNameKebabCase}", Config: new()
            {
                RealmId = bigpodsRealm.Id.Apply(id => id),
                ResourceServerId = bigpodsMonolithAPIClient.Id.Apply(id => id),
                DecisionStrategy = "UNANIMOUS",
                Description = $"{ResourceAttributeTypesName} Permission",
                Policies = Output.All(attributeTypesPolicies),
                Resources = resources[ClientAuthorizationResourcesData.AttributeTypesName].Name.Apply(name => new[] { name }),
                Type = "resource"
            }),
            [ResourceAttributesName] = new ClientAuthorizationPermission(Name: $"permissions:{resourceAttributesNameKebabCase}", Config: new()
            {
                RealmId = bigpodsRealm.Id.Apply(id => id),
                ResourceServerId = bigpodsMonolithAPIClient.Id.Apply(id => id),
                DecisionStrategy = "UNANIMOUS",
                Description = $"{ResourceAttributesName} Permission",
                Policies = Output.All(attributesPolicies),
                Resources = resources[ClientAuthorizationResourcesData.AttributesName].Name.Apply(name => new[] { name }),
                Type = "resource"
            }),
            [ResourceVariantsName] = new ClientAuthorizationPermission(Name: $"permissions:{resourceVariantsNameKebabCase}", Config: new()
            {
                RealmId = bigpodsRealm.Id.Apply(id => id),
                ResourceServerId = bigpodsMonolithAPIClient.Id.Apply(id => id),
                DecisionStrategy = "UNANIMOUS",
                Description = $"{ResourceVariantsName} Permission",
                Policies = Output.All(variantsPolicies),
                Resources = resources[ClientAuthorizationResourcesData.VariantsName].Name.Apply(name => new[] { name }),
                Type = "resource"
            }),
            [ResourceProductsName] = new ClientAuthorizationPermission(Name: $"permissions:{resourceProductsNameKebabCase}", Config: new()
            {
                RealmId = bigpodsRealm.Id.Apply(id => id),
                ResourceServerId = bigpodsMonolithAPIClient.Id.Apply(id => id),
                DecisionStrategy = "UNANIMOUS",
                Description = $"{ResourceProductsName} Permission",
                Policies = Output.All(productsPolicies),
                Resources = resources[ClientAuthorizationResourcesData.ProductsName].Name.Apply(name => new[] { name }),
                Type = "resource"
            }),
            [ResourceWarehousesName] = new ClientAuthorizationPermission(Name: $"permissions:{resourceWarehousesNameKebabCase}", Config: new()
            {
                RealmId = bigpodsRealm.Id.Apply(id => id),
                ResourceServerId = bigpodsMonolithAPIClient.Id.Apply(id => id),
                DecisionStrategy = "UNANIMOUS",
                Description = $"{ResourceWarehousesName} Permission",
                Policies = Output.All(warehousesPolicies),
                Resources = resources[ClientAuthorizationResourcesData.WarehousesName].Name.Apply(name => new[] { name }),
                Type = "resource"
            }),
        };
    }
}
