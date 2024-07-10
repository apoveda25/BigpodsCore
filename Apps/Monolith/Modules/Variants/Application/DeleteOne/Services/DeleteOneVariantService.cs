using Bigpods.Monolith.Modules.Shared.Domain.Database;
using Bigpods.Monolith.Modules.Shared.Domain.Models;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;
using Bigpods.Monolith.Modules.Variants.Domain.DeleteOne.Commands;
using Bigpods.Monolith.Modules.Variants.Domain.DeleteOne.Services;

namespace Bigpods.Monolith.Modules.Variants.Application.DeleteOne.Services;

public sealed class DeleteOneVariantService([Service] IUnitOfWork unitOfWork)
    : IDeleteOneVariantService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<IDeleteOneVariantServiceResponse> ExecuteAsync(
        IDeleteOneVariantCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var productsRepository = _unitOfWork.GetRepository<ProductModel>();
        var variantsRepository = _unitOfWork.GetRepository<VariantModel>();
        var variantsOnAttributesRepository = _unitOfWork.GetRepository<VariantOnAttributeModel>();
        var inventoriesRepository = _unitOfWork.GetRepository<InventoryModel>();

        var variantFoundById = await variantsRepository.FindOneAsync(
            filter: x => x.Id == command.VariantDto.Id,
            cancellationToken: cancellationToken
        );

        var productFoundById = variantFoundById is null
            ? null
            : await productsRepository.FindOneAsync(
                filter: x => x.Id == variantFoundById.ProductId,
                cancellationToken: cancellationToken
            );

        var variantsOnAttributesFoundByVariantId = variantFoundById is null
            ? []
            : await variantsOnAttributesRepository.FindManyAsync(
                filter: x => x.VariantId == variantFoundById.Id,
                cancellationToken: cancellationToken
            );

        var inventoryFoundByVariantId = variantFoundById is null
            ? null
            : await inventoriesRepository.FindOneAsync(
                filter: x => x.VariantId == variantFoundById.Id,
                cancellationToken: cancellationToken
            );

        return new DeleteOneVariantServiceResponse(
            ProductFoundById: productFoundById,
            VariantFoundById: variantFoundById,
            VariantsOnAttributesFoundByVariantId: variantsOnAttributesFoundByVariantId.ToArray(),
            InventoryFoundByVariantId: inventoryFoundByVariantId
        );
    }
}

public sealed record DeleteOneVariantServiceResponse(
    IProductModel? ProductFoundById,
    IVariantModel? VariantFoundById,
    IVariantOnAttributeModel[] VariantsOnAttributesFoundByVariantId,
    IInventoryModel? InventoryFoundByVariantId
) : IDeleteOneVariantServiceResponse;
