namespace Bigpods.Monolith.Modules.Attributes.Domain.CreateOne.Dtos;

public interface ICreateOneAttributeDto
{
    public Guid Id { get; init; }

    public string Value { get; init; }

    public string MeasuringUnit { get; init; }

    public string CreatedAtTimezone { get; init; }

    public Guid CreatedBy { get; init; }

    public Guid AttributeTypeId { get; init; }
}
