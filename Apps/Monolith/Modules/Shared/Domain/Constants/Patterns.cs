namespace Bigpods.Monolith.Modules.Shared.Domain.Constants;

public record Patterns
{
    public static string Name { get; } =
        @"^([\w\(\),]+(\.|'){0,1}[\w\(\),]*)(\s[\w\(\),]+(\.|'){0,1}[\w\(\),]*)*$";
    public static string Sku { get; } = @"^[\w]+(-[\w]+)+$";
    public static string Sentence { get; } = @"^(\w+([\s\W]+\w+\W*)*)*$";
    public static string Brand { get; } = @"^\w+(\s\w+)*$";
    public static string Model { get; } = @"^\w+((-|\s)\w+)*$";
    public static string Word { get; } = @"^\w+$";
}
