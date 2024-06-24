namespace Bigpods.Shared;

public record ClientAuthorizationScopes
{
    public static string CreateOne => "create-one-scope";
    public static string CreateMany => "create-many-scope";
    public static string ReadMany => "read-many-scope";
    public static string ReadOne => "read-one-scope";
    public static string UpdateOne => "update-one-scope";
    public static string UpdateMany => "update-many-scope";
    public static string DeleteOne => "delete-one-scope";
    public static string DeleteMany => "delete-many-scope";
    public static string CountMany => "count-many-scope";
}
