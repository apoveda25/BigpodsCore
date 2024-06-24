using Bigpods.MonolithIaC.Resources;
using Bigpods.MonolithIaC.Scopes;

namespace Bigpods.MonolithIaC.Permissions;

public record AuthorizationPermissions
{
    public static string ProductsCreateOne => $"{AuthorizationResources.Products}:{AuthorizationScopes.CreateOne}";
    public static string ProductsCreateMany => $"{AuthorizationResources.Products}:{AuthorizationScopes.CreateMany}";
    public static string ProductsReadOne => $"{AuthorizationResources.Products}:{AuthorizationScopes.ReadOne}";
    public static string ProductsReadMany => $"{AuthorizationResources.Products}:{AuthorizationScopes.ReadMany}";
    public static string ProductsUpdateOne => $"{AuthorizationResources.Products}:{AuthorizationScopes.UpdateOne}";
    public static string ProductsUpdateMany => $"{AuthorizationResources.Products}:{AuthorizationScopes.UpdateMany}";
    public static string ProductsDeleteOne => $"{AuthorizationResources.Products}:{AuthorizationScopes.DeleteOne}";
    public static string ProductsDeleteMany => $"{AuthorizationResources.Products}:{AuthorizationScopes.DeleteMany}";

    public static string VariantsCreateOne => $"{AuthorizationResources.Variants}:{AuthorizationScopes.CreateOne}";
    public static string VariantsCreateMany => $"{AuthorizationResources.Variants}:{AuthorizationScopes.CreateMany}";
    public static string VariantsReadOne => $"{AuthorizationResources.Variants}:{AuthorizationScopes.ReadOne}";
    public static string VariantsReadMany => $"{AuthorizationResources.Variants}:{AuthorizationScopes.ReadMany}";
    public static string VariantsUpdateOne => $"{AuthorizationResources.Variants}:{AuthorizationScopes.UpdateOne}";
    public static string VariantsUpdateMany => $"{AuthorizationResources.Variants}:{AuthorizationScopes.UpdateMany}";
    public static string VariantsDeleteOne => $"{AuthorizationResources.Variants}:{AuthorizationScopes.DeleteOne}";
    public static string VariantsDeleteMany => $"{AuthorizationResources.Variants}:{AuthorizationScopes.DeleteMany}";

    public static string AttributesCreateOne => $"{AuthorizationResources.Attributes}:{AuthorizationScopes.CreateOne}";
    public static string AttributesCreateMany => $"{AuthorizationResources.Attributes}:{AuthorizationScopes.CreateMany}";
    public static string AttributesReadOne => $"{AuthorizationResources.Attributes}:{AuthorizationScopes.ReadOne}";
    public static string AttributesReadMany => $"{AuthorizationResources.Attributes}:{AuthorizationScopes.ReadMany}";
    public static string AttributesUpdateOne => $"{AuthorizationResources.Attributes}:{AuthorizationScopes.UpdateOne}";
    public static string AttributesUpdateMany => $"{AuthorizationResources.Attributes}:{AuthorizationScopes.UpdateMany}";
    public static string AttributesDeleteOne => $"{AuthorizationResources.Attributes}:{AuthorizationScopes.DeleteOne}";
    public static string AttributesDeleteMany => $"{AuthorizationResources.Attributes}:{AuthorizationScopes.DeleteMany}";

    public static string AttributeTypesCreateOne => $"{AuthorizationResources.AttributeTypes}:{AuthorizationScopes.CreateOne}";
    public static string AttributeTypesCreateMany => $"{AuthorizationResources.AttributeTypes}:{AuthorizationScopes.CreateMany}";
    public static string AttributeTypesReadOne => $"{AuthorizationResources.AttributeTypes}:{AuthorizationScopes.ReadOne}";
    public static string AttributeTypesReadMany => $"{AuthorizationResources.AttributeTypes}:{AuthorizationScopes.ReadMany}";
    public static string AttributeTypesUpdateOne => $"{AuthorizationResources.AttributeTypes}:{AuthorizationScopes.UpdateOne}";
    public static string AttributeTypesUpdateMany => $"{AuthorizationResources.AttributeTypes}:{AuthorizationScopes.UpdateMany}";
    public static string AttributeTypesDeleteOne => $"{AuthorizationResources.AttributeTypes}:{AuthorizationScopes.DeleteOne}";
    public static string AttributeTypesDeleteMany => $"{AuthorizationResources.AttributeTypes}:{AuthorizationScopes.DeleteMany}";

    public static string[] All() => new[] {
        ProductsCreateOne,
        ProductsCreateMany,
        ProductsReadOne,
        ProductsReadMany,
        ProductsUpdateOne,
        ProductsUpdateMany,
        ProductsDeleteOne,
        ProductsDeleteMany,

        VariantsCreateOne,
        VariantsCreateMany,
        VariantsReadOne,
        VariantsReadMany,
        VariantsUpdateOne,
        VariantsUpdateMany,
        VariantsDeleteOne,
        VariantsDeleteMany,

        AttributesCreateOne,
        AttributesCreateMany,
        AttributesReadOne,
        AttributesReadMany,
        AttributesUpdateOne,
        AttributesUpdateMany,
        AttributesDeleteOne,
        AttributesDeleteMany,

        AttributeTypesCreateOne,
        AttributeTypesCreateMany,
        AttributeTypesReadOne,
        AttributeTypesReadMany,
        AttributeTypesUpdateOne,
        AttributeTypesUpdateMany,
        AttributeTypesDeleteOne,
        AttributeTypesDeleteMany,
    };
}
