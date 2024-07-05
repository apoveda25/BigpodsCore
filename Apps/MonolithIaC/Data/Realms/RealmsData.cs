using System.Collections.Generic;

using Bigpods.MonolithIaC.Utils;

namespace Bigpods.MonolithIaC.Data.Realms;

public sealed record Realm(string Name, Pulumi.Keycloak.RealmArgs Config);

public sealed record RealmsData
{
    public static string BigpodsName { get; } = "Bigpods";

    public static Dictionary<string, Realm> GetRealms()
    {
        string bigpodsNameKebabCase = StringUtils.ToKebabCase(BigpodsName);

        return new()
        {
            [BigpodsName] = new Realm(Name: $"realms:{bigpodsNameKebabCase}", Config: new()
            {
                RealmName = bigpodsNameKebabCase,
                AccountTheme = "base",
                DisplayName = $"{BigpodsName} Realm",
                DisplayNameHtml = $"{BigpodsName} Realm",
                DuplicateEmailsAllowed = false,
                EditUsernameAllowed = false,
                Enabled = true,
                Internationalization = new Pulumi.Keycloak.Inputs.RealmInternationalizationArgs
                {
                    DefaultLocale = "es",
                    SupportedLocales = new[]
                    {
                        "es",
                        "en",
                    },
                },
                LoginTheme = "base",
                LoginWithEmailAllowed = true,
                PasswordPolicy = "upperCase(1) and length(8) and forceExpiredPasswordChange(365) and notUsername",
                RegistrationAllowed = false,
                RememberMe = true,
                ResetPasswordAllowed = true,
                SecurityDefenses = new Pulumi.Keycloak.Inputs.RealmSecurityDefensesArgs
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
                VerifyEmail = true,
            })
        };
    }
}
