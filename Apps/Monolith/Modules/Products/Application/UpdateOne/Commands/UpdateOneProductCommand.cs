using Bigpods.Monolith.Modules.Products.Domain.UpdateOne.Commands;
using Bigpods.Monolith.Modules.Products.Domain.UpdateOne.Dtos;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;
using MediatR;

namespace Bigpods.Monolith.Modules.Products.Application.UpdateOne.Commands;

public record UpdateOneProductCommand(IUpdateOneProductDto ProductDto)
    : IUpdateOneProductCommand,
        IRequest<ProductModel>;
