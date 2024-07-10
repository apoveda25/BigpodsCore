using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;
using Bigpods.Monolith.Modules.VariantsOnAttributes.Domain.DettachMany.Commands;
using Bigpods.Monolith.Modules.VariantsOnAttributes.Domain.DettachMany.Dtos;
using MediatR;

namespace Bigpods.Monolith.Modules.VariantsOnAttributes.Application.DettachMany.Commands;

public sealed record DettachManyVariantOnAttributeCommand(
    IDettachManyVariantOnAttributeDto ProductDto,
    IDeleteOneVariantOnAttributeDto[] VariantOnAttributeDtos
) : IDettachManyVariantOnAttributeCommand, IRequest<VariantOnAttributeModel[]>;
