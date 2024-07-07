using System.Collections.Generic;
using System.Linq;

using Bigpods.MonolithIaC.Data.ClientAuthorizationScopes;

using Bigpods.MonolithIaC.Data.Clients;

using Bigpods.MonolithIaC.Data.Realms;

using Bigpods.MonolithIaC.Utils;

namespace Bigpods.MonolithIaC.Data.ClientAuthorizationResources;

public sealed record ClientAuthorizationResource(string Name, Pulumi.Keycloak.OpenId.ClientAuthorizationResourceArgs Config);

public class ClientAuthorizationResourcesData
{
    public static string WarehousesName { get; } = "Warehouses";
    public static string InventoriesName { get; } = "Inventories";
    public static string ProductsName { get; } = "Products";
    public static string VariantsName { get; } = "Variants";
    public static string AttributesName { get; } = "Attributes";
    public static string AttributeTypesName { get; } = "AttributeTypes";

    public static Dictionary<string, ClientAuthorizationResource> GetClientAuthorizationResources(
        Dictionary<string, Pulumi.Keycloak.Realm> realms,
        Dictionary<string, Pulumi.Keycloak.OpenId.Client> clients,
        Dictionary<string, Pulumi.Keycloak.OpenId.ClientAuthorizationScope> scopes
    )
    {
        var bigpodsRealm = realms[RealmsData.BigpodsName];
        var bigpodsMonolithAPIClient = clients[ClientsData.BigpodsMonolithAPIClientName];
        var createOneScope = scopes[ClientAuthorizationScopesData.CreateOneName];

        string warehousesNameKebabCase = StringUtils.ToKebabCase(WarehousesName);
        string inventoriesNameKebabCase = StringUtils.ToKebabCase(InventoriesName);
        string productsNameKebabCase = StringUtils.ToKebabCase(ProductsName);
        string variantsNameKebabCase = StringUtils.ToKebabCase(VariantsName);
        string attributesNameKebabCase = StringUtils.ToKebabCase(AttributesName);
        string attributeTypesNameKebabCase = StringUtils.ToKebabCase(AttributeTypesName);

        return new Dictionary<string, ClientAuthorizationResource>
        {
            [WarehousesName] = new ClientAuthorizationResource(Name: $"resources:{warehousesNameKebabCase}", Config: new()
            {
                RealmId = bigpodsRealm.Id.Apply(id => id),
                ResourceServerId = bigpodsMonolithAPIClient.Id.Apply(id => id),
                DisplayName = WarehousesName,
                OwnerManagedAccess = true,
                Scopes = scopes.AsParallel().Select(x => x.Value.Name).ToArray(),
            }),
            [InventoriesName] = new ClientAuthorizationResource(Name: $"resources:{inventoriesNameKebabCase}", Config: new()
            {
                RealmId = bigpodsRealm.Id.Apply(id => id),
                ResourceServerId = bigpodsMonolithAPIClient.Id.Apply(id => id),
                DisplayName = InventoriesName,
                OwnerManagedAccess = true,
                Scopes = scopes.AsParallel().Select(x => x.Value.Name).ToArray(),
            }),
            [ProductsName] = new ClientAuthorizationResource(Name: $"resources:{productsNameKebabCase}", Config: new()
            {
                RealmId = bigpodsRealm.Id.Apply(id => id),
                ResourceServerId = bigpodsMonolithAPIClient.Id.Apply(id => id),
                DisplayName = ProductsName,
                OwnerManagedAccess = true,
                Scopes = scopes.AsParallel().Select(x => x.Value.Name).ToArray(),
            }),
            [VariantsName] = new ClientAuthorizationResource(Name: $"resources:{variantsNameKebabCase}", Config: new()
            {
                RealmId = bigpodsRealm.Id.Apply(id => id),
                ResourceServerId = bigpodsMonolithAPIClient.Id.Apply(id => id),
                DisplayName = VariantsName,
                OwnerManagedAccess = true,
                Scopes = scopes.AsParallel().Select(x => x.Value.Name).ToArray(),
            }),
            [AttributesName] = new ClientAuthorizationResource(Name: $"resources:{attributesNameKebabCase}", Config: new()
            {
                RealmId = bigpodsRealm.Id.Apply(id => id),
                ResourceServerId = bigpodsMonolithAPIClient.Id.Apply(id => id),
                DisplayName = AttributesName,
                OwnerManagedAccess = true,
                Scopes = scopes.AsParallel().Select(x => x.Value.Name).ToArray(),
            }),
            [AttributeTypesName] = new ClientAuthorizationResource(Name: $"resources:{attributeTypesNameKebabCase}", Config: new()
            {
                RealmId = bigpodsRealm.Id.Apply(id => id),
                ResourceServerId = bigpodsMonolithAPIClient.Id.Apply(id => id),
                DisplayName = AttributeTypesName,
                OwnerManagedAccess = true,
                Scopes = scopes.AsParallel().Select(x => x.Value.Name).ToArray(),
            }),
        };
    }
}
