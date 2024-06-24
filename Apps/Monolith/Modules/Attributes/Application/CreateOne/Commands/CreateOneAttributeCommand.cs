using Bigpods.Monolith.Modules.Attributes.Domain.CreateOne.Commands;
using Bigpods.Monolith.Modules.Attributes.Domain.CreateOne.Dtos;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;

using MediatR;

namespace Bigpods.Monolith.Modules.Attributes.Application.CreateOne.Commands;

public sealed record CreateOneAttributeCommand(
    ICreateOneAttributeDto AttributeDto
) : ICreateOneAttributeCommand, IRequest<AttributeModel>;