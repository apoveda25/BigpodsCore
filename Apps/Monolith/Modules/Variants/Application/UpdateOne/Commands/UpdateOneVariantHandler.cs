using AutoMapper;
using Bigpods.Monolith.Modules.Shared.Domain.Database;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;
using Bigpods.Monolith.Modules.Variants.Domain.Common.Aggregates;
using Bigpods.Monolith.Modules.Variants.Domain.UpdateOne.Services;
using MediatR;

namespace Bigpods.Monolith.Modules.Variants.Application.UpdateOne.Commands;

public sealed class UpdateOneVariantHandler(
    [Service] IUpdateOneVariantService updateOneVariantService,
    [Service] IUnitOfWork unitOfWork,
    [Service] IMapper mapper
) : IRequestHandler<UpdateOneVariantCommand, VariantModel>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IUpdateOneVariantService _updateOneVariantService = updateOneVariantService;

    public async Task<VariantModel> Handle(
        UpdateOneVariantCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var fetchResponse = await _updateOneVariantService.ExecuteAsync(
            command: command,
            cancellationToken: cancellationToken
        );

        var variantsRepository = _unitOfWork.GetRepository<VariantModel>();

        var aggregateRoot = ProductAggregateRoot.UpdateOneVariant(
            command: command,
            data: fetchResponse
        );

        var variantModel = _mapper.Map<VariantModel>(
            source: aggregateRoot.Variants.FirstOrDefault(x => x.Id == command.VariantDto.Id)
        );

        variantsRepository.UpdateOne(entity: variantModel);

        await _unitOfWork.CompleteAsync(cancellationToken);

        return variantModel;
    }
}
