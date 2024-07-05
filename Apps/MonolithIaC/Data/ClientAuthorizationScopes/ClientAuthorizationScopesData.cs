using System.Collections.Generic;

using Bigpods.MonolithIaC.Data.Clients;

using Bigpods.MonolithIaC.Data.Realms;

using Bigpods.MonolithIaC.Utils;

namespace Bigpods.MonolithIaC.Data.ClientAuthorizationScopes;

public sealed record ClientAuthorizationScope(string Name, Pulumi.Keycloak.OpenId.ClientAuthorizationScopeArgs Config);

public static class ClientAuthorizationScopesData
{
    public static string CreateOneName { get; } = "Create one";
    public static string CreateManyName { get; } = "Create many";
    public static string ReadManyName { get; } = "Read many";
    public static string ReadOneName { get; } = "Read one";
    public static string UpdateOneName { get; } = "Update one";
    public static string UpdateManyName { get; } = "Update many";
    public static string DeleteOneName { get; } = "Delete one";
    public static string DeleteManyName { get; } = "Delete many";

    public static Dictionary<string, ClientAuthorizationScope> GetClientAuthorizationScopes(
        Dictionary<string, Pulumi.Keycloak.Realm> realms,
        Dictionary<string, Pulumi.Keycloak.OpenId.Client> clients
    )
    {
        var bigpodsRealm = realms[RealmsData.BigpodsName];
        var bigpodsMonolithAPIClient = clients[ClientsData.BigpodsMonolithAPIClientName];

        string createOneNameKebabCase = StringUtils.ToKebabCase(CreateOneName);
        string createManyNameKebabCase = StringUtils.ToKebabCase(CreateManyName);
        string readManyNameKebabCase = StringUtils.ToKebabCase(ReadManyName);
        string readOneNameKebabCase = StringUtils.ToKebabCase(ReadOneName);
        string updateOneNameKebabCase = StringUtils.ToKebabCase(UpdateOneName);
        string updateManyNameKebabCase = StringUtils.ToKebabCase(UpdateManyName);
        string deleteOneNameKebabCase = StringUtils.ToKebabCase(DeleteOneName);
        string deleteManyNameKebabCase = StringUtils.ToKebabCase(DeleteManyName);

        return new Dictionary<string, ClientAuthorizationScope>()
        {
            [CreateOneName] = new ClientAuthorizationScope(
                Name: $"scopes:{createOneNameKebabCase}",
                Config: new Pulumi.Keycloak.OpenId.ClientAuthorizationScopeArgs
                {
                    RealmId = bigpodsRealm.Id.Apply(id => id),
                    ResourceServerId = bigpodsMonolithAPIClient.Id.Apply(id => id),
                    DisplayName = CreateOneName,
                }
            ),
            [CreateManyName] = new ClientAuthorizationScope(
                Name: $"scopes:{createManyNameKebabCase}",
                Config: new Pulumi.Keycloak.OpenId.ClientAuthorizationScopeArgs
                {
                    RealmId = bigpodsRealm.Id.Apply(id => id),
                    ResourceServerId = bigpodsMonolithAPIClient.Id.Apply(id => id),
                    DisplayName = CreateManyName,
                }
            ),
            [ReadOneName] = new ClientAuthorizationScope(
                Name: $"scopes:{readOneNameKebabCase}",
                Config: new Pulumi.Keycloak.OpenId.ClientAuthorizationScopeArgs
                {
                    RealmId = bigpodsRealm.Id.Apply(id => id),
                    ResourceServerId = bigpodsMonolithAPIClient.Id.Apply(id => id),
                    DisplayName = ReadOneName,
                }
            ),
            [ReadManyName] = new ClientAuthorizationScope(
                Name: $"scopes:{readManyNameKebabCase}",
                Config: new Pulumi.Keycloak.OpenId.ClientAuthorizationScopeArgs
                {
                    RealmId = bigpodsRealm.Id.Apply(id => id),
                    ResourceServerId = bigpodsMonolithAPIClient.Id.Apply(id => id),
                    DisplayName = ReadManyName,
                }
            ),
            [UpdateOneName] = new ClientAuthorizationScope(
                Name: $"scopes:{updateOneNameKebabCase}",
                Config: new Pulumi.Keycloak.OpenId.ClientAuthorizationScopeArgs
                {
                    RealmId = bigpodsRealm.Id.Apply(id => id),
                    ResourceServerId = bigpodsMonolithAPIClient.Id.Apply(id => id),
                    DisplayName = UpdateOneName,
                }
            ),
            [UpdateManyName] = new ClientAuthorizationScope(
                Name: $"scopes:{updateManyNameKebabCase}",
                Config: new Pulumi.Keycloak.OpenId.ClientAuthorizationScopeArgs
                {
                    RealmId = bigpodsRealm.Id.Apply(id => id),
                    ResourceServerId = bigpodsMonolithAPIClient.Id.Apply(id => id),
                    DisplayName = UpdateManyName,
                }
            ),
            [DeleteOneName] = new ClientAuthorizationScope(
                Name: $"scopes:{deleteOneNameKebabCase}",
                Config: new Pulumi.Keycloak.OpenId.ClientAuthorizationScopeArgs
                {
                    RealmId = bigpodsRealm.Id.Apply(id => id),
                    ResourceServerId = bigpodsMonolithAPIClient.Id.Apply(id => id),
                    DisplayName = DeleteOneName,
                }
            ),
            [DeleteManyName] = new ClientAuthorizationScope(
                Name: $"scopes:{deleteManyNameKebabCase}",
                Config: new Pulumi.Keycloak.OpenId.ClientAuthorizationScopeArgs
                {
                    RealmId = bigpodsRealm.Id.Apply(id => id),
                    ResourceServerId = bigpodsMonolithAPIClient.Id.Apply(id => id),
                    DisplayName = DeleteManyName,
                }
            ),
        };
    }
}
