namespace Bigpods.MonolithIaC.Scopes;

public record AuthorizationScopes
{
    public static string CreateOne => "create-one";
    public static string CreateMany => "create-many";
    public static string ReadMany => "read-many";
    public static string ReadOne => "read-one";
    public static string UpdateOne => "update-one";
    public static string UpdateMany => "update-many";
    public static string DeleteOne => "delete-one";
    public static string DeleteMany => "delete-many";

    public static string[] All() => new[] { CreateOne, CreateMany, ReadMany, ReadOne, UpdateOne, UpdateMany, DeleteOne, DeleteMany };
}
