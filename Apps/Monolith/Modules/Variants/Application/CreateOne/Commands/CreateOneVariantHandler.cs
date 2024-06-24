using AutoMapper;

using Bigpods.Monolith.Modules.Shared.Domain.Database;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;
using Bigpods.Monolith.Modules.Variants.Domain.Common.Aggregates;
using Bigpods.Monolith.Modules.Variants.Domain.Common.Entities;
using Bigpods.Monolith.Modules.Variants.Domain.CreateOne.Services;

using MediatR;

namespace Bigpods.Monolith.Modules.Variants.Application.CreateOne.Commands;

public sealed class CreateOneVariantHandler(
    [Service] ICreateOneVariantService createOneVariantService,
    [Service] IUnitOfWork unitOfWork,
    [Service] IMapper mapper
) : IRequestHandler<CreateOneVariantCommand, VariantModel>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly ICreateOneVariantService _createOneVariantService = createOneVariantService;

    public async Task<VariantModel> Handle(CreateOneVariantCommand command, CancellationToken token)
    {
        var fetchResponse = await _createOneVariantService.ExecuteAsync(command: command, cancellationToken: token);

        var productsRepository = _unitOfWork.GetRepository<ProductModel>();
        var variantsRepository = _unitOfWork.GetRepository<VariantModel>();
        var variantsOnAttributesRepository = _unitOfWork.GetRepository<VariantOnAttributeModel>();

        var aggregateRoot = ProductAggregateRoot.Build(
            product: fetchResponse.ProductFoundById
        );

        var variantEntitiesExisting = fetchResponse.VariantsFoundByProductId.Select(
            VariantEntity.Build
        );

        var variantEntityNotExisting = VariantEntity.CreateOne(
            variant: command.VariantDto,
            variantFoundById: fetchResponse.VariantFoundById
        );

        var variantOnAttributeEntitiesExisting = fetchResponse.VariantsOnAttributesFoundByVariantId.Select(
            VariantOnAttributeEntity.Build
        );

        var variantOnAttributeEntitiesNotExisting = command.VariantOnAttributeDtos
            .Select(dto => VariantOnAttributeEntity.CreateOne(
                variantOnAttribute: dto,
                variantsOnAttributesFoundById: fetchResponse.VariantsOnAttributesFoundById,
                attributesFoundById: fetchResponse.AttributesFoundById
            )
        );

        aggregateRoot.AttachVariants(
            variants: variantEntitiesExisting.ToArray()
        );

        aggregateRoot.AttachVariants(
            variants: [variantEntityNotExisting]
        );

        aggregateRoot.AttachVariantsOnAttributes(
            variantsOnAttributes: variantOnAttributeEntitiesExisting.ToArray()
        );

        aggregateRoot.AttachVariantsOnAttributes(
            variantsOnAttributes: variantOnAttributeEntitiesNotExisting.ToArray()
        );

        var productModel = _mapper.Map<ProductModel>(
            source: aggregateRoot
        );
        var variantModel = _mapper.Map<VariantModel>(
            source: variantEntityNotExisting
        );
        var variantOnAttributeModels = _mapper.Map<VariantOnAttributeModel[]>(
            source: variantOnAttributeEntitiesNotExisting
        );

        productsRepository.UpdateOne(entity: productModel);
        await variantsRepository.CreateOneAsync(
            entity: variantModel,
            cancellationToken: token
        );
        await variantsOnAttributesRepository.CreateManyAsync(
            entities: variantOnAttributeModels,
            cancellationToken: token
        );

        await _unitOfWork.CompleteAsync(cancellationToken: token);

        return variantModel;
    }
}
