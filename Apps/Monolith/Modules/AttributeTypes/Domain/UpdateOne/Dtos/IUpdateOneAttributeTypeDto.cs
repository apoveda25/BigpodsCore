namespace Bigpods.Monolith.Modules.AttributeTypes.Domain.UpdateOne.Dtos;

public interface IUpdateOneAttributeTypeDto
{
    public Guid Id { get; init; }
    public string? Name { get; init; }
    public string? Description { get; init; }
    public string? ValuePattern { get; init; }
    public string? MeasuringUnitPattern { get; init; }
    public string UpdatedAtTimezone { get; init; }
    public Guid UpdatedBy { get; init; }
}
