using Bigpods.Monolith.Modules.Shared.Domain.Database;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;
using Bigpods.Monolith.Modules.Shared.Domain.Models;
using Bigpods.Monolith.Modules.Variants.Domain.DeleteOne.Commands;
using Bigpods.Monolith.Modules.Variants.Domain.DeleteOne.Services;

namespace Bigpods.Monolith.Modules.Variants.Application.DeleteOne.Services;

public sealed class DeleteOneVariantService(
    [Service] IUnitOfWork unitOfWork
) : IDeleteOneVariantService
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

        var variantFoundById = await variantsRepository.FindOneAsync(
            filter: x => x.Id == command.VariantDto.Id && x.IsDeleted == false,
            cancellationToken: cancellationToken
        );

        var productFoundById = variantFoundById is null ? null : await productsRepository.FindOneAsync(
            filter: x => x.Id == variantFoundById.ProductId && x.IsDeleted == false,
            cancellationToken: cancellationToken
        );

        var variantsOnAttributesFoundByVariantId = variantFoundById is null ? [] : await variantsOnAttributesRepository.FindManyAsync(
            filter: x => x.VariantId == variantFoundById.Id && x.IsDeleted == false,
            cancellationToken: cancellationToken
        );

        return new DeleteOneVariantServiceResponse(
            ProductFoundById: productFoundById,
            VariantFoundById: variantFoundById,
            VariantsOnAttributesFoundByVariantId: variantsOnAttributesFoundByVariantId.ToArray()
        );
    }
}

public sealed record DeleteOneVariantServiceResponse(
    IProductModel? ProductFoundById,
    IVariantModel? VariantFoundById,
    IVariantOnAttributeModel[] VariantsOnAttributesFoundByVariantId
) : IDeleteOneVariantServiceResponse;