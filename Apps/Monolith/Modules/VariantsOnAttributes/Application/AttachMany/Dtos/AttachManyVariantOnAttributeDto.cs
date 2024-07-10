using Bigpods.Monolith.Modules.VariantsOnAttributes.Domain.AttachMany.Dtos;

namespace Bigpods.Monolith.Modules.VariantsOnAttributes.Application.AttachMany.Dtos;

public sealed record AttachManyVariantOnAttributeDto(Guid ProductId)
    : IAttachManyVariantOnAttributeDto;

public sealed record CreateOneVariantOnAttributeDto(
    Guid Id,
    string CreatedAtTimezone,
    Guid CreatedBy,
    Guid AttributeId,
    Guid VariantId
) : ICreateOneVariantOnAttributeDto;
