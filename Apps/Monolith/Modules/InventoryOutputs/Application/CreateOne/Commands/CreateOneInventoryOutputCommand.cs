using Bigpods.Monolith.Modules.InventoryOutputs.Domain.CreateOne.Commands;
using Bigpods.Monolith.Modules.InventoryOutputs.Domain.CreateOne.Dtos;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;
using MediatR;

namespace Bigpods.Monolith.Modules.InventoryOutputs.Application.CreateOne.Commands;

public sealed record CreateOneInventoryOutputCommand(
    ICreateOneInventoryOutputDto InventoryOutputDto
) : ICreateOneInventoryOutputCommand, IRequest<InventoryOutputModel>;
