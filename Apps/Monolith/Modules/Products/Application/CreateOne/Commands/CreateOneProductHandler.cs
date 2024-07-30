using AutoMapper;
using Bigpods.Monolith.Modules.Products.Domain.Common.Aggregates;
using Bigpods.Monolith.Modules.Products.Domain.CreateOne.Services;
using Bigpods.Monolith.Modules.Shared.Domain.Database;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;
using MediatR;

namespace Bigpods.Monolith.Modules.Products.Application.CreateOne.Commands;

public sealed class CreateOneProductHandler(
    [Service] ICreateOneProductService createOneProductService,
    [Service] IUnitOfWork unitOfWork,
    [Service] IMapper mapper
) : IRequestHandler<CreateOneProductCommand, ProductModel>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly ICreateOneProductService _createOneProductService = createOneProductService;

    public async Task<ProductModel> Handle(
        CreateOneProductCommand command,
        CancellationToken cancellationToken
    )
    {
        var fetchResponse = await _createOneProductService.ExecuteAsync(
            command: command,
            cancellationToken: cancellationToken
        );

        var productsRepository = _unitOfWork.GetRepository<ProductModel>();
        var variantsRepository = _unitOfWork.GetRepository<VariantModel>();
        var variantsOnAttributesRepository = _unitOfWork.GetRepository<VariantOnAttributeModel>();

        var aggregateRoot = ProductAggregateRoot.CreateOne(command: command, data: fetchResponse);

        var productModel = _mapper.Map<ProductModel>(source: aggregateRoot);
        var variantModels = _mapper.Map<VariantModel[]>(source: aggregateRoot.Variants);
        var variantOnAttributeModels = _mapper.Map<VariantOnAttributeModel[]>(
            source: aggregateRoot.Variants.SelectMany(v => v.VariantsOnAttributes)
        );

        await productsRepository.CreateOneAsync(
            entity: productModel,
            cancellationToken: cancellationToken
        );
        await variantsRepository.CreateManyAsync(
            entities: variantModels,
            cancellationToken: cancellationToken
        );
        await variantsOnAttributesRepository.CreateManyAsync(
            entities: variantOnAttributeModels,
            cancellationToken: cancellationToken
        );

        await _unitOfWork.CompleteAsync(cancellationToken: cancellationToken);

        return productModel;
    }
}
