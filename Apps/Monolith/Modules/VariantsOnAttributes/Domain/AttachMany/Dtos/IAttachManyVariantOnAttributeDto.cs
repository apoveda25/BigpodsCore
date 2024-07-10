namespace Bigpods.Monolith.Modules.VariantsOnAttributes.Domain.AttachMany.Dtos;

public interface IAttachManyVariantOnAttributeDto
{
    public Guid ProductId { get; init; }
}

public interface ICreateOneVariantOnAttributeDto
{
    public Guid Id { get; init; }
    public string CreatedAtTimezone { get; init; }
    public Guid CreatedBy { get; init; }
    public Guid AttributeId { get; init; }
    public Guid VariantId { get; init; }
}
