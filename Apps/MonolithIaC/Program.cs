using System.Collections.Generic;
using System.Linq;

using Bigpods.MonolithIaC.Data.AudienceProtocolMappers;

using Bigpods.MonolithIaC.Data.ClientAuthorizationPermissions;

using Bigpods.MonolithIaC.Data.ClientAuthorizationResources;

using Bigpods.MonolithIaC.Data.ClientAuthorizationScopes;
using Bigpods.MonolithIaC.Data.ClientRolePolicies;

using Bigpods.MonolithIaC.Data.Clients;
using Bigpods.MonolithIaC.Data.ClientScopes;
using Bigpods.MonolithIaC.Data.DefaultClientScopes;
using Bigpods.MonolithIaC.Data.Realms;
using Bigpods.MonolithIaC.Data.Roles;
using Bigpods.MonolithIaC.Data.Users;
using Bigpods.MonolithIaC.Data.UsersRoles;

using Bigpods.MonolithIaC.Factories;

using Pulumi;

return await Deployment.RunAsync(() =>
{
    // Create Keycloak realms
    var realmsCreated = RealmsData
        .GetRealms()
        .AsParallel()
        .ToDictionary(item => item.Key, item => RealmFactory.Build(item.Value.Name, item.Value.Config));

    // Create Keycloak clients
    var clientsCreated = ClientsData
        .GetClients(realms: realmsCreated)
        .AsParallel()
        .ToDictionary(item => item.Key, item => ClientFactory.Build(item.Value.Name, item.Value.Config));

    // Create a Keycloak users
    var usersCreated = UsersData
        .GetUsers(realms: realmsCreated)
        .AsParallel()
        .ToDictionary(item => item.Key, item => UserFactory.Build(item.Value.Name, item.Value.Config));

    // Get Keycloak default roles
    var gettingRoles = RolesData
        .InvokeRoles(realms: realmsCreated);

    // Create Keycloak roles
    var rolesCreated = RolesData
        .GetRoles(realms: realmsCreated, clients: clientsCreated, gettingRoles: gettingRoles)
        .AsParallel()
        .ToDictionary(item => item.Key, item => RolesFactory.Build(item.Value.Name, item.Value.Config));

    // Create Keycloak user roles
    var userRolesCreated = UsersRolesData
        .GetUsersRoles(realms: realmsCreated, users: usersCreated, roles: rolesCreated, gettingRoles: gettingRoles)
        .AsParallel()
        .ToDictionary(item => item.Key, item => UserRolesFactory.Build(item.Value.Name, item.Value.Config));

    // Create Keycloak client authorization scopes
    var clientAuthorizationScopesCreated = ClientAuthorizationScopesData
        .GetClientAuthorizationScopes(realms: realmsCreated, clients: clientsCreated)
        .AsParallel()
        .ToDictionary(item => item.Key, item => ClientAuthorizationScopeFactory.Build(item.Value.Name, item.Value.Config));

    // Create Keycloak client authorization resources
    var clientAuthorizationResourcesCreated = ClientAuthorizationResourcesData
        .GetClientAuthorizationResources(realms: realmsCreated, clients: clientsCreated, scopes: clientAuthorizationScopesCreated)
        .AsParallel()
        .ToDictionary(item => item.Key, item => ClientAuthorizationResourceFactory.Build(item.Value.Name, item.Value.Config));

    // Create Keycloak client role policies
    var clientRolePoliciesCreated = ClientRolePoliciesData
        .GetClientRolePolicies(realms: realmsCreated, clients: clientsCreated, roles: rolesCreated)
        .AsParallel()
        .ToDictionary(item => item.Key, item => ClientRolePolicyFactory.Build(item.Value.Name, item.Value.Config));

    // Create Keycloak client authorization permissions
    var clientAuthorizationPermissionsCreated = ClientAuthorizationPermissionsData
        .GetClientAuthorizationPermissions(
            realms: realmsCreated,
            clients: clientsCreated,
            policies: clientRolePoliciesCreated,
            resources: clientAuthorizationResourcesCreated,
            scopes: clientAuthorizationScopesCreated
        )
        .AsParallel()
        .ToDictionary(item => item.Key, item => ClientAuthorizationPermissionFactory.Build(item.Value.Name, item.Value.Config));

    // Create Keycloak client scopes
    var clientScopesCreated = ClientScopesData
        .GetClientScopes(realms: realmsCreated)
        .AsParallel()
        .ToDictionary(item => item.Key, item => ClientScopesFactory.Build(item.Value.Name, item.Value.Config));

    // Create Keycloak audience protocol mappers
    var audienceProtocolMappersCreated = AudienceProtocolMappersData
        .GetAudienceProtocolMappers(realms: realmsCreated, clients: clientsCreated, clientScopes: clientScopesCreated)
        .AsParallel()
        .ToDictionary(item => item.Key, item => AudienceProtocolMapperFactory.Build(item.Value.Name, item.Value.Config));

    // Create Keycloak client default scopes
    var clientDefaultScopesCreated = ClientDefaultScopesData
        .GetClientDefaultScopes(realms: realmsCreated, clients: clientsCreated, clientScopes: clientScopesCreated)
        .AsParallel()
        .ToDictionary(item => item.Key, item => ClientDefaultScopeFactory.Build(item.Value.Name, item.Value.Config));

    return new Dictionary<string, object?>
    {
        ["realmsId"] = Output.All(
            realmsCreated.Select(
                realm => Output.All(
                    realm.Value.Id
                ).Apply(realm => realm)
            )
        ),
        ["clients"] = Output.All(
            clientsCreated.Select(
                client => Output.All(
                    client.Value.Id,
                    client.Value.ClientId
                ).Apply(client => client)
            )
        ),
        ["usersIds"] = Output.All(
            usersCreated.Select(
                user => Output.All(
                    user.Value.Id,
                    user.Value.FirstName.Apply(x => x ?? string.Empty),
                    user.Value.LastName.Apply(x => x ?? string.Empty)
                ).Apply(user => user)
            )
        ),
        ["rolesIds"] = Output.All(
            rolesCreated.Select(
                role => Output.All(
                    role.Value.Id,
                    role.Value.Name,
                    role.Value.RealmId
                ).Apply(role => role)
            )
        ),
        ["userRolesIds"] = Output.All(
            userRolesCreated.Select(
                userRole => Output.All(
                    userRole.Value.Id,
                    userRole.Value.RoleIds.Apply(x => string.Join(",", x)),
                    userRole.Value.UserId
                ).Apply(userRole => userRole)
            )
        ),
        ["clientAuthorizationScopesIds"] = Output.All(
            clientAuthorizationScopesCreated.Select(
                scope => Output.All(
                    scope.Value.Id,
                    scope.Value.Name
                ).Apply(scope => scope)
            )
        ),
        ["clientAuthorizationResourcesIds"] = Output.All(
            clientAuthorizationResourcesCreated.Select(
                resource => Output.All(
                    resource.Value.Id,
                    resource.Value.Name
                ).Apply(resource => resource)
            )
        ),
        ["clientRolePoliciesIds"] = Output.All(
            clientRolePoliciesCreated.Select(
                policy => Output.All(
                    policy.Value.Id,
                    policy.Value.Name
                ).Apply(policy => policy)
            )
        ),
        ["clientAuthorizationPermissionsIds"] = Output.All(
            clientAuthorizationPermissionsCreated.Select(
                permission => Output.All(
                    permission.Value.Id,
                    permission.Value.Name
                ).Apply(permission => permission)
            )
        ),
        ["clientScopesIds"] = Output.All(
            clientScopesCreated.Select(clientScope =>
                Output.All(
                    clientScope.Value.Id,
                    clientScope.Value.Name,
                    clientScope.Value.RealmId
                ).Apply(clientScope => clientScope)
            )
        ),
        ["audienceProtocolMapperIds"] = Output.All(
            audienceProtocolMappersCreated.Select(protocolMapper =>
                Output.All(
                    protocolMapper.Value.Id,
                    protocolMapper.Value.Name,
                    protocolMapper.Value.RealmId
                ).Apply(protocolMapper => protocolMapper)
            )
        ),
        ["clientDefaultScopesIds"] = Output.All(
            clientDefaultScopesCreated.Select(clientScope =>
                Output.All(
                    clientScope.Value.Id,
                    clientScope.Value.RealmId,
                    clientScope.Value.ClientId
                ).Apply(clientScope => clientScope)
            )
        ),
    };
});