using System;
using System.Collections.Generic;
using System.Linq;

using Bigpods.MonolithIaC.Factories;
using Bigpods.MonolithIaC.Permissions;
using Bigpods.MonolithIaC.Policies;
using Bigpods.MonolithIaC.Resources;
using Bigpods.MonolithIaC.Roles;
using Bigpods.MonolithIaC.Scopes;
using Bigpods.MonolithIaC.Utils;

using Pulumi;

return await Deployment.RunAsync(() =>
{
    // Create a Keycloak realm
    string realmName = "Bigpods";

    var realm = RealmFactory.Build(realmName, new()
    {
        RealmName = realmName.ToLower(),
        AccountTheme = "base",
        DisplayName = $"{realmName} Realm",
        DisplayNameHtml = $"{realmName} Realm",
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
        RegistrationAllowed = true,
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
    });

    // Create a Keycloak clients
    string guiAPIClientName = "Bigpods GUI API Client";
    string monolithAPIClientName = "Bigpods Monolith API Client";

    string baseUrlKeycloakServer = "http://localhost:8082";

    var clientsWithoutResources = new[] {
        ClientFactory.Build(guiAPIClientName, new()
        {
            AccessType = "CONFIDENTIAL",
            RealmId = realm.Id,
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
    };

    var clientsWithResources = new[] {
        ClientFactory.Build(monolithAPIClientName, new()
        {
            AccessType = "CONFIDENTIAL",
            RealmId = realm.Id,
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
            RootUrl = baseUrlKeycloakServer,
            ServiceAccountsEnabled = true,
            StandardFlowEnabled = false,
            WebOrigins = new[]
            {
                "*",
            },
        }),
    };

    // Create a Keycloak user admin
    string adminUserName = "admin";
    var adminUser = UserFactory.Build(adminUserName, new()
    {
        RealmId = realm.Id,
        Username = adminUserName,
        Email = $"{adminUserName}@bigpods.com",
        EmailVerified = true,
        Enabled = true,
        FirstName = adminUserName,
        LastName = "",
        InitialPassword = new Pulumi.Keycloak.Inputs.UserInitialPasswordArgs
        {
            Value = "Secret123",
            Temporary = false,
        },
    });

    // Create a Keycloak user product team
    string productTeamUserNameAproveda25 = "apoveda25";
    var productTeamUserApoveda25 = UserFactory.Build(productTeamUserNameAproveda25, new()
    {
        RealmId = realm.Id,
        Username = productTeamUserNameAproveda25,
        Email = "alfpovsistemas@gmail.com",
        EmailVerified = true,
        Enabled = true,
        FirstName = "Alfredo",
        LastName = "Poveda",
        InitialPassword = new Pulumi.Keycloak.Inputs.UserInitialPasswordArgs
        {
            Value = "Secret123",
            Temporary = false,
        },
    });

    // Get a Keycloak default role
    var defaultRole = Pulumi.Keycloak.GetRole.Invoke(new()
    {
        RealmId = realm.Id,
        Name = AuthorizationRoles.Default,
    });

    // Create a Keycloak role admin
    var adminRole = RolesFactory.Build(AuthorizationRoles.Admin, new()
    {
        RealmId = realm.Id,
        ClientId = clientsWithResources[0].Id,
        Name = $"roles:{AuthorizationRoles.Admin}",
        Description = StringUtils.ToCapitalCase(AuthorizationRoles.Admin.Split('-')),
        CompositeRoles = new[] { defaultRole.Apply(role => role.Id) },
    });

    // Create a Keycloak role product team
    var productTeamRole = RolesFactory.Build(AuthorizationRoles.ProductTeam, new()
    {
        RealmId = realm.Id,
        ClientId = clientsWithResources[0].Id,
        Name = $"roles:{AuthorizationRoles.ProductTeam}",
        Description = StringUtils.ToCapitalCase(AuthorizationRoles.ProductTeam.Split('-')),
        CompositeRoles = new[] { defaultRole.Apply(role => role.Id) },
    });

    // Create a Keycloak user roles admin
    var adminUserToAdminRole = UserRolesFactory.Build($"user-roles:{adminUserName}:{AuthorizationRoles.Admin}", new()
    {
        RealmId = realm.Id,
        UserId = adminUser.Id,
        RoleIds = new[] { adminRole.Id, defaultRole.Apply(role => role.Id) },
    });

    // Create a Keycloak user roles product team
    var productTeamUserToProductTeamRoleAproveda25 = UserRolesFactory.Build($"user-roles:{productTeamUserNameAproveda25}:{AuthorizationRoles.ProductTeam}", new()
    {
        RealmId = realm.Id,
        UserId = productTeamUserApoveda25.Id,
        RoleIds = new[] { productTeamRole.Id, defaultRole.Apply(role => role.Id) },
    });

    // Create a Keycloak client authorization scopes
    var scopesNames = AuthorizationScopes.All();
    var clientAuthorizationScopes = clientsWithResources.SelectMany(
        client => scopesNames.Select(
            scope => ClientAuthorizationScopeFactory.Build($"scopes:{scope}", new()
            {
                RealmId = realm.Id.Apply(id => id),
                ResourceServerId = client.Id.Apply(id => id),
                DisplayName = StringUtils.ToCapitalCase(scope.Split('-')),
            })
        )
    );

    // Create a Keycloak client authorization resources client authorization permissions
    var resourcesNames = AuthorizationResources.All();
    var authorizationResourcesMapping = new Dictionary<string, Pulumi.Keycloak.OpenId.ClientAuthorizationResourceArgs>()
    {
        [AuthorizationResources.Products] = new Pulumi.Keycloak.OpenId.ClientAuthorizationResourceArgs
        {
            Scopes = AuthorizationScopes.All().Select(scope => $"scopes:{scope}").ToArray(),
        },
        [AuthorizationResources.Variants] = new Pulumi.Keycloak.OpenId.ClientAuthorizationResourceArgs
        {
            Scopes = AuthorizationScopes.All().Select(scope => $"scopes:{scope}").ToArray(),
        },
        [AuthorizationResources.Attributes] = new Pulumi.Keycloak.OpenId.ClientAuthorizationResourceArgs
        {
            Scopes = AuthorizationScopes.All().Select(scope => $"scopes:{scope}").ToArray(),
        },
        [AuthorizationResources.AttributeTypes] = new Pulumi.Keycloak.OpenId.ClientAuthorizationResourceArgs
        {
            Scopes = AuthorizationScopes.All().Select(scope => $"scopes:{scope}").ToArray(),
        },
    };
    var clientAuthorizationResources = clientsWithResources.SelectMany(
        client => resourcesNames.Select(
            resource => ClientAuthorizationResourceFactory.Build($"resources:{resource}", new()
            {
                RealmId = realm.Id.Apply(id => id),
                ResourceServerId = client.Id.Apply(id => id),
                DisplayName = StringUtils.ToCapitalCase(resource.Split('-')),
                OwnerManagedAccess = true,
                Scopes = authorizationResourcesMapping[resource].Scopes,
            })
        )
    );

    // Create a Keycloak client role policies and 
    var clientRolePolicyNames = AuthorizationPolicies.All();
    var clientRolePolicyMapping = new Dictionary<string, Pulumi.Keycloak.OpenId.Inputs.ClientRolePolicyRoleArgs[]>()
    {
        [AuthorizationPolicies.ProductsCreateOne] = new[] {
            new Pulumi.Keycloak.OpenId.Inputs.ClientRolePolicyRoleArgs
            {
                Id = adminRole.Id.Apply(id => id),
                Required = false,
            },
            new Pulumi.Keycloak.OpenId.Inputs.ClientRolePolicyRoleArgs
            {
                Id = productTeamRole.Id.Apply(id => id),
                Required = false,
            }
        },
        [AuthorizationPolicies.ProductsCreateMany] = new[] {
            new Pulumi.Keycloak.OpenId.Inputs.ClientRolePolicyRoleArgs
            {
                Id = adminRole.Id.Apply(id => id),
                Required = false,
            },
        },
        [AuthorizationPolicies.ProductsReadOne] = new[] {
            new Pulumi.Keycloak.OpenId.Inputs.ClientRolePolicyRoleArgs
            {
                Id = adminRole.Id.Apply(id => id),
                Required = false,
            },
            new Pulumi.Keycloak.OpenId.Inputs.ClientRolePolicyRoleArgs
            {
                Id = productTeamRole.Id.Apply(id => id),
                Required = false,
            }
        },
        [AuthorizationPolicies.ProductsReadMany] = new[] {
            new Pulumi.Keycloak.OpenId.Inputs.ClientRolePolicyRoleArgs
            {
                Id = adminRole.Id.Apply(id => id),
                Required = false,
            },
            new Pulumi.Keycloak.OpenId.Inputs.ClientRolePolicyRoleArgs
            {
                Id = productTeamRole.Id.Apply(id => id),
                Required = false,
            }
        },
        [AuthorizationPolicies.ProductsUpdateOne] = new[] {
            new Pulumi.Keycloak.OpenId.Inputs.ClientRolePolicyRoleArgs
            {
                Id = adminRole.Id.Apply(id => id),
                Required = false,
            },
            new Pulumi.Keycloak.OpenId.Inputs.ClientRolePolicyRoleArgs
            {
                Id = productTeamRole.Id.Apply(id => id),
                Required = false,
            }
        },
        [AuthorizationPolicies.ProductsUpdateMany] = new[] {
            new Pulumi.Keycloak.OpenId.Inputs.ClientRolePolicyRoleArgs
            {
                Id = adminRole.Id.Apply(id => id),
                Required = false,
            },
        },
        [AuthorizationPolicies.ProductsDeleteOne] = new[] {
            new Pulumi.Keycloak.OpenId.Inputs.ClientRolePolicyRoleArgs
            {
                Id = adminRole.Id.Apply(id => id),
                Required = false,
            },
            new Pulumi.Keycloak.OpenId.Inputs.ClientRolePolicyRoleArgs
            {
                Id = productTeamRole.Id.Apply(id => id),
                Required = false,
            }
        },
        [AuthorizationPolicies.ProductsDeleteMany] = new[] {
            new Pulumi.Keycloak.OpenId.Inputs.ClientRolePolicyRoleArgs
            {
                Id = adminRole.Id.Apply(id => id),
                Required = false,
            },
        },

        [AuthorizationPolicies.VariantsCreateOne] = new[] {
            new Pulumi.Keycloak.OpenId.Inputs.ClientRolePolicyRoleArgs
            {
                Id = adminRole.Id.Apply(id => id),
                Required = false,
            },
            new Pulumi.Keycloak.OpenId.Inputs.ClientRolePolicyRoleArgs
            {
                Id = productTeamRole.Id.Apply(id => id),
                Required = false,
            }
        },
        [AuthorizationPolicies.VariantsCreateMany] = new[] {
            new Pulumi.Keycloak.OpenId.Inputs.ClientRolePolicyRoleArgs
            {
                Id = adminRole.Id.Apply(id => id),
                Required = false,
            },
        },
        [AuthorizationPolicies.VariantsReadOne] = new[] {
            new Pulumi.Keycloak.OpenId.Inputs.ClientRolePolicyRoleArgs
            {
                Id = adminRole.Id.Apply(id => id),
                Required = false,
            },
            new Pulumi.Keycloak.OpenId.Inputs.ClientRolePolicyRoleArgs
            {
                Id = productTeamRole.Id.Apply(id => id),
                Required = false,
            }
        },
        [AuthorizationPolicies.VariantsReadMany] = new[] {
            new Pulumi.Keycloak.OpenId.Inputs.ClientRolePolicyRoleArgs
            {
                Id = adminRole.Id.Apply(id => id),
                Required = false,
            },
            new Pulumi.Keycloak.OpenId.Inputs.ClientRolePolicyRoleArgs
            {
                Id = productTeamRole.Id.Apply(id => id),
                Required = false,
            }
        },
        [AuthorizationPolicies.VariantsUpdateOne] = new[] {
            new Pulumi.Keycloak.OpenId.Inputs.ClientRolePolicyRoleArgs
            {
                Id = adminRole.Id.Apply(id => id),
                Required = false,
            },
            new Pulumi.Keycloak.OpenId.Inputs.ClientRolePolicyRoleArgs
            {
                Id = productTeamRole.Id.Apply(id => id),
                Required = false,
            }
        },
        [AuthorizationPolicies.VariantsUpdateMany] = new[] {
            new Pulumi.Keycloak.OpenId.Inputs.ClientRolePolicyRoleArgs
            {
                Id = adminRole.Id.Apply(id => id),
                Required = false,
            },
        },
        [AuthorizationPolicies.VariantsDeleteOne] = new[] {
            new Pulumi.Keycloak.OpenId.Inputs.ClientRolePolicyRoleArgs
            {
                Id = adminRole.Id.Apply(id => id),
                Required = false,
            },
            new Pulumi.Keycloak.OpenId.Inputs.ClientRolePolicyRoleArgs
            {
                Id = productTeamRole.Id.Apply(id => id),
                Required = false,
            }
        },
        [AuthorizationPolicies.VariantsDeleteMany] = new[] {
            new Pulumi.Keycloak.OpenId.Inputs.ClientRolePolicyRoleArgs
            {
                Id = adminRole.Id.Apply(id => id),
                Required = false,
            },
        },

        [AuthorizationPolicies.AttributesCreateOne] = new[] {
            new Pulumi.Keycloak.OpenId.Inputs.ClientRolePolicyRoleArgs
            {
                Id = adminRole.Id.Apply(id => id),
                Required = false,
            },
            new Pulumi.Keycloak.OpenId.Inputs.ClientRolePolicyRoleArgs
            {
                Id = productTeamRole.Id.Apply(id => id),
                Required = false,
            }
        },
        [AuthorizationPolicies.AttributesCreateMany] = new[] {
            new Pulumi.Keycloak.OpenId.Inputs.ClientRolePolicyRoleArgs
            {
                Id = adminRole.Id.Apply(id => id),
                Required = false,
            },
        },
        [AuthorizationPolicies.AttributesReadOne] = new[] {
            new Pulumi.Keycloak.OpenId.Inputs.ClientRolePolicyRoleArgs
            {
                Id = adminRole.Id.Apply(id => id),
                Required = false,
            },
            new Pulumi.Keycloak.OpenId.Inputs.ClientRolePolicyRoleArgs
            {
                Id = productTeamRole.Id.Apply(id => id),
                Required = false,
            }
        },
        [AuthorizationPolicies.AttributesReadMany] = new[] {
            new Pulumi.Keycloak.OpenId.Inputs.ClientRolePolicyRoleArgs
            {
                Id = adminRole.Id.Apply(id => id),
                Required = false,
            },
            new Pulumi.Keycloak.OpenId.Inputs.ClientRolePolicyRoleArgs
            {
                Id = productTeamRole.Id.Apply(id => id),
                Required = false,
            }
        },
        [AuthorizationPolicies.AttributesUpdateOne] = new[] {
            new Pulumi.Keycloak.OpenId.Inputs.ClientRolePolicyRoleArgs
            {
                Id = adminRole.Id.Apply(id => id),
                Required = false,
            },
            new Pulumi.Keycloak.OpenId.Inputs.ClientRolePolicyRoleArgs
            {
                Id = productTeamRole.Id.Apply(id => id),
                Required = false,
            }
        },
        [AuthorizationPolicies.AttributesUpdateMany] = new[] {
            new Pulumi.Keycloak.OpenId.Inputs.ClientRolePolicyRoleArgs
            {
                Id = adminRole.Id.Apply(id => id),
                Required = false,
            },
        },
        [AuthorizationPolicies.AttributesDeleteOne] = new[] {
            new Pulumi.Keycloak.OpenId.Inputs.ClientRolePolicyRoleArgs
            {
                Id = adminRole.Id.Apply(id => id),
                Required = false,
            },
            new Pulumi.Keycloak.OpenId.Inputs.ClientRolePolicyRoleArgs
            {
                Id = productTeamRole.Id.Apply(id => id),
                Required = false,
            }
        },
        [AuthorizationPolicies.AttributesDeleteMany] = new[] {
            new Pulumi.Keycloak.OpenId.Inputs.ClientRolePolicyRoleArgs
            {
                Id = adminRole.Id.Apply(id => id),
                Required = false,
            },
        },

        [AuthorizationPolicies.AttributeTypesCreateOne] = new[] {
            new Pulumi.Keycloak.OpenId.Inputs.ClientRolePolicyRoleArgs
            {
                Id = adminRole.Id.Apply(id => id),
                Required = false,
            },
        },
        [AuthorizationPolicies.AttributeTypesCreateMany] = new[] {
            new Pulumi.Keycloak.OpenId.Inputs.ClientRolePolicyRoleArgs
            {
                Id = adminRole.Id.Apply(id => id),
                Required = false,
            },
        },
        [AuthorizationPolicies.AttributeTypesReadOne] = new[] {
            new Pulumi.Keycloak.OpenId.Inputs.ClientRolePolicyRoleArgs
            {
                Id = adminRole.Id.Apply(id => id),
                Required = false,
            },
        },
        [AuthorizationPolicies.AttributeTypesReadMany] = new[] {
            new Pulumi.Keycloak.OpenId.Inputs.ClientRolePolicyRoleArgs
            {
                Id = adminRole.Id.Apply(id => id),
                Required = false,
            },
        },
        [AuthorizationPolicies.AttributeTypesUpdateOne] = new[] {
            new Pulumi.Keycloak.OpenId.Inputs.ClientRolePolicyRoleArgs
            {
                Id = adminRole.Id.Apply(id => id),
                Required = false,
            },
        },
        [AuthorizationPolicies.AttributeTypesUpdateMany] = new[] {
            new Pulumi.Keycloak.OpenId.Inputs.ClientRolePolicyRoleArgs
            {
                Id = adminRole.Id.Apply(id => id),
                Required = false,
            },
        },
        [AuthorizationPolicies.AttributeTypesDeleteOne] = new[] {
            new Pulumi.Keycloak.OpenId.Inputs.ClientRolePolicyRoleArgs
            {
                Id = adminRole.Id.Apply(id => id),
                Required = false,
            },
        },
        [AuthorizationPolicies.AttributeTypesDeleteMany] = new[] {
            new Pulumi.Keycloak.OpenId.Inputs.ClientRolePolicyRoleArgs
            {
                Id = adminRole.Id.Apply(id => id),
                Required = false,
            },
        },
    };
    var clientAuthorizationPermissionsMapping = new Dictionary<string, Pulumi.Keycloak.OpenId.ClientAuthorizationPermissionArgs>()
    {
        [AuthorizationPermissions.ProductsCreateOne] = new Pulumi.Keycloak.OpenId.ClientAuthorizationPermissionArgs
        {
            Resources = new[] { $"resources:{AuthorizationResources.Products}" },
            Scopes = new[] { $"scopes:{AuthorizationScopes.CreateOne}" },
        },
        [AuthorizationPermissions.ProductsCreateMany] = new Pulumi.Keycloak.OpenId.ClientAuthorizationPermissionArgs
        {
            Resources = new[] { $"resources:{AuthorizationResources.Products}" },
            Scopes = new[] { $"scopes:{AuthorizationScopes.CreateMany}" },
        },
        [AuthorizationPermissions.ProductsReadOne] = new Pulumi.Keycloak.OpenId.ClientAuthorizationPermissionArgs
        {
            Resources = new[] { $"resources:{AuthorizationResources.Products}" },
            Scopes = new[] { $"scopes:{AuthorizationScopes.ReadOne}" },
        },
        [AuthorizationPermissions.ProductsReadMany] = new Pulumi.Keycloak.OpenId.ClientAuthorizationPermissionArgs
        {
            Resources = new[] { $"resources:{AuthorizationResources.Products}" },
            Scopes = new[] { $"scopes:{AuthorizationScopes.ReadMany}" },
        },
        [AuthorizationPermissions.ProductsUpdateOne] = new Pulumi.Keycloak.OpenId.ClientAuthorizationPermissionArgs
        {
            Resources = new[] { $"resources:{AuthorizationResources.Products}" },
            Scopes = new[] { $"scopes:{AuthorizationScopes.UpdateOne}" },
        },
        [AuthorizationPermissions.ProductsUpdateMany] = new Pulumi.Keycloak.OpenId.ClientAuthorizationPermissionArgs
        {
            Resources = new[] { $"resources:{AuthorizationResources.Products}" },
            Scopes = new[] { $"scopes:{AuthorizationScopes.UpdateMany}" },
        },
        [AuthorizationPermissions.ProductsDeleteOne] = new Pulumi.Keycloak.OpenId.ClientAuthorizationPermissionArgs
        {
            Resources = new[] { $"resources:{AuthorizationResources.Products}" },
            Scopes = new[] { $"scopes:{AuthorizationScopes.DeleteOne}" },
        },
        [AuthorizationPermissions.ProductsDeleteMany] = new Pulumi.Keycloak.OpenId.ClientAuthorizationPermissionArgs
        {
            Resources = new[] { $"resources:{AuthorizationResources.Products}" },
            Scopes = new[] { $"scopes:{AuthorizationScopes.DeleteMany}" },
        },

        [AuthorizationPermissions.VariantsCreateOne] = new Pulumi.Keycloak.OpenId.ClientAuthorizationPermissionArgs
        {
            Resources = new[] { $"resources:{AuthorizationResources.Variants}" },
            Scopes = new[] { $"scopes:{AuthorizationScopes.CreateOne}" },
        },
        [AuthorizationPermissions.VariantsCreateMany] = new Pulumi.Keycloak.OpenId.ClientAuthorizationPermissionArgs
        {
            Resources = new[] { $"resources:{AuthorizationResources.Variants}" },
            Scopes = new[] { $"scopes:{AuthorizationScopes.CreateMany}" },
        },
        [AuthorizationPermissions.VariantsReadOne] = new Pulumi.Keycloak.OpenId.ClientAuthorizationPermissionArgs
        {
            Resources = new[] { $"resources:{AuthorizationResources.Variants}" },
            Scopes = new[] { $"scopes:{AuthorizationScopes.ReadOne}" },
        },
        [AuthorizationPermissions.VariantsReadMany] = new Pulumi.Keycloak.OpenId.ClientAuthorizationPermissionArgs
        {
            Resources = new[] { $"resources:{AuthorizationResources.Variants}" },
            Scopes = new[] { $"scopes:{AuthorizationScopes.ReadMany}" },
        },
        [AuthorizationPermissions.VariantsUpdateOne] = new Pulumi.Keycloak.OpenId.ClientAuthorizationPermissionArgs
        {
            Resources = new[] { $"resources:{AuthorizationResources.Variants}" },
            Scopes = new[] { $"scopes:{AuthorizationScopes.UpdateOne}" },
        },
        [AuthorizationPermissions.VariantsUpdateMany] = new Pulumi.Keycloak.OpenId.ClientAuthorizationPermissionArgs
        {
            Resources = new[] { $"resources:{AuthorizationResources.Variants}" },
            Scopes = new[] { $"scopes:{AuthorizationScopes.UpdateMany}" },
        },
        [AuthorizationPermissions.VariantsDeleteOne] = new Pulumi.Keycloak.OpenId.ClientAuthorizationPermissionArgs
        {
            Resources = new[] { $"resources:{AuthorizationResources.Variants}" },
            Scopes = new[] { $"scopes:{AuthorizationScopes.DeleteOne}" },
        },
        [AuthorizationPermissions.VariantsDeleteMany] = new Pulumi.Keycloak.OpenId.ClientAuthorizationPermissionArgs
        {
            Resources = new[] { $"resources:{AuthorizationResources.Variants}" },
            Scopes = new[] { $"scopes:{AuthorizationScopes.DeleteMany}" },
        },

        [AuthorizationPermissions.AttributesCreateOne] = new Pulumi.Keycloak.OpenId.ClientAuthorizationPermissionArgs
        {
            Resources = new[] { $"resources:{AuthorizationResources.Attributes}" },
            Scopes = new[] { $"scopes:{AuthorizationScopes.CreateOne}" },
        },
        [AuthorizationPermissions.AttributesCreateMany] = new Pulumi.Keycloak.OpenId.ClientAuthorizationPermissionArgs
        {
            Resources = new[] { $"resources:{AuthorizationResources.Attributes}" },
            Scopes = new[] { $"scopes:{AuthorizationScopes.CreateMany}" },
        },
        [AuthorizationPermissions.AttributesReadOne] = new Pulumi.Keycloak.OpenId.ClientAuthorizationPermissionArgs
        {
            Resources = new[] { $"resources:{AuthorizationResources.Attributes}" },
            Scopes = new[] { $"scopes:{AuthorizationScopes.ReadOne}" },
        },
        [AuthorizationPermissions.AttributesReadMany] = new Pulumi.Keycloak.OpenId.ClientAuthorizationPermissionArgs
        {
            Resources = new[] { $"resources:{AuthorizationResources.Attributes}" },
            Scopes = new[] { $"scopes:{AuthorizationScopes.ReadMany}" },
        },
        [AuthorizationPermissions.AttributesUpdateOne] = new Pulumi.Keycloak.OpenId.ClientAuthorizationPermissionArgs
        {
            Resources = new[] { $"resources:{AuthorizationResources.Attributes}" },
            Scopes = new[] { $"scopes:{AuthorizationScopes.UpdateOne}" },
        },
        [AuthorizationPermissions.AttributesUpdateMany] = new Pulumi.Keycloak.OpenId.ClientAuthorizationPermissionArgs
        {
            Resources = new[] { $"resources:{AuthorizationResources.Attributes}" },
            Scopes = new[] { $"scopes:{AuthorizationScopes.UpdateMany}" },
        },
        [AuthorizationPermissions.AttributesDeleteOne] = new Pulumi.Keycloak.OpenId.ClientAuthorizationPermissionArgs
        {
            Resources = new[] { $"resources:{AuthorizationResources.Attributes}" },
            Scopes = new[] { $"scopes:{AuthorizationScopes.DeleteOne}" },
        },
        [AuthorizationPermissions.AttributesDeleteMany] = new Pulumi.Keycloak.OpenId.ClientAuthorizationPermissionArgs
        {
            Resources = new[] { $"resources:{AuthorizationResources.Attributes}" },
            Scopes = new[] { $"scopes:{AuthorizationScopes.DeleteMany}" },
        },

        [AuthorizationPermissions.AttributeTypesCreateOne] = new Pulumi.Keycloak.OpenId.ClientAuthorizationPermissionArgs
        {
            Resources = new[] { $"resources:{AuthorizationResources.AttributeTypes}" },
            Scopes = new[] { $"scopes:{AuthorizationScopes.CreateOne}" },
        },
        [AuthorizationPermissions.AttributeTypesCreateMany] = new Pulumi.Keycloak.OpenId.ClientAuthorizationPermissionArgs
        {
            Resources = new[] { $"resources:{AuthorizationResources.AttributeTypes}" },
            Scopes = new[] { $"scopes:{AuthorizationScopes.CreateMany}" },
        },
        [AuthorizationPermissions.AttributeTypesReadOne] = new Pulumi.Keycloak.OpenId.ClientAuthorizationPermissionArgs
        {
            Resources = new[] { $"resources:{AuthorizationResources.AttributeTypes}" },
            Scopes = new[] { $"scopes:{AuthorizationScopes.ReadOne}" },
        },
        [AuthorizationPermissions.AttributeTypesReadMany] = new Pulumi.Keycloak.OpenId.ClientAuthorizationPermissionArgs
        {
            Resources = new[] { $"resources:{AuthorizationResources.AttributeTypes}" },
            Scopes = new[] { $"scopes:{AuthorizationScopes.ReadMany}" },
        },
        [AuthorizationPermissions.AttributeTypesUpdateOne] = new Pulumi.Keycloak.OpenId.ClientAuthorizationPermissionArgs
        {
            Resources = new[] { $"resources:{AuthorizationResources.AttributeTypes}" },
            Scopes = new[] { $"scopes:{AuthorizationScopes.UpdateOne}" },
        },
        [AuthorizationPermissions.AttributeTypesUpdateMany] = new Pulumi.Keycloak.OpenId.ClientAuthorizationPermissionArgs
        {
            Resources = new[] { $"resources:{AuthorizationResources.AttributeTypes}" },
            Scopes = new[] { $"scopes:{AuthorizationScopes.UpdateMany}" },
        },
        [AuthorizationPermissions.AttributeTypesDeleteOne] = new Pulumi.Keycloak.OpenId.ClientAuthorizationPermissionArgs
        {
            Resources = new[] { $"resources:{AuthorizationResources.AttributeTypes}" },
            Scopes = new[] { $"scopes:{AuthorizationScopes.DeleteOne}" },
        },
        [AuthorizationPermissions.AttributeTypesDeleteMany] = new Pulumi.Keycloak.OpenId.ClientAuthorizationPermissionArgs
        {
            Resources = new[] { $"resources:{AuthorizationResources.AttributeTypes}" },
            Scopes = new[] { $"scopes:{AuthorizationScopes.DeleteMany}" },
        },
    };

    var clientRolePoliciesClientAuthorizationPermissionsMonolithAPIClient = clientRolePolicyNames.Select((policy, index) =>
        {
            string permission = AuthorizationPermissions.All()[index];

            var clientRolePolicy = ClientRolePolicyFactory.Build($"policies:{policy}", new()
            {
                RealmId = realm.Id.Apply(id => id),
                ResourceServerId = clientsWithResources[0].Id.Apply(id => id),
                Roles = clientRolePolicyMapping[policy],
                Type = "role",
                DecisionStrategy = "UNANIMOUS",
                Description = StringUtils.ToCapitalCase(policy.Replace(":", "-").Split('-')),
                Logic = "POSITIVE",
            });

            var clientAuthorizationPermission = ClientAuthorizationPermissionFactory.Build($"permissions:{permission}", new()
            {
                RealmId = realm.Id.Apply(id => id),
                ResourceServerId = clientsWithResources[0].Id.Apply(id => id),
                DecisionStrategy = "UNANIMOUS",
                Description = StringUtils.ToCapitalCase(permission.Replace(":", "-").Split('-')),
                Policies = clientRolePolicy.Id.Apply(id => new[] { id }),
                Resources = clientAuthorizationPermissionsMapping[permission].Resources,
                Scopes = clientAuthorizationPermissionsMapping[permission].Scopes,
                Type = "scope"
            });

            return Output.All(clientRolePolicy.Id, clientRolePolicy.Name, clientAuthorizationPermission.Id, clientAuthorizationPermission.Name);
        }
    );

    // Create a Keycloak client scopes
    var clientScopesMonolithAPIClient = ClientScopesFactory.Build($"client-scopes:{ClientScopes.Audience}", new()
    {
        RealmId = realm.Id.Apply(id => id),
        Description = StringUtils.ToCapitalCase(ClientScopes.Audience),
        IncludeInTokenScope = false,
    });

    // Create a Keycloak audience protocol mapper
    var audienceProtocolMapperMonolithAPIClient = AudienceProtocolMapperFactory.Build(
        $"protocol-mapper:{ClientScopes.Audience}:{monolithAPIClientName.ToLower().Replace(" ", "-")}",
        new()
        {
            RealmId = realm.Id.Apply(id => id),
            AddToAccessToken = true,
            AddToIdToken = false,
            ClientScopeId = clientScopesMonolithAPIClient.Id.Apply(id => id),
            IncludedClientAudience = clientsWithResources[0].ClientId.Apply(id => id),
        }
    );

    return new Dictionary<string, object?>
    {
        ["realmId"] = realm.RealmName,
        ["clientsWithoutResourcesIds"] = Output.All(
            clientsWithoutResources.Select(
                client => Output.All(
                    client.ClientId,
                    client.Id
                )
                .Apply(client => client)
            )
        ),
        ["clientsWithResourcesIds"] = Output.All(
            clientsWithResources.Select(
                client => Output.All(
                    client.ClientId,
                    client.Id
                )
                .Apply(client => client)
            )
        ),
        ["usersIds"] = Output.All(
            new[] { adminUser, productTeamUserApoveda25 }
                .Select(
                    user => Output.All(
                        user.Username,
                        user.Id
                    )
                    .Apply(user => user)
                )
        ),
        ["rolesIds"] = Output.All(
            new[] {
                adminRole,
                productTeamRole
            }
                .Select(
                    role => Output.All(
                        role.Name,
                        role.Id
                    )
                    .Apply(role => role)
                )
        ),
        ["userRolesIds"] = Output.All(
            new[] {
                adminUserToAdminRole,
                productTeamUserToProductTeamRoleAproveda25
            }
                .Select(
                    userRole => Output.All(
                        userRole.Id,
                        userRole.UserId
                    )
                    .Apply(userRole => userRole)
                )
        ),
        ["clientRolePoliciesClientAuthorizationPermissionsIds"] = Output.All(
            clientRolePoliciesClientAuthorizationPermissionsMonolithAPIClient
        ),
        ["clientAuthorizationScopesIds"] = Output.All(
            clientAuthorizationScopes.Select(
                scope => Output.All(
                    scope.Name,
                    scope.Id
                )
                .Apply(scope => scope)
            )
        ),
        ["clientAuthorizationResourcesIds"] = Output.All(
            clientAuthorizationResources.Select(
                resource => Output.All(
                    resource.Name,
                    resource.Id
                )
                .Apply(resource => resource)
            )
        ),
        ["clientScopesIds"] = Output.All(
            new[] { clientScopesMonolithAPIClient }
                .Select(clientScope =>
                    Output.All(
                        clientScope.Id,
                        clientScope.Name
                    )
                    .Apply(clientScope => clientScope)
                )
        ),
        ["audienceProtocolMapperIds"] = Output.All(
            new[] { audienceProtocolMapperMonolithAPIClient }
                .Select(protocolMapper =>
                    Output.All(
                        protocolMapper.Id,
                        protocolMapper.Name
                    )
                    .Apply(protocolMapper => protocolMapper)
                )
        ),
    };
});