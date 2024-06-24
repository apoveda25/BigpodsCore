using AutoMapper;

using Bigpods.Monolith.Modules.Shared.Domain.Database;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;
using Bigpods.Monolith.Modules.Variants.Domain.Common.Aggregates;
using Bigpods.Monolith.Modules.Variants.Domain.Common.Entities;
using Bigpods.Monolith.Modules.Variants.Domain.DeleteOne.Services;

using MediatR;

namespace Bigpods.Monolith.Modules.Variants.Application.DeleteOne.Commands;

public sealed class DeleteOneVariantHandler(
    [Service] IDeleteOneVariantService deleteOneVariantService,
    [Service] IUnitOfWork unitOfWork,
    [Service] IMapper mapper
) : IRequestHandler<DeleteOneVariantCommand, VariantModel>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IDeleteOneVariantService _deleteOneVariantService = deleteOneVariantService;

    public async Task<VariantModel> Handle(DeleteOneVariantCommand command, CancellationToken cancellationToken = default)
    {
        var fetchResponse = await _deleteOneVariantService.ExecuteAsync(
            command: command,
            cancellationToken: cancellationToken
        );

        var variantsRepository = _unitOfWork.GetRepository<VariantModel>();
        var variantsOnAttributesRepository = _unitOfWork.GetRepository<VariantOnAttributeModel>();

        var aggregateRoot = ProductAggregateRoot.Build(
            product: fetchResponse.ProductFoundById
        );

        var variantEntity = VariantEntity.DeleteOne(
            variant: command.VariantDto,
            variantFoundById: fetchResponse.VariantFoundById
        );

        var variantOnAttributeEntities = fetchResponse.VariantsOnAttributesFoundByVariantId.Select(
            variantOnAttribute => VariantOnAttributeEntity.DeleteOne(
                variant: command.VariantDto,
                variantOnAttributeFoundByVariantId: variantOnAttribute
            )
        );

        aggregateRoot.AttachVariants(variants: [variantEntity]);
        aggregateRoot.AttachVariantsOnAttributes(variantsOnAttributes: variantOnAttributeEntities.ToArray());

        var variantModel = _mapper.Map<VariantModel>(source: variantEntity);
        var variantOnAttributeModels = _mapper.Map<VariantOnAttributeModel[]>(
            source: variantOnAttributeEntities
        );

        variantsRepository.DeleteOne(entity: variantModel);
        variantsOnAttributesRepository.DeleteMany(entities: variantOnAttributeModels);

        await _unitOfWork.CompleteAsync(cancellationToken);

        return variantModel;
    }
}
