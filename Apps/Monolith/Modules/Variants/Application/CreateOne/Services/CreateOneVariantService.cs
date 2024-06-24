using Bigpods.Monolith.Modules.Shared.Domain.Database;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;
using Bigpods.Monolith.Modules.Shared.Domain.Models;
using Bigpods.Monolith.Modules.Variants.Domain.CreateOne.Commands;
using Bigpods.Monolith.Modules.Variants.Domain.CreateOne.Services;

namespace Bigpods.Monolith.Modules.Variants.Application.CreateOne.Services;

public sealed class CreateOneVariantService(
    [Service] IUnitOfWork unitOfWork
) : ICreateOneVariantService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ICreateOneVariantServiceResponse> ExecuteAsync(
        ICreateOneVariantCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var productsRepository = _unitOfWork.GetRepository<ProductModel>();
        var variantsRepository = _unitOfWork.GetRepository<VariantModel>();
        var variantsOnAttributesRepository = _unitOfWork.GetRepository<VariantOnAttributeModel>();
        var attributesRepository = _unitOfWork.GetRepository<AttributeModel>();

        var productFoundById = await productsRepository.FindOneAsync(
            filter: x => x.Id == command.VariantDto.ProductId,
            cancellationToken: cancellationToken
        );
        var variantFoundById = await variantsRepository.FindOneAsync(
            filter: x => x.Id == command.VariantDto.Id,
            cancellationToken: cancellationToken
        );
        var variantsFoundByProductId = await variantsRepository.FindManyAsync(
           filter: x => x.ProductId == command.VariantDto.ProductId && x.IsDeleted == false,
           cancellationToken: cancellationToken
        );
        var variantsOnAttributesFoundById = await variantsOnAttributesRepository.FindManyAsync(
            filter: x => command.VariantOnAttributeDtos.Select(dto => dto.Id).Contains(x.Id),
            cancellationToken: cancellationToken
        );
        var variantsOnAttributesFoundByVariantId = await variantsOnAttributesRepository.FindManyAsync(
            filter: x => variantsFoundByProductId.Select(model => model.Id).Contains(x.VariantId) && x.IsDeleted == false,
            cancellationToken: cancellationToken
        );
        var attributesFoundById = await attributesRepository.FindManyAsync(
            filter: x =>
                command.VariantOnAttributeDtos.Select(dto => dto.AttributeId).Contains(x.Id),
            cancellationToken: cancellationToken
        );

        return new CreateOneVariantServiceResponse(
            ProductFoundById: productFoundById,
            VariantFoundById: variantFoundById,
            VariantsOnAttributesFoundById: variantsOnAttributesFoundById.ToArray(),
            VariantsFoundByProductId: variantsFoundByProductId.ToArray(),
            VariantsOnAttributesFoundByVariantId: variantsOnAttributesFoundByVariantId.ToArray(),
            AttributesFoundById: attributesFoundById.ToArray()
        );
    }
}

public record CreateOneVariantServiceResponse(
    IProductModel? ProductFoundById,
    IVariantModel? VariantFoundById,
    IVariantModel[] VariantsFoundByProductId,
    IVariantOnAttributeModel[] VariantsOnAttributesFoundById,
    IVariantOnAttributeModel[] VariantsOnAttributesFoundByVariantId,
    IAttributeModel[] AttributesFoundById
) : ICreateOneVariantServiceResponse;