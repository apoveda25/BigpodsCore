using Bigpods.Monolith.Modules.AttributeTypes.Domain.CreateOne.Commands;
using Bigpods.Monolith.Modules.AttributeTypes.Domain.CreateOne.Dtos;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;
using MediatR;

namespace Bigpods.Monolith.Modules.AttributeTypes.Application.CreateOne.Commands;

public sealed record CreateOneAttributeTypeCommand(ICreateOneAttributeTypeDto AttributeTypeDto)
    : ICreateOneAttributeTypeCommand,
        IRequest<AttributeTypeModel>;
