using Bigpods.Monolith.Modules.Products.Domain.CreateOne.Commands;
using Bigpods.Monolith.Modules.Products.Domain.CreateOne.Services;
using Bigpods.Monolith.Modules.Shared.Domain.Database;
using Bigpods.Monolith.Modules.Shared.Domain.Models;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;

namespace Bigpods.Monolith.Modules.Products.Application.CreateOne.Services;

public sealed class CreateOneProductService([Service] IUnitOfWork unitOfWork)
    : ICreateOneProductService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ICreateOneProductServiceResponse> ExecuteAsync(
        ICreateOneProductCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var productsRepository = _unitOfWork.GetRepository<ProductModel>();
        var variantsRepository = _unitOfWork.GetRepository<VariantModel>();
        var variantsOnAttributesRepository = _unitOfWork.GetRepository<VariantOnAttributeModel>();
        var attributesRepository = _unitOfWork.GetRepository<AttributeModel>();

        var productFoundById = await productsRepository.FindOneAsync(
            filter: x => x.Id == command.ProductDto.Id,
            cancellationToken: cancellationToken
        );
        var variantsFoundById = await variantsRepository.FindManyAsync(
            filter: x => command.VariantDtos.Select(dto => dto.Id).Contains(x.Id),
            cancellationToken: cancellationToken
        );
        var variantsOnAttributesFoundById = await variantsOnAttributesRepository.FindManyAsync(
            filter: x => command.VariantOnAttributeDtos.Select(dto => dto.Id).Contains(x.Id),
            cancellationToken: cancellationToken
        );
        var attributesFoundById = await attributesRepository.FindManyAsync(
            filter: x =>
                command.VariantOnAttributeDtos.Select(dto => dto.AttributeId).Contains(x.Id),
            cancellationToken: cancellationToken
        );

        return new CreateOneProductServiceResponse(
            ProductFoundById: productFoundById,
            VariantsFoundById: variantsFoundById.ToArray(),
            VariantsOnAttributesFoundById: variantsOnAttributesFoundById.ToArray(),
            AttributesFoundById: attributesFoundById.ToArray()
        );
    }
}

public sealed record CreateOneProductServiceResponse(
    IProductModel? ProductFoundById,
    IVariantModel[] VariantsFoundById,
    IVariantOnAttributeModel[] VariantsOnAttributesFoundById,
    IAttributeModel[] AttributesFoundById
) : ICreateOneProductServiceResponse;
