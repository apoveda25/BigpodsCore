using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;
using Bigpods.Monolith.Modules.VariantsOnAttributes.Domain.AttachMany.Commands;
using Bigpods.Monolith.Modules.VariantsOnAttributes.Domain.AttachMany.Dtos;
using MediatR;

namespace Bigpods.Monolith.Modules.VariantsOnAttributes.Application.AttachMany.Commands;

public sealed record AttachManyVariantOnAttributeCommand(
    IAttachManyVariantOnAttributeDto ProductDto,
    ICreateOneVariantOnAttributeDto[] VariantOnAttributeDtos
) : IAttachManyVariantOnAttributeCommand, IRequest<VariantOnAttributeModel[]>;
