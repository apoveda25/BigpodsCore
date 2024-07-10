using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;
using Bigpods.Monolith.Modules.Variants.Domain.UpdateOne.Commands;
using Bigpods.Monolith.Modules.Variants.Domain.UpdateOne.Dtos;
using MediatR;

namespace Bigpods.Monolith.Modules.Variants.Application.UpdateOne.Commands;

public sealed record UpdateOneVariantCommand(IUpdateOneVariantDto VariantDto)
    : IUpdateOneVariantCommand,
        IRequest<VariantModel>;
