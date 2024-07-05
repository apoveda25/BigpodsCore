using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;
using Bigpods.Monolith.Modules.Warehouses.Domain.UpdateOne.Commands;
using Bigpods.Monolith.Modules.Warehouses.Domain.UpdateOne.Dtos;

using MediatR;

namespace Bigpods.Monolith.Modules.Warehouses.Application.UpdateOne.Commands;

public sealed record UpdateOneWarehouseCommand(
    IUpdateOneWarehouseDto WarehouseDto
) : IUpdateOneWarehouseCommand, IRequest<WarehouseModel>;
