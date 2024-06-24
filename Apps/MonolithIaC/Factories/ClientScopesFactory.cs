namespace Bigpods.MonolithIaC.Factories;

public class ClientScopesFactory
{
    public static Pulumi.Keycloak.OpenId.ClientScope Build(string name, Pulumi.Keycloak.OpenId.ClientScopeArgs args)
    {
        return new Pulumi.Keycloak.OpenId.ClientScope(name, new()
        {
            RealmId = args.RealmId,
            ConsentScreenText = args.ConsentScreenText,
            Description = args.Description,
            GuiOrder = args.GuiOrder ?? 0,
            IncludeInTokenScope = args.IncludeInTokenScope ?? false,
            Name = args.Name ?? name,
        });
    }
}
