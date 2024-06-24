namespace Bigpods.MonolithIaC.Roles;

public record AuthorizationRoles
{
    public static string Default => "default-roles-bigpods";
    public static string Admin => "admin";
    public static string ProductTeam => "product-team";

    public static string[] All() => new[] { Default, Admin, ProductTeam };
}
