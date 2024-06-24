using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;
using Bigpods.Monolith.Modules.Products.Domain.CreateOne.Commands;
using Bigpods.Monolith.Modules.Products.Domain.CreateOne.Dtos;

using MediatR;

namespace Bigpods.Monolith.Modules.Products.Application.CreateOne.Commands;

public record CreateOneProductCommand(
    ICreateOneProductDto ProductDto,
    ICreateOneVariantDto[] VariantDtos,
    ICreateOneVariantOnAttributeDto[] VariantOnAttributeDtos
) : ICreateOneProductCommand, IRequest<ProductModel>;
