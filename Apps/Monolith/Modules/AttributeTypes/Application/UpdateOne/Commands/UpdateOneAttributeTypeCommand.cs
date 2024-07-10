using Bigpods.Monolith.Modules.AttributeTypes.Domain.UpdateOne.Commands;
using Bigpods.Monolith.Modules.AttributeTypes.Domain.UpdateOne.Dtos;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;
using MediatR;

namespace Bigpods.Monolith.Modules.AttributeTypes.Application.UpdateOne.Commands;

public sealed record UpdateOneAttributeTypeCommand(IUpdateOneAttributeTypeDto AttributeTypeDto)
    : IUpdateOneAttributeTypeCommand,
        IRequest<AttributeTypeModel>;
