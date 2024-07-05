using System.Collections.Generic;

using Bigpods.MonolithIaC.Data.Realms;

using Bigpods.MonolithIaC.Utils;

namespace Bigpods.MonolithIaC.Data.Clients;

public record Client(string Name, Pulumi.Keycloak.OpenId.ClientArgs Config);

public static class ClientsData
{
    public static string BigpodsGUIAPIClientName { get; } = "Bigpods GUI API Client";
    public static string BigpodsMonolithAPIClientName { get; } = "Bigpods Monolith API Client";

    public static Dictionary<string, Client> GetClients(Dictionary<string, Pulumi.Keycloak.Realm> realms)
    {
        var bigpodsRealm = realms[RealmsData.BigpodsName];

        string bigpodsGUIAPIClientNameKebabCase = StringUtils.ToKebabCase(BigpodsGUIAPIClientName);
        string bigpodsMonolithAPIClientNameKebabCase = StringUtils.ToKebabCase(BigpodsMonolithAPIClientName);


        return new Dictionary<string, Client>
        {
            [BigpodsGUIAPIClientName] = new Client(Name: $"clients:{bigpodsGUIAPIClientNameKebabCase}", Config: new()
            {
                Name = BigpodsGUIAPIClientName,
                ClientId = bigpodsGUIAPIClientNameKebabCase,
                AccessType = "CONFIDENTIAL",
                RealmId = bigpodsRealm.Id.Apply(id => id),
                ClientAuthenticatorType = "client-secret",
                FrontchannelLogoutEnabled = false,
                DirectAccessGrantsEnabled = false,
                BackchannelLogoutSessionRequired = true,
                ImplicitFlowEnabled = false,
                LoginTheme = "keycloak",
                Oauth2DeviceAuthorizationGrantEnabled = false,
                PkceCodeChallengeMethod = "S256",
                ServiceAccountsEnabled = false,
                StandardFlowEnabled = true,
                UseRefreshTokens = true,
                ValidRedirectUris = new[]
                {
                    "*",
                },
                WebOrigins = new[]
                {
                    "*",
                },
            }),
            [BigpodsMonolithAPIClientName] = new Client(Name: $"clients:{bigpodsMonolithAPIClientNameKebabCase}", Config: new()
            {
                Name = BigpodsMonolithAPIClientName,
                ClientId = bigpodsMonolithAPIClientNameKebabCase,
                AccessType = "CONFIDENTIAL",
                RealmId = bigpodsRealm.Id.Apply(id => id),
                ClientAuthenticatorType = "client-secret",
                FrontchannelLogoutEnabled = false,
                DirectAccessGrantsEnabled = true,
                BackchannelLogoutSessionRequired = true,
                Authorization = new Pulumi.Keycloak.OpenId.Inputs.ClientAuthorizationArgs
                {
                    PolicyEnforcementMode = "ENFORCING",
                    AllowRemoteResourceManagement = true,
                    DecisionStrategy = "UNANIMOUS",
                },
                ImplicitFlowEnabled = false,
                LoginTheme = "keycloak",
                Oauth2DeviceAuthorizationGrantEnabled = false,
                RootUrl = "http://localhost:8082",
                WebOrigins = new[]
                {
                    "*",
                },
                StandardFlowEnabled = false,
                ServiceAccountsEnabled = true,
                PkceCodeChallengeMethod = ""
            }),
        };
    }
}