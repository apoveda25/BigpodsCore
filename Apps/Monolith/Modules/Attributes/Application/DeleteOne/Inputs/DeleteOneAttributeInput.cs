using Bigpods.Monolith.Modules.Attributes.Domain.DeleteOne.Inputs;
using NodaTime;

namespace Bigpods.Monolith.Modules.Attributes.Application.DeleteOne.Inputs;

public sealed record DeleteOneAttributeInput : IDeleteOneAttributeInput
{
    public Guid Id { get; set; } = Guid.Empty;

    public DateTimeZone DeletedAtTimezone { get; set; } = DateTimeZone.Utc;

    [GraphQLIgnore]
    public Guid DeletedBy { get; set; } = Guid.Empty;
}
