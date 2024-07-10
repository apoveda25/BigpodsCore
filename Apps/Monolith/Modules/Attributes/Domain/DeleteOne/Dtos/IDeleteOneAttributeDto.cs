namespace Bigpods.Monolith.Modules.Attributes.Domain.DeleteOne.Dtos;

public interface IDeleteOneAttributeDto
{
    public Guid Id { get; init; }

    public string DeletedAtTimezone { get; init; }

    public Guid DeletedBy { get; init; }
}
