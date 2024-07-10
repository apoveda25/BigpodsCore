using Bigpods.Monolith.Modules.AttributeTypes.Domain.Common.Factories;
using Bigpods.Monolith.Modules.AttributeTypes.Domain.CreateOne.Commands;
using Bigpods.Monolith.Modules.AttributeTypes.Domain.CreateOne.Services;
using Bigpods.Monolith.Modules.AttributeTypes.Domain.UpdateOne.Commands;
using Bigpods.Monolith.Modules.AttributeTypes.Domain.UpdateOne.Services;
using Bigpods.Monolith.Modules.Shared.Domain.ValueObjects;

namespace Bigpods.Monolith.Modules.AttributeTypes.Domain.Common.Aggregates;

public sealed class AttributeTypeAggregateRoot(
    Guid? id = null,
    string? name = null,
    string? description = null,
    string? valuePattern = null,
    string? measuringUnitPattern = null,
    bool? isDeleted = null,
    DateTime? createdAtDatetime = null,
    DateTime? updatedAtDatetime = null,
    DateTime? deletedAtDatetime = null,
    string? createdAtTimezone = null,
    string? updatedAtTimezone = null,
    string? deletedAtTimezone = null,
    Guid? createdBy = null,
    Guid? updatedBy = null,
    Guid? deletedBy = null
)
{
    public Guid Id { get; private set; } = id ?? Guid.NewGuid();
    public WordVO Name { get; private set; } = new WordVO(name ?? string.Empty);
    public SentenceVO Description { get; private set; } =
        new SentenceVO(description ?? string.Empty);
    public string ValuePattern { get; private set; } = valuePattern ?? string.Empty;
    public string MeasuringUnitPattern { get; private set; } = measuringUnitPattern ?? string.Empty;
    public bool IsDeleted { get; private set; } = isDeleted ?? false;
    public DateTime CreatedAtDatetime { get; private set; } = createdAtDatetime ?? DateTime.Now;
    public DateTime? UpdatedAtDatetime { get; private set; } = updatedAtDatetime;
    public DateTime? DeletedAtDatetime { get; private set; } = deletedAtDatetime;
    public string CreatedAtTimezone { get; private set; } = createdAtTimezone ?? string.Empty;
    public string? UpdatedAtTimezone { get; private set; } = updatedAtTimezone;
    public string? DeletedAtTimezone { get; private set; } = deletedAtTimezone;
    public Guid CreatedBy { get; private set; } = createdBy ?? Guid.Empty;
    public Guid? UpdatedBy { get; private set; } = updatedBy;
    public Guid? DeletedBy { get; private set; } = deletedBy;

    public static AttributeTypeAggregateRoot CreateOne(
        ICreateOneAttributeTypeCommand command,
        ICreateOneAttributeTypeServiceResponse data
    )
    {
        return AttributeTypeAggregateRootFactory.CreateOne(
            attributeType: command.AttributeTypeDto,
            data: data
        );
    }

    public static AttributeTypeAggregateRoot UpdateOne(
        IUpdateOneAttributeTypeCommand command,
        IUpdateOneAttributeTypeServiceResponse data
    )
    {
        return AttributeTypeAggregateRootFactory.UpdateOne(
            attributeType: command.AttributeTypeDto,
            data: data
        );
    }
}
