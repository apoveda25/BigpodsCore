using AutoMapper;
using Bigpods.Monolith.Modules.Shared.Domain.Database;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;
using Bigpods.Monolith.Modules.VariantsOnAttributes.Domain.Common.Aggregates;
using Bigpods.Monolith.Modules.VariantsOnAttributes.Domain.DettachMany.Services;
using MediatR;

namespace Bigpods.Monolith.Modules.VariantsOnAttributes.Application.DettachMany.Commands;

public sealed class DettachManyVariantOnAttributeHandler(
    [Service] IDettachManyVariantOnAttributeService attachManyVariantOnAttributeService,
    [Service] IUnitOfWork unitOfWork,
    [Service] IMapper mapper
) : IRequestHandler<DettachManyVariantOnAttributeCommand, VariantOnAttributeModel[]>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IDettachManyVariantOnAttributeService _attachManyVariantOnAttributeService =
        attachManyVariantOnAttributeService;

    public async Task<VariantOnAttributeModel[]> Handle(
        DettachManyVariantOnAttributeCommand command,
        CancellationToken cancellationToken
    )
    {
        var fetchResponse = await _attachManyVariantOnAttributeService.ExecuteAsync(
            command: command,
            cancellationToken: cancellationToken
        );

        var variantsOnAttributesRepository = _unitOfWork.GetRepository<VariantOnAttributeModel>();

        var aggregateRoot = ProductAggregateRoot.DettachManyVariantsOnAttributes(
            command: command,
            data: fetchResponse
        );

        var variantOnAttributeModels = _mapper.Map<VariantOnAttributeModel[]>(
            source: aggregateRoot.Variants.AsParallel().SelectMany(x => x.VariantsOnAttributes)
        );

        variantsOnAttributesRepository.DeleteMany(entities: variantOnAttributeModels);

        await _unitOfWork.CompleteAsync(cancellationToken: cancellationToken);

        return variantOnAttributeModels;
    }
}
