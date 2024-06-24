using AutoMapper;

using Bigpods.Monolith.Modules.Shared.Domain.Database;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;
using Bigpods.Monolith.Modules.Products.Domain.Common.Aggregates;
using Bigpods.Monolith.Modules.Products.Domain.Common.Entities;
using Bigpods.Monolith.Modules.Products.Domain.CreateOne.Services;

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

    public async Task<ProductModel> Handle(CreateOneProductCommand command, CancellationToken token)
    {
        var fetchResponse = await _createOneProductService.ExecuteAsync(command: command, cancellationToken: token);

        var productsRepository = _unitOfWork.GetRepository<ProductModel>();
        var variantsRepository = _unitOfWork.GetRepository<VariantModel>();
        var variantsOnAttributesRepository = _unitOfWork.GetRepository<VariantOnAttributeModel>();

        var aggregateRoot = ProductAggregateRoot.CreateOne(
            product: command.ProductDto,
            productFoundById: fetchResponse.ProductFoundById
        );

        var variantEntities = command.VariantDtos.Select(dto => VariantEntity.CreateOne(
            variant: dto,
            variantsFoundById: fetchResponse.VariantsFoundById
        )).ToArray();

        aggregateRoot.AttachVariants(variants: variantEntities);

        var variantOnAttributeEntities = command.VariantOnAttributeDtos.Select(dto => VariantOnAttributeEntity.CreateOne(
            variantOnAttribute: dto,
            variantsOnAttributesFoundById: fetchResponse.VariantsOnAttributesFoundById,
            attributesFoundById: fetchResponse.AttributesFoundById
        )).ToArray();

        aggregateRoot.AttachVariantsOnAttributes(variantOnAttributeEntities);

        var productModel = _mapper.Map<ProductModel>(source: aggregateRoot);
        var variantModels = _mapper.Map<VariantModel[]>(source: variantEntities);
        var variantOnAttributeModels = _mapper.Map<VariantOnAttributeModel[]>(source: variantOnAttributeEntities);

        await productsRepository.CreateOneAsync(entity: productModel, cancellationToken: token);
        await variantsRepository.CreateManyAsync(entities: variantModels, cancellationToken: token);
        await variantsOnAttributesRepository.CreateManyAsync(entities: variantOnAttributeModels, cancellationToken: token);

        await _unitOfWork.CompleteAsync(cancellationToken: token);

        return productModel;
    }
}
