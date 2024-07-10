using Bigpods.Monolith.Modules.Products.Domain.UpdateOne.Commands;
using Bigpods.Monolith.Modules.Products.Domain.UpdateOne.Services;
using Bigpods.Monolith.Modules.Shared.Domain.Database;
using Bigpods.Monolith.Modules.Shared.Domain.Models;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;

namespace Bigpods.Monolith.Modules.Products.Application.UpdateOne.Services;

public sealed class UpdateOneProductService([Service] IUnitOfWork unitOfWork)
    : IUpdateOneProductService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<IUpdateOneProductServiceResponse> ExecuteAsync(
        IUpdateOneProductCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var productsRepository = _unitOfWork.GetRepository<ProductModel>();

        var productFoundById = await productsRepository.FindOneAsync(
            filter: x => x.Id == command.ProductDto.Id,
            cancellationToken: cancellationToken
        );

        return new UpdateOneProductServiceResponse(ProductFoundById: productFoundById);
    }
}

public record UpdateOneProductServiceResponse(IProductModel? ProductFoundById)
    : IUpdateOneProductServiceResponse;
