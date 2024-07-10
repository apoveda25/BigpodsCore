using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;
using Bigpods.Monolith.Modules.Warehouses.Domain.DeleteOne.Commands;
using Bigpods.Monolith.Modules.Warehouses.Domain.DeleteOne.Dtos;
using MediatR;

namespace Bigpods.Monolith.Modules.Warehouses.Application.DeleteOne.Commands;

public sealed record DeleteOneWarehouseCommand(IDeleteOneWarehouseDto WarehouseDto)
    : IDeleteOneWarehouseCommand,
        IRequest<WarehouseModel>;
