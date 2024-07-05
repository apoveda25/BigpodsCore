using System.Collections.Generic;

namespace Bigpods.MonolithIaC.Factories;

public class ClientFactory
{
    public static Pulumi.Keycloak.OpenId.Client Build(string name, Pulumi.Keycloak.OpenId.ClientArgs args)
    {
        return new Pulumi.Keycloak.OpenId.Client(name, new()
        {
            ClientId = args.ClientId,
            AccessType = args.AccessType,
            RealmId = args.RealmId,
            ExcludeSessionStateFromAuthResponse = args?.ExcludeSessionStateFromAuthResponse,
            // AuthenticationFlowBindingOverrides = new Pulumi.Keycloak.OpenId.Inputs.ClientAuthenticationFlowBindingOverridesArgs
            // {
            //     BrowserId = "string",
            //     DirectGrantId = "string",
            // },
            BackchannelLogoutRevokeOfflineSessions = args?.BackchannelLogoutRevokeOfflineSessions,
            // ExtraConfig =
            // {
            //     { "string", "any" },
            // },
            BackchannelLogoutUrl = args?.BackchannelLogoutUrl,
            FrontchannelLogoutUrl = args?.FrontchannelLogoutUrl,
            ClientAuthenticatorType = args?.ClientAuthenticatorType,
            FrontchannelLogoutEnabled = args?.FrontchannelLogoutEnabled,
            ClientOfflineSessionIdleTimeout = args?.ClientOfflineSessionIdleTimeout,
            ClientOfflineSessionMaxLifespan = args?.ClientOfflineSessionMaxLifespan,
            ClientSecret = args?.ClientSecret,
            ClientSessionIdleTimeout = args?.ClientSessionIdleTimeout,
            ClientSessionMaxLifespan = args?.ClientSessionMaxLifespan,
            ConsentRequired = args?.ConsentRequired,
            ConsentScreenText = args?.ConsentScreenText,
            Description = args?.Description,
            DirectAccessGrantsEnabled = args?.DirectAccessGrantsEnabled,
            DisplayOnConsentScreen = args?.DisplayOnConsentScreen,
            Enabled = args?.Enabled,
            AccessTokenLifespan = args?.AccessTokenLifespan,
            BackchannelLogoutSessionRequired = args?.BackchannelLogoutSessionRequired,
            Authorization = args?.Authorization,
            BaseUrl = args?.BaseUrl,
            FullScopeAllowed = args?.FullScopeAllowed,
            ImplicitFlowEnabled = args?.ImplicitFlowEnabled,
            Import = args?.Import,
            LoginTheme = args?.LoginTheme,
            Name = args?.Name ?? name,
            Oauth2DeviceAuthorizationGrantEnabled = args?.Oauth2DeviceAuthorizationGrantEnabled,
            Oauth2DeviceCodeLifespan = args?.Oauth2DeviceCodeLifespan,
            Oauth2DevicePollingInterval = args?.Oauth2DevicePollingInterval,
            PkceCodeChallengeMethod = args?.PkceCodeChallengeMethod,
            AdminUrl = args?.AdminUrl,
            RootUrl = args?.RootUrl,
            ServiceAccountsEnabled = args?.ServiceAccountsEnabled,
            StandardFlowEnabled = args?.StandardFlowEnabled,
            UseRefreshTokens = args?.UseRefreshTokens,
            UseRefreshTokensClientCredentials = args?.UseRefreshTokensClientCredentials,
            ValidPostLogoutRedirectUris = args?.ValidPostLogoutRedirectUris ?? new List<string>(),
            ValidRedirectUris = args?.ValidRedirectUris ?? new List<string>(),
            WebOrigins = args?.WebOrigins ?? new List<string>(),
        });
    }
}
