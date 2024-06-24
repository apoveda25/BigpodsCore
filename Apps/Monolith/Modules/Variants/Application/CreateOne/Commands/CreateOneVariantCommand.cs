using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;
using Bigpods.Monolith.Modules.Variants.Domain.CreateOne.Commands;
using Bigpods.Monolith.Modules.Variants.Domain.CreateOne.Dtos;

using MediatR;

namespace Bigpods.Monolith.Modules.Variants.Application.CreateOne.Commands;

public sealed record CreateOneVariantCommand(
    ICreateOneVariantDto VariantDto,
    ICreateOneVariantOnAttributeDto[] VariantOnAttributeDtos
) : ICreateOneVariantCommand, IRequest<VariantModel>;
