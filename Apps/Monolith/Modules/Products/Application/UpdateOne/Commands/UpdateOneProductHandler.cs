using AutoMapper;
using Bigpods.Monolith.Modules.Products.Domain.Common.Aggregates;
using Bigpods.Monolith.Modules.Products.Domain.UpdateOne.Services;
using Bigpods.Monolith.Modules.Shared.Domain.Database;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;
using MediatR;

namespace Bigpods.Monolith.Modules.Products.Application.UpdateOne.Commands;

public sealed class UpdateOneProductHandler(
    [Service] IUpdateOneProductService updateOneProductService,
    [Service] IUnitOfWork unitOfWork,
    [Service] IMapper mapper
) : IRequestHandler<UpdateOneProductCommand, ProductModel>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IUpdateOneProductService _updateOneProductService = updateOneProductService;

    public async Task<ProductModel> Handle(
        UpdateOneProductCommand command,
        CancellationToken cancellationToken
    )
    {
        var fetchResponse = await _updateOneProductService.ExecuteAsync(
            command: command,
            cancellationToken: cancellationToken
        );

        var productsRepository = _unitOfWork.GetRepository<ProductModel>();

        var aggregateRoot = ProductAggregateRoot.UpdateOne(command: command, data: fetchResponse);

        var productModel = _mapper.Map<ProductModel>(source: aggregateRoot);

        productsRepository.UpdateOne(entity: productModel);

        await _unitOfWork.CompleteAsync(cancellationToken: cancellationToken);

        return productModel;
    }
}
