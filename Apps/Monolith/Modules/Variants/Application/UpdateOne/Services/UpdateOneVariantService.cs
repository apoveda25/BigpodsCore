using Bigpods.Monolith.Modules.Shared.Domain.Database;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;
using Bigpods.Monolith.Modules.Shared.Domain.Models;
using Bigpods.Monolith.Modules.Variants.Domain.UpdateOne.Commands;
using Bigpods.Monolith.Modules.Variants.Domain.UpdateOne.Services;

namespace Bigpods.Monolith.Modules.Variants.Application.UpdateOne.Services;

public sealed class UpdateOneVariantService(
    [Service] IUnitOfWork unitOfWork
) : IUpdateOneVariantService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<IUpdateOneVariantServiceResponse> ExecuteAsync(
        IUpdateOneVariantCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var productsRepository = _unitOfWork.GetRepository<ProductModel>();
        var variantsRepository = _unitOfWork.GetRepository<VariantModel>();

        var variantFoundById = await variantsRepository.FindOneAsync(
            filter: x => x.Id == command.VariantDto.Id && x.IsDeleted == false,
            cancellationToken: cancellationToken
        );

        var productFoundById = variantFoundById is null ? null : await productsRepository.FindOneAsync(
            filter: x => x.Id == variantFoundById.ProductId && x.IsDeleted == false,
            cancellationToken: cancellationToken
        );

        return new UpdateOneVariantServiceResponse(
            ProductFoundById: productFoundById,
            VariantFoundById: variantFoundById
        );
    }
}

public sealed record UpdateOneVariantServiceResponse(
    IProductModel? ProductFoundById,
    IVariantModel? VariantFoundById
) : IUpdateOneVariantServiceResponse;