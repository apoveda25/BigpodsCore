using AutoMapper;
using Bigpods.Monolith.Modules.Shared.Domain.Database;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;
using Bigpods.Monolith.Modules.VariantsOnAttributes.Domain.AttachMany.Services;
using Bigpods.Monolith.Modules.VariantsOnAttributes.Domain.Common.Aggregates;
using MediatR;

namespace Bigpods.Monolith.Modules.VariantsOnAttributes.Application.AttachMany.Commands;

public sealed class AttachManyVariantOnAttributeHandler(
    [Service] IAttachManyVariantOnAttributeService attachManyVariantOnAttributeService,
    [Service] IUnitOfWork unitOfWork,
    [Service] IMapper mapper
) : IRequestHandler<AttachManyVariantOnAttributeCommand, VariantOnAttributeModel[]>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IAttachManyVariantOnAttributeService _attachManyVariantOnAttributeService =
        attachManyVariantOnAttributeService;

    public async Task<VariantOnAttributeModel[]> Handle(
        AttachManyVariantOnAttributeCommand command,
        CancellationToken cancellationToken
    )
    {
        var fetchResponse = await _attachManyVariantOnAttributeService.ExecuteAsync(
            command: command,
            cancellationToken: cancellationToken
        );

        var variantsOnAttributesRepository = _unitOfWork.GetRepository<VariantOnAttributeModel>();

        var aggregateRoot = ProductAggregateRoot.AttachManyVariantsOnAttributes(
            command: command,
            data: fetchResponse
        );

        var variantOnAttributeModels = _mapper.Map<VariantOnAttributeModel[]>(
            source: aggregateRoot.Variants.SelectMany(x => x.VariantsOnAttributes)
        );

        await variantsOnAttributesRepository.CreateManyAsync(
            entities: variantOnAttributeModels,
            cancellationToken: cancellationToken
        );

        await _unitOfWork.CompleteAsync(cancellationToken: cancellationToken);

        return variantOnAttributeModels;
    }
}
