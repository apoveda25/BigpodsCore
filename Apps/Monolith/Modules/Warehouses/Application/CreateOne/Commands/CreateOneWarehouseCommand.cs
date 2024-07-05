using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;
using Bigpods.Monolith.Modules.Warehouses.Domain.CreateOne.Commands;
using Bigpods.Monolith.Modules.Warehouses.Domain.CreateOne.Dtos;

using MediatR;

namespace Bigpods.Monolith.Modules.Warehouses.Application.CreateOne.Commands;

public sealed record CreateOneWarehouseCommand(
    ICreateOneWarehouseDto WarehouseDto
) : ICreateOneWarehouseCommand, IRequest<WarehouseModel>;
