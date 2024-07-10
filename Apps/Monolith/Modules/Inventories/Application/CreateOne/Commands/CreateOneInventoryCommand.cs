using Bigpods.Monolith.Modules.Inventories.Domain.CreateOne.Commands;
using Bigpods.Monolith.Modules.Inventories.Domain.CreateOne.Dtos;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;
using MediatR;

namespace Bigpods.Monolith.Modules.Inventories.Application.CreateOne.Commands;

public sealed record CreateOneInventoryCommand(ICreateOneInventoryDto InventoryDto)
    : ICreateOneInventoryCommand,
        IRequest<InventoryModel>;
