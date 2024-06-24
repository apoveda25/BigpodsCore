namespace Bigpods.MonolithIaC.Resources;

public record AuthorizationResources
{
    public static string Products => "products";
    public static string Variants => "variants";
    public static string Attributes => "attributes";
    public static string AttributeTypes => "attribute-classes";

    public static string[] All() => new[] { Products, Variants, Attributes, AttributeTypes };
}
