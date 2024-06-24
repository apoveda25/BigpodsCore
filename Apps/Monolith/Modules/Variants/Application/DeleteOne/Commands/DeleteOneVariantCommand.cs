using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;
using Bigpods.Monolith.Modules.Variants.Domain.DeleteOne.Commands;
using Bigpods.Monolith.Modules.Variants.Domain.DeleteOne.Dtos;

using MediatR;

namespace Bigpods.Monolith.Modules.Variants.Application.DeleteOne.Commands;

public sealed record DeleteOneVariantCommand(
    IDeleteOneVariantDto VariantDto
) : IDeleteOneVariantCommand, IRequest<VariantModel>;
