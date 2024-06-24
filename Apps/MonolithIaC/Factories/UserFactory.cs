namespace Bigpods.MonolithIaC.Factories;

public class UserFactory
{
    public static Pulumi.Keycloak.User Build(string name, Pulumi.Keycloak.UserArgs args)
    {
        return new Pulumi.Keycloak.User(name, new()
        {
            RealmId = args.RealmId,
            Username = args.Username,
            Attributes = args.Attributes,
            Email = args.Email,
            EmailVerified = args.EmailVerified ?? false,
            Enabled = args.Enabled ?? false,
            FederatedIdentities = args.FederatedIdentities,
            FirstName = args.FirstName ?? name,
            InitialPassword = args.InitialPassword,
            LastName = args.LastName ?? name,
        });
    }
}
