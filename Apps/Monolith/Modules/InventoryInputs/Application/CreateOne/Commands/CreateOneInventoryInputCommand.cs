using Bigpods.Monolith.Modules.InventoryInputs.Domain.CreateOne.Commands;
using Bigpods.Monolith.Modules.InventoryInputs.Domain.CreateOne.Dtos;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;

using MediatR;

namespace Bigpods.Monolith.Modules.InventoryInputs.Application.CreateOne.Commands;

public sealed record CreateOneInventoryInputCommand(
    ICreateOneInventoryInputDto InventoryInputDto
) : ICreateOneInventoryInputCommand, IRequest<InventoryInputModel>;