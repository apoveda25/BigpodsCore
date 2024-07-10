using System.Collections.Generic;
using System.Linq;
using Bigpods.MonolithIaC.Data.ClientAuthorizationScopes;
using Bigpods.MonolithIaC.Data.Clients;
using Bigpods.MonolithIaC.Data.Realms;
using Bigpods.MonolithIaC.Utils;
using Pulumi;

namespace Bigpods.MonolithIaC.Data.ClientAuthorizationPermissions;

public partial class ClientAuthorizationPermissionsData
{
    public static string ScopeCreateOneName { get; } = ClientAuthorizationScopesData.CreateOneName;
    public static string ScopeCreateManyName { get; } =
        ClientAuthorizationScopesData.CreateManyName;
    public static string ScopeReadOneName { get; } = ClientAuthorizationScopesData.ReadOneName;
    public static string ScopeReadManyName { get; } = ClientAuthorizationScopesData.ReadManyName;
    public static string ScopeUpdateOneName { get; } = ClientAuthorizationScopesData.UpdateOneName;
    public static string ScopeUpdateManyName { get; } =
        ClientAuthorizationScopesData.UpdateManyName;
    public static string ScopeDeleteOneName { get; } = ClientAuthorizationScopesData.DeleteOneName;
    public static string ScopeDeleteManyName { get; } =
        ClientAuthorizationScopesData.DeleteManyName;
    public static string ScopeAttachOneName { get; } = ClientAuthorizationScopesData.AttachOneName;
    public static string ScopeAttachManyName { get; } =
        ClientAuthorizationScopesData.AttachManyName;
    public static string ScopeDettachOneName { get; } =
        ClientAuthorizationScopesData.DettachOneName;
    public static string ScopeDettachManyName { get; } =
        ClientAuthorizationScopesData.DettachManyName;

