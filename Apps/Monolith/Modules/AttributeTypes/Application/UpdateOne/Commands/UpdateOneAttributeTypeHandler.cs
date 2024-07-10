using AutoMapper;
using Bigpods.Monolith.Modules.AttributeTypes.Domain.Common.Aggregates;
using Bigpods.Monolith.Modules.AttributeTypes.Domain.UpdateOne.Services;
using Bigpods.Monolith.Modules.Shared.Domain.Database;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;
using MediatR;

namespace Bigpods.Monolith.Modules.AttributeTypes.Application.UpdateOne.Commands;

public sealed class UpdateOneAttributeTypeHandler(
    [Service] IUpdateOneAttributeTypeService updateOneAttributeTypeService,
    [Service] IUnitOfWork unitOfWork,
    [Service] IMapper mapper
) : IRequestHandler<UpdateOneAttributeTypeCommand, AttributeTypeModel>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IUpdateOneAttributeTypeService _updateOneAttributeTypeService =
        updateOneAttributeTypeService;

    public async Task<AttributeTypeModel> Handle(
        UpdateOneAttributeTypeCommand command,
        CancellationToken cancellationToken
    )
    {
        var fetchResponse = await _updateOneAttributeTypeService.ExecuteAsync(
            command: command,
            cancellationToken: cancellationToken
        );

        var attributeTypesRepository = _unitOfWork.GetRepository<AttributeTypeModel>();

        var aggregateRoot = AttributeTypeAggregateRoot.UpdateOne(
            command: command,
            data: fetchResponse
        );

        var attributeTypeModel = _mapper.Map<AttributeTypeModel>(source: aggregateRoot);

        attributeTypesRepository.UpdateOne(entity: attributeTypeModel);

        await _unitOfWork.CompleteAsync(cancellationToken: cancellationToken);

        return attributeTypeModel;
    }
}
