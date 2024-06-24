using System;
using System.Collections.Generic;

namespace Bigpods.MonolithIaC.Factories;

public class ClientFactory
{
    public static Pulumi.Keycloak.OpenId.Client Build(string name, Pulumi.Keycloak.OpenId.ClientArgs args)
    {
        return new Pulumi.Keycloak.OpenId.Client(name, new()
        {
            ClientId = args.ClientId ?? name.Replace(" ", "-").ToLower(),
            AccessType = args.AccessType ?? "CONFIDENTIAL",
            RealmId = args.RealmId,
            // ExcludeSessionStateFromAuthResponse = false,
            // AuthenticationFlowBindingOverrides = new Pulumi.Keycloak.OpenId.Inputs.ClientAuthenticationFlowBindingOverridesArgs
            // {
            //     BrowserId = "string",
            //     DirectGrantId = "string",
            // },
            // BackchannelLogoutRevokeOfflineSessions = false,
            // ExtraConfig =
            // {
            //     { "string", "any" },
            // },
            // BackchannelLogoutUrl = "string",
            // FrontchannelLogoutUrl = "string",
            ClientAuthenticatorType = args.ClientAuthenticatorType ?? "client-secret",
            FrontchannelLogoutEnabled = args.FrontchannelLogoutEnabled ?? false,
            // ClientOfflineSessionIdleTimeout = "string",
            // ClientOfflineSessionMaxLifespan = "string",
            // ClientSecret = "string",
            // ClientSessionIdleTimeout = "string",
            // ClientSessionMaxLifespan = "string",
            // ConsentRequired = false,
            // ConsentScreenText = "string",
            // Description = "string",
            DirectAccessGrantsEnabled = args.DirectAccessGrantsEnabled ?? false,
            // DisplayOnConsentScreen = false,
            Enabled = args.Enabled ?? true,
            // AccessTokenLifespan = "string",
            BackchannelLogoutSessionRequired = args.BackchannelLogoutSessionRequired ?? false,
            Authorization = args.Authorization ?? null,
            BaseUrl = args.BaseUrl,
            // FullScopeAllowed = false,
            ImplicitFlowEnabled = args.ImplicitFlowEnabled ?? false,
            // Import = false,
            LoginTheme = args.LoginTheme ?? "keycloak",
            Name = args.Name ?? name.Replace(" ", "-").ToLower(),
            Oauth2DeviceAuthorizationGrantEnabled = args.Oauth2DeviceAuthorizationGrantEnabled ?? false,
            // Oauth2DeviceCodeLifespan = "string",
            // Oauth2DevicePollingInterval = "string",
            PkceCodeChallengeMethod = args.PkceCodeChallengeMethod,
            // AdminUrl = "string",
            RootUrl = args.RootUrl,
            ServiceAccountsEnabled = args.ServiceAccountsEnabled ?? false,
            StandardFlowEnabled = args.StandardFlowEnabled ?? false,
            UseRefreshTokens = args.UseRefreshTokens ?? false,
            // UseRefreshTokensClientCredentials = false,
            ValidPostLogoutRedirectUris = args.ValidPostLogoutRedirectUris ?? new List<string>(),
            ValidRedirectUris = args.ValidRedirectUris ?? new List<string>(),
            WebOrigins = args.WebOrigins ?? new List<string>(),
        });
    }
}
