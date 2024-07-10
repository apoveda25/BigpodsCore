using Bigpods.Monolith.Modules.Shared.Domain.Database;
using Bigpods.Monolith.Modules.Shared.Domain.Models;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;
using Bigpods.Monolith.Modules.VariantsOnAttributes.Domain.DettachMany.Commands;
using Bigpods.Monolith.Modules.VariantsOnAttributes.Domain.DettachMany.Services;

namespace Bigpods.Monolith.Modules.VariantsOnAttributes.Application.DettachMany.Services;

public sealed class DettachManyVariantOnAttributeService([Service] IUnitOfWork unitOfWork)
    : IDettachManyVariantOnAttributeService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<IDettachManyVariantOnAttributeServiceResponse> ExecuteAsync(
        IDettachManyVariantOnAttributeCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var productsRepository = _unitOfWork.GetRepository<ProductModel>();
        var variantsRepository = _unitOfWork.GetRepository<VariantModel>();
        var variantsOnAttributesRepository = _unitOfWork.GetRepository<VariantOnAttributeModel>();
        var attributesRepository = _unitOfWork.GetRepository<AttributeModel>();

        var productFoundById = await productsRepository.FindOneAsync(
            filter: x => x.Id == command.ProductDto.ProductId,
            cancellationToken: cancellationToken
        );
        var variantsOnAttributesFoundById = await variantsOnAttributesRepository.FindManyAsync(
            filter: x => command.VariantOnAttributeDtos.Select(dto => dto.Id).Contains(x.Id),
            cancellationToken: cancellationToken
        );
        var variantsFoundById = await variantsRepository.FindManyAsync(
            filter: x =>
                variantsOnAttributesFoundById.Select(dto => dto.VariantId).ToArray().Contains(x.Id),
            cancellationToken: cancellationToken
        );

        return new DettachManyVariantOnAttributeServiceResponse(
            ProductFoundById: productFoundById,
            VariantsFoundById: variantsFoundById.ToArray(),
            VariantsOnAttributesFoundById: variantsOnAttributesFoundById.ToArray()
        );
    }
}

public record DettachManyVariantOnAttributeServiceResponse(
    IProductModel? ProductFoundById,
    IVariantModel[] VariantsFoundById,
    IVariantOnAttributeModel[] VariantsOnAttributesFoundById
) : IDettachManyVariantOnAttributeServiceResponse;
