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

        var variantsRepository = _unitOfWork.GetRepository<VariantModel>();
        var variantsOnAttributesRepository = _unitOfWork.GetRepository<VariantOnAttributeModel>();

        var aggregateRoot = ProductAggregateRoot.CreateOneVariant(
            variant: command.VariantDto,
            variantsOnAttributes: command.VariantOnAttributeDtos,
            data: fetchResponse
        );

        var variantModel = _mapper.Map<VariantModel>(
            source: aggregateRoot.Variants.FirstOrDefault(x => x.Id == command.VariantDto.Id)
        );
        var variantOnAttributeModels = _mapper.Map<VariantOnAttributeModel[]>(
            source: aggregateRoot.Variants
                .SelectMany(x => x.VariantsOnAttributes)
                .Where(x => command.VariantOnAttributeDtos.Any(dto => dto.Id == x.Id))
        );

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
