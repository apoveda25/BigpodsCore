using Bigpods.Monolith.Modules.Inventories.Domain.CreateOne.Inputs;
using NodaTime;

namespace Bigpods.Monolith.Modules.Inventories.Application.CreateOne.Inputs;

public sealed record CreateOneInventoryInput : ICreateOneInventoryInput
{
    private Guid _id;

    [DefaultValue("00000000-0000-0000-0000-000000000000")]
    public Optional<Guid> Id
    {
        get => _id;
        set => _id = Guid.Empty.CompareTo(value.Value) == 0 ? Guid.NewGuid() : value;
    }

    public DateTimeZone CreatedAtTimezone { get; set; } = DateTimeZone.Utc;

    [GraphQLIgnore]
    public Guid CreatedBy { get; set; } = Guid.Empty;

    public Guid ProductId { get; set; } = Guid.Empty;

    public Guid VariantId { get; set; } = Guid.Empty;

    public Guid WarehouseId { get; set; } = Guid.Empty;
}
