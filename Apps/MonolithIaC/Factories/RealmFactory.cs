namespace Bigpods.MonolithIaC.Factories;

public class RealmFactory
{
    public static Pulumi.Keycloak.Realm Build(string name, Pulumi.Keycloak.RealmArgs? args)
    {
        return new Pulumi.Keycloak.Realm(name, new()
        {
            RealmName = args?.RealmName ?? name,
            AccessCodeLifespan = args?.AccessCodeLifespan,
            AccessCodeLifespanLogin = args?.AccessCodeLifespanLogin,
            AccessCodeLifespanUserAction = args?.AccessCodeLifespanUserAction,
            AccessTokenLifespan = args?.AccessTokenLifespan,
            AccessTokenLifespanForImplicitFlow = args?.AccessTokenLifespanForImplicitFlow,
            AccountTheme = args?.AccountTheme,
            ActionTokenGeneratedByAdminLifespan = args?.ActionTokenGeneratedByAdminLifespan,
            ActionTokenGeneratedByUserLifespan = args?.ActionTokenGeneratedByUserLifespan,
            AdminTheme = args?.AdminTheme,
            // Attributes = 
            // {
            //     { "string", "any" },
            // },
            BrowserFlow = args?.BrowserFlow,
            ClientAuthenticationFlow = args?.ClientAuthenticationFlow,
            ClientSessionIdleTimeout = args?.ClientSessionIdleTimeout,
            ClientSessionMaxLifespan = args?.ClientSessionMaxLifespan,
            // DefaultDefaultClientScopes = new[]
            // {
            //     "string",
            // },
            // DefaultOptionalClientScopes = new[]
            // {
            //     "string",
            // },
            DefaultSignatureAlgorithm = args?.DefaultSignatureAlgorithm,
            DirectGrantFlow = args?.DirectGrantFlow,
            DisplayName = args?.DisplayName,
            DisplayNameHtml = args?.DisplayNameHtml,
            DockerAuthenticationFlow = args?.DockerAuthenticationFlow,
            DuplicateEmailsAllowed = args?.DuplicateEmailsAllowed,
            EditUsernameAllowed = args?.EditUsernameAllowed,
            EmailTheme = args?.EmailTheme,
            Enabled = args?.Enabled,
            InternalId = args?.InternalId,
            Internationalization = args?.Internationalization,
            LoginTheme = args?.LoginTheme,
            LoginWithEmailAllowed = args?.LoginWithEmailAllowed,
            Oauth2DeviceCodeLifespan = args?.Oauth2DeviceCodeLifespan,
            Oauth2DevicePollingInterval = args?.Oauth2DevicePollingInterval,
            OfflineSessionIdleTimeout = args?.OfflineSessionIdleTimeout,
            OfflineSessionMaxLifespan = args?.OfflineSessionMaxLifespan,
            OfflineSessionMaxLifespanEnabled = args?.OfflineSessionMaxLifespanEnabled,
            // OtpPolicy = new Pulumi.Keycloak.Inputs.RealmOtpPolicyArgs
            // {
            //     Algorithm = "string",
            //     Digits = 0,
            //     InitialCounter = 0,
            //     LookAheadWindow = 0,
            //     Period = 0,
            //     Type = "string",
            // },
            PasswordPolicy = args?.PasswordPolicy,
            RefreshTokenMaxReuse = args?.RefreshTokenMaxReuse,
            RegistrationAllowed = args?.RegistrationAllowed,
            RegistrationEmailAsUsername = args?.RegistrationEmailAsUsername,
            RegistrationFlow = args?.RegistrationFlow,
            RememberMe = args?.RememberMe ?? true,
            ResetCredentialsFlow = args?.ResetCredentialsFlow,
            ResetPasswordAllowed = args?.ResetPasswordAllowed,
            RevokeRefreshToken = args?.RevokeRefreshToken,
            SecurityDefenses = args?.SecurityDefenses,
            // SmtpServer = new Pulumi.Keycloak.Inputs.RealmSmtpServerArgs
            // {
            //     From = "string",
            //     Host = "string",
            //     Auth = new Pulumi.Keycloak.Inputs.RealmSmtpServerAuthArgs
            //     {
            //         Password = "string",
            //         Username = "string",
            //     },
            //     EnvelopeFrom = "string",
            //     FromDisplayName = "string",
            //     Port = "string",
            //     ReplyTo = "string",
            //     ReplyToDisplayName = "string",
            //     Ssl = false,
            //     Starttls = false,
            // },
            SslRequired = args?.SslRequired,
            SsoSessionIdleTimeout = args?.SsoSessionIdleTimeout,
            SsoSessionIdleTimeoutRememberMe = args?.SsoSessionIdleTimeoutRememberMe,
            SsoSessionMaxLifespan = args?.SsoSessionMaxLifespan,
            SsoSessionMaxLifespanRememberMe = args?.SsoSessionMaxLifespanRememberMe,
            UserManagedAccess = args?.UserManagedAccess,
            VerifyEmail = args?.VerifyEmail,
            // WebAuthnPasswordlessPolicy = new Pulumi.Keycloak.Inputs.RealmWebAuthnPasswordlessPolicyArgs
            // {
            //     AcceptableAaguids = new[]
            //     {
            //         "string",
            //     },
            //     AttestationConveyancePreference = "string",
            //     AuthenticatorAttachment = "string",
            //     AvoidSameAuthenticatorRegister = false,
            //     CreateTimeout = 0,
            //     RelyingPartyEntityName = "string",
            //     RelyingPartyId = "string",
            //     RequireResidentKey = "string",
            //     SignatureAlgorithms = new[]
            //     {
            //         "string",
            //     },
            //     UserVerificationRequirement = "string",
            // },
            // WebAuthnPolicy = new Pulumi.Keycloak.Inputs.RealmWebAuthnPolicyArgs
            // {
            //     AcceptableAaguids = new[]
            //     {
            //         "string",
            //     },
            //     AttestationConveyancePreference = "string",
            //     AuthenticatorAttachment = "string",
            //     AvoidSameAuthenticatorRegister = false,
            //     CreateTimeout = 0,
            //     RelyingPartyEntityName = "string",
            //     RelyingPartyId = "string",
            //     RequireResidentKey = "string",
            //     SignatureAlgorithms = new[]
            //     {
            //         "string",
            //     },
            //     UserVerificationRequirement = "string",
            // },
        });
    }
}
