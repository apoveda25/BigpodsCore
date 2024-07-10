using AutoMapper;
using Bigpods.Monolith.Modules.Shared.Domain.Database;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;
using Bigpods.Monolith.Modules.Variants.Domain.Common.Aggregates;
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

    public async Task<VariantModel> Handle(
        DeleteOneVariantCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var fetchResponse = await _deleteOneVariantService.ExecuteAsync(
            command: command,
            cancellationToken: cancellationToken
        );

        var variantsRepository = _unitOfWork.GetRepository<VariantModel>();
        var variantsOnAttributesRepository = _unitOfWork.GetRepository<VariantOnAttributeModel>();

        var aggregateRoot = ProductAggregateRoot.DeleteOneVariant(
            command: command,
            data: fetchResponse
        );

        var variantEntity = aggregateRoot.Variants.FirstOrDefault(x =>
            x.Id == command.VariantDto.Id
        );

        var variantModel = _mapper.Map<VariantModel>(source: variantEntity);
        var variantOnAttributeModels = _mapper.Map<VariantOnAttributeModel[]>(
            source: variantEntity?.VariantsOnAttributes ?? []
        );

        variantsRepository.DeleteOne(entity: variantModel);
        variantsOnAttributesRepository.DeleteMany(entities: variantOnAttributeModels);

        await _unitOfWork.CompleteAsync(cancellationToken);

        return variantModel;
    }
}
