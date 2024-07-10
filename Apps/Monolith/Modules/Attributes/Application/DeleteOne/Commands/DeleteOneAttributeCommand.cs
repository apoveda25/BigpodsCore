using Bigpods.Monolith.Modules.Attributes.Domain.DeleteOne.Commands;
using Bigpods.Monolith.Modules.Attributes.Domain.DeleteOne.Dtos;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;
using MediatR;

namespace Bigpods.Monolith.Modules.Attributes.Application.DeleteOne.Commands;

public sealed record DeleteOneAttributeCommand(IDeleteOneAttributeDto AttributeDto)
    : IDeleteOneAttributeCommand,
        IRequest<AttributeModel>;
