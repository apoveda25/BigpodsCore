namespace Bigpods.MonolithIaC.Factories;

public class RealmFactory
{
    public static Pulumi.Keycloak.Realm Build(string name, Pulumi.Keycloak.RealmArgs? args)
    {
        return new Pulumi.Keycloak.Realm(name.ToLower(), new()
        {
            RealmName = name.ToLower(),
            // AccessCodeLifespan = "string",
            // AccessCodeLifespanLogin = "string",
            // AccessCodeLifespanUserAction = "string",
            // AccessTokenLifespan = "string",
            // AccessTokenLifespanForImplicitFlow = "string",
            AccountTheme = args?.AccountTheme ?? "base",
            // ActionTokenGeneratedByAdminLifespan = "string",
            // ActionTokenGeneratedByUserLifespan = "string",
            // AdminTheme = "string",
            // Attributes = 
            // {
            //     { "string", "any" },
            // },
            // BrowserFlow = "string",
            // ClientAuthenticationFlow = "string",
            // ClientSessionIdleTimeout = "string",
            // ClientSessionMaxLifespan = "string",
            // DefaultDefaultClientScopes = new[]
            // {
            //     "string",
            // },
            // DefaultOptionalClientScopes = new[]
            // {
            //     "string",
            // },
            // DefaultSignatureAlgorithm = "string",
            // DirectGrantFlow = "string",
            DisplayName = $"{name} Realm",
            DisplayNameHtml = $"{name} Realm",
            // DockerAuthenticationFlow = "string",
            DuplicateEmailsAllowed = args?.DuplicateEmailsAllowed ?? false,
            EditUsernameAllowed = args?.EditUsernameAllowed ?? false,
            // EmailTheme = "string",
            Enabled = args?.Enabled ?? true,
            // InternalId = "string",
            Internationalization = args?.Internationalization ?? new Pulumi.Keycloak.Inputs.RealmInternationalizationArgs
            {
                DefaultLocale = "es",
                SupportedLocales = new[]
                {
                    "es",
                    "en",
                },
            },
            LoginTheme = args?.LoginTheme ?? "base",
            LoginWithEmailAllowed = args?.LoginWithEmailAllowed ?? true,
            // Oauth2DeviceCodeLifespan = "string",
            // Oauth2DevicePollingInterval = 0,
            // OfflineSessionIdleTimeout = "string",
            // OfflineSessionMaxLifespan = "string",
            // OfflineSessionMaxLifespanEnabled = false,
            // OtpPolicy = new Pulumi.Keycloak.Inputs.RealmOtpPolicyArgs
            // {
            //     Algorithm = "string",
            //     Digits = 0,
            //     InitialCounter = 0,
            //     LookAheadWindow = 0,
            //     Period = 0,
            //     Type = "string",
            // },
            PasswordPolicy = args?.PasswordPolicy ?? "upperCase(1) and length(8) and forceExpiredPasswordChange(365) and notUsername",
            // RefreshTokenMaxReuse = 0,
            RegistrationAllowed = args?.RegistrationAllowed ?? true,
            // RegistrationEmailAsUsername = false,
            // RegistrationFlow = "string",
            RememberMe = args?.RememberMe ?? true,
            // ResetCredentialsFlow = "string",
            ResetPasswordAllowed = args?.ResetPasswordAllowed ?? true,
            // RevokeRefreshToken = false,
            SecurityDefenses = args?.SecurityDefenses ?? new Pulumi.Keycloak.Inputs.RealmSecurityDefensesArgs
            {
                BruteForceDetection = new Pulumi.Keycloak.Inputs.RealmSecurityDefensesBruteForceDetectionArgs
                {
                    FailureResetTimeSeconds = 43200,
                    MaxFailureWaitSeconds = 900,
                    MaxLoginFailures = 30,
                    MinimumQuickLoginWaitSeconds = 60,
                    PermanentLockout = false,
                    QuickLoginCheckMilliSeconds = 1000,
                    WaitIncrementSeconds = 60,
                },
                Headers = new Pulumi.Keycloak.Inputs.RealmSecurityDefensesHeadersArgs
                {
                    ContentSecurityPolicy = "frame-src 'self'; frame-ancestors 'self'; object-src 'none';",
                    ContentSecurityPolicyReportOnly = "",
                    StrictTransportSecurity = "max-age=31536000; includeSubDomains",
                    XContentTypeOptions = "nosniff",
                    XFrameOptions = "DENY",
                    XRobotsTag = "none",
                    XXssProtection = "1; mode=block",
                },
            },
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
            // SslRequired = "string",
            // SsoSessionIdleTimeout = "string",
            // SsoSessionIdleTimeoutRememberMe = "string",
            // SsoSessionMaxLifespan = "string",
            // SsoSessionMaxLifespanRememberMe = "string",
            // UserManagedAccess = false,
            VerifyEmail = args?.VerifyEmail ?? true,
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