    private static Dictionary<
        string,
        ClientAuthorizationPermission
    > GetClientAuthorizationPermissionsTypeScope(
        Dictionary<string, Pulumi.Keycloak.Realm> realms,
        Dictionary<string, Pulumi.Keycloak.OpenId.Client> clients,
        Dictionary<string, Pulumi.Keycloak.OpenId.ClientRolePolicy> policies,
        Dictionary<string, Pulumi.Keycloak.OpenId.ClientAuthorizationScope> scopes
    )
    {
        var bigpodsRealm = realms[RealmsData.BigpodsName];
        var bigpodsMonolithAPIClient = clients[ClientsData.BigpodsMonolithAPIClientName];

        var createOnePolicies = policies
            .Where(policy => policy.Key.Contains(ClientAuthorizationScopesData.CreateOneName))
            .Select(policy => policy.Value.Name);
        var createManyPolicies = policies
            .Where(policy => policy.Key.Contains(ClientAuthorizationScopesData.CreateManyName))
            .Select(policy => policy.Value.Name);
        var readOnePolicies = policies
            .Where(policy => policy.Key.Contains(ClientAuthorizationScopesData.ReadOneName))
            .Select(policy => policy.Value.Name);
        var readManyPolicies = policies
            .Where(policy => policy.Key.Contains(ClientAuthorizationScopesData.ReadManyName))
            .Select(policy => policy.Value.Name);
        var updateOnePolicies = policies
            .Where(policy => policy.Key.Contains(ClientAuthorizationScopesData.UpdateOneName))
            .Select(policy => policy.Value.Name);
        var updateManyPolicies = policies
            .Where(policy => policy.Key.Contains(ClientAuthorizationScopesData.UpdateManyName))
            .Select(policy => policy.Value.Name);
        var deleteOnePolicies = policies
            .Where(policy => policy.Key.Contains(ClientAuthorizationScopesData.DeleteOneName))
            .Select(policy => policy.Value.Name);
        var deleteManyPolicies = policies
            .Where(policy => policy.Key.Contains(ClientAuthorizationScopesData.DeleteManyName))
            .Select(policy => policy.Value.Name);
        var attachOnePolicies = policies
            .Where(policy => policy.Key.Contains(ClientAuthorizationScopesData.AttachOneName))
            .Select(policy => policy.Value.Name);
        var attachManyPolicies = policies
            .Where(policy => policy.Key.Contains(ClientAuthorizationScopesData.AttachManyName))
            .Select(policy => policy.Value.Name);
        var dettachOnePolicies = policies
            .Where(policy => policy.Key.Contains(ClientAuthorizationScopesData.DettachOneName))
            .Select(policy => policy.Value.Name);
        var dettachManyPolicies = policies
            .Where(policy => policy.Key.Contains(ClientAuthorizationScopesData.DettachManyName))
            .Select(policy => policy.Value.Name);

        string scopeCreateOneNameKebabCase = StringUtils.ToKebabCase(ScopeCreateOneName);
        string scopeCreateManyNameKebabCase = StringUtils.ToKebabCase(ScopeCreateManyName);
        string scopeReadOneNameKebabCase = StringUtils.ToKebabCase(ScopeReadOneName);
        string scopeReadManyNameKebabCase = StringUtils.ToKebabCase(ScopeReadManyName);
        string scopeUpdateOneNameKebabCase = StringUtils.ToKebabCase(ScopeUpdateOneName);
        string scopeUpdateManyNameKebabCase = StringUtils.ToKebabCase(ScopeUpdateManyName);
        string scopeDeleteOneNameKebabCase = StringUtils.ToKebabCase(ScopeDeleteOneName);
        string scopeDeleteManyNameKebabCase = StringUtils.ToKebabCase(ScopeDeleteManyName);
        string scopeAttachOneNameKebabCase = StringUtils.ToKebabCase(ScopeAttachOneName);
        string scopeAttachManyNameKebabCase = StringUtils.ToKebabCase(ScopeAttachManyName);
        string scopeDettachOneNameKebabCase = StringUtils.ToKebabCase(ScopeDettachOneName);
        string scopeDettachManyNameKebabCase = StringUtils.ToKebabCase(ScopeDettachManyName);

        return new Dictionary<string, ClientAuthorizationPermission>
        {
            [ScopeCreateOneName] = new ClientAuthorizationPermission(
                Name: $"permissions:{scopeCreateOneNameKebabCase}",
                Config: new()
                {
                    RealmId = bigpodsRealm.Id.Apply(id => id),
                    ResourceServerId = bigpodsMonolithAPIClient.Id.Apply(id => id),
                    DecisionStrategy = "UNANIMOUS",
                    Description = $"{ScopeCreateOneName} Permission",
                    Policies = Output.All(createOnePolicies),
                    Scopes = scopes[ClientAuthorizationScopesData.CreateOneName]
                        .Name.Apply(name => new[] { name }),
                    Type = "scope"
                }
            ),
            [ScopeCreateManyName] = new ClientAuthorizationPermission(
                Name: $"permissions:{scopeCreateManyNameKebabCase}",
                Config: new()
                {
                    RealmId = bigpodsRealm.Id.Apply(id => id),
                    ResourceServerId = bigpodsMonolithAPIClient.Id.Apply(id => id),
                    DecisionStrategy = "UNANIMOUS",
                    Description = $"{ScopeCreateManyName} Permission",
                    Policies = Output.All(createManyPolicies),
                    Scopes = scopes[ClientAuthorizationScopesData.CreateManyName]
                        .Name.Apply(name => new[] { name }),
                    Type = "scope"
                }
            ),
            [ScopeReadOneName] = new ClientAuthorizationPermission(
                Name: $"permissions:{scopeReadOneNameKebabCase}",
                Config: new()
                {
                    RealmId = bigpodsRealm.Id.Apply(id => id),
                    ResourceServerId = bigpodsMonolithAPIClient.Id.Apply(id => id),
                    DecisionStrategy = "UNANIMOUS",
                    Description = $"{ScopeReadOneName} Permission",
                    Policies = Output.All(readOnePolicies),
                    Scopes = scopes[ClientAuthorizationScopesData.ReadOneName]
                        .Name.Apply(name => new[] { name }),
                    Type = "scope"
                }
            ),
            [ScopeReadManyName] = new ClientAuthorizationPermission(
                Name: $"permissions:{scopeReadManyNameKebabCase}",
                Config: new()
                {
                    RealmId = bigpodsRealm.Id.Apply(id => id),
                    ResourceServerId = bigpodsMonolithAPIClient.Id.Apply(id => id),
                    DecisionStrategy = "UNANIMOUS",
                    Description = $"{ScopeReadManyName} Permission",
                    Policies = Output.All(readManyPolicies),
                    Scopes = scopes[ClientAuthorizationScopesData.ReadManyName]
                        .Name.Apply(name => new[] { name }),
                    Type = "scope"
                }
            ),
            [ScopeUpdateOneName] = new ClientAuthorizationPermission(
                Name: $"permissions:{scopeUpdateOneNameKebabCase}",
                Config: new()
                {
                    RealmId = bigpodsRealm.Id.Apply(id => id),
                    ResourceServerId = bigpodsMonolithAPIClient.Id.Apply(id => id),
                    DecisionStrategy = "UNANIMOUS",
                    Description = $"{ScopeUpdateOneName} Permission",
                    Policies = Output.All(updateOnePolicies),
                    Scopes = scopes[ClientAuthorizationScopesData.UpdateOneName]
                        .Name.Apply(name => new[] { name }),
                    Type = "scope"
                }
            ),
            [ScopeUpdateManyName] = new ClientAuthorizationPermission(
                Name: $"permissions:{scopeUpdateManyNameKebabCase}",
                Config: new()
                {
                    RealmId = bigpodsRealm.Id.Apply(id => id),
                    ResourceServerId = bigpodsMonolithAPIClient.Id.Apply(id => id),
                    DecisionStrategy = "UNANIMOUS",
                    Description = $"{ScopeUpdateManyName} Permission",
                    Policies = Output.All(updateManyPolicies),
                    Scopes = scopes[ClientAuthorizationScopesData.UpdateManyName]
                        .Name.Apply(name => new[] { name }),
                    Type = "scope"
                }
            ),
            [ScopeDeleteOneName] = new ClientAuthorizationPermission(
                Name: $"permissions:{scopeDeleteOneNameKebabCase}",
                Config: new()
                {
                    RealmId = bigpodsRealm.Id.Apply(id => id),
                    ResourceServerId = bigpodsMonolithAPIClient.Id.Apply(id => id),
                    DecisionStrategy = "UNANIMOUS",
                    Description = $"{ScopeDeleteOneName} Permission",
                    Policies = Output.All(deleteOnePolicies),
                    Scopes = scopes[ClientAuthorizationScopesData.DeleteOneName]
                        .Name.Apply(name => new[] { name }),
                    Type = "scope"
                }
            ),
            [ScopeDeleteManyName] = new ClientAuthorizationPermission(
                Name: $"permissions:{scopeDeleteManyNameKebabCase}",
                Config: new()
                {
                    RealmId = bigpodsRealm.Id.Apply(id => id),
                    ResourceServerId = bigpodsMonolithAPIClient.Id.Apply(id => id),
                    DecisionStrategy = "UNANIMOUS",
                    Description = $"{ScopeDeleteManyName} Permission",
                    Policies = Output.All(deleteManyPolicies),
                    Scopes = scopes[ClientAuthorizationScopesData.DeleteManyName]
                        .Name.Apply(name => new[] { name }),
                    Type = "scope"
                }
            ),
            [ScopeAttachOneName] = new ClientAuthorizationPermission(
                Name: $"permissions:{scopeAttachOneNameKebabCase}",
                Config: new()
                {
                    RealmId = bigpodsRealm.Id.Apply(id => id),
                    ResourceServerId = bigpodsMonolithAPIClient.Id.Apply(id => id),
                    DecisionStrategy = "UNANIMOUS",
                    Description = $"{ScopeAttachOneName} Permission",
                    Policies = Output.All(attachOnePolicies),
                    Scopes = scopes[ClientAuthorizationScopesData.AttachOneName]
                        .Name.Apply(name => new[] { name }),
                    Type = "scope"
                }
            ),
            [ScopeAttachManyName] = new ClientAuthorizationPermission(
                Name: $"permissions:{scopeAttachManyNameKebabCase}",
                Config: new()
                {
                    RealmId = bigpodsRealm.Id.Apply(id => id),
                    ResourceServerId = bigpodsMonolithAPIClient.Id.Apply(id => id),
                    DecisionStrategy = "UNANIMOUS",
                    Description = $"{ScopeAttachManyName} Permission",
                    Policies = Output.All(attachManyPolicies),
                    Scopes = scopes[ClientAuthorizationScopesData.AttachManyName]
                        .Name.Apply(name => new[] { name }),
                    Type = "scope"
                }
            ),
            [ScopeDettachOneName] = new ClientAuthorizationPermission(
                Name: $"permissions:{scopeDettachOneNameKebabCase}",
                Config: new()
                {
                    RealmId = bigpodsRealm.Id.Apply(id => id),
                    ResourceServerId = bigpodsMonolithAPIClient.Id.Apply(id => id),
                    DecisionStrategy = "UNANIMOUS",
                    Description = $"{ScopeDettachOneName} Permission",
                    Policies = Output.All(dettachOnePolicies),
                    Scopes = scopes[ClientAuthorizationScopesData.DettachOneName]
                        .Name.Apply(name => new[] { name }),
                    Type = "scope"
                }
            ),
            [ScopeDettachManyName] = new ClientAuthorizationPermission(
                Name: $"permissions:{scopeDettachManyNameKebabCase}",
                Config: new()
                {
                    RealmId = bigpodsRealm.Id.Apply(id => id),
                    ResourceServerId = bigpodsMonolithAPIClient.Id.Apply(id => id),
                    DecisionStrategy = "UNANIMOUS",
                    Description = $"{ScopeDettachManyName} Permission",
                    Policies = Output.All(dettachManyPolicies),
                    Scopes = scopes[ClientAuthorizationScopesData.DettachManyName]
                        .Name.Apply(name => new[] { name }),
                    Type = "scope"
                }
            ),
        };
    }
}
