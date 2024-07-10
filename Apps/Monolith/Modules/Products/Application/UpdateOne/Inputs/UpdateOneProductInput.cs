using Bigpods.Monolith.Modules.Products.Domain.UpdateOne.Inputs;
using NodaTime;

namespace Bigpods.Monolith.Modules.Products.Application.UpdateOne.Inputs;

[GraphQLName(name: "ProductUpdateOneProductInput")]
public record UpdateOneProductInput : IUpdateOneProductInput
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? Brand { get; set; }

    public string? Model { get; set; }

    public DateTimeZone UpdatedAtTimezone { get; set; } = DateTimeZone.Utc;

    [GraphQLIgnore]
    public Guid UpdatedBy { get; set; } = Guid.Empty;
}
