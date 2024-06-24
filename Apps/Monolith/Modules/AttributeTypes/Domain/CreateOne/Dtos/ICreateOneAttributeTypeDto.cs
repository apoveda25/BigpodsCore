namespace Bigpods.Monolith.Modules.AttributeTypes.Domain.CreateOne.Dtos;

public interface ICreateOneAttributeTypeDto
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public string ValuePattern { get; init; }
    public string MeasuringUnitPattern { get; init; }
    public string CreatedAtTimezone { get; init; }
    public Guid CreatedBy { get; init; }
}
