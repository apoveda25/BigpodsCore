using Bigpods.Monolith.Modules.Inventories.Domain.DeleteOne.Commands;
using Bigpods.Monolith.Modules.Inventories.Domain.DeleteOne.Dtos;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;

using MediatR;

namespace Bigpods.Monolith.Modules.Inventories.Application.DeleteOne.Commands;

public sealed record DeleteOneInventoryCommand(
    IDeleteOneInventoryDto InventoryDto
) : IDeleteOneInventoryCommand, IRequest<InventoryModel>;