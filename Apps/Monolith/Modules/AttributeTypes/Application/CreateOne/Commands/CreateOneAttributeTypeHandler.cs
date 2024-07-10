using AutoMapper;
using Bigpods.Monolith.Modules.AttributeTypes.Domain.Common.Aggregates;
using Bigpods.Monolith.Modules.AttributeTypes.Domain.CreateOne.Services;
using Bigpods.Monolith.Modules.Shared.Domain.Database;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;
using MediatR;

namespace Bigpods.Monolith.Modules.AttributeTypes.Application.CreateOne.Commands;

public sealed class CreateOneAttributeTypeHandler(
    [Service] ICreateOneAttributeTypeService createOneAttributeTypeService,
    [Service] IUnitOfWork unitOfWork,
    [Service] IMapper mapper
) : IRequestHandler<CreateOneAttributeTypeCommand, AttributeTypeModel>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly ICreateOneAttributeTypeService _createOneAttributeTypeService =
        createOneAttributeTypeService;

    public async Task<AttributeTypeModel> Handle(
        CreateOneAttributeTypeCommand command,
        CancellationToken token = default
    )
    {
        var fetchResponse = await _createOneAttributeTypeService.ExecuteAsync(
            command: command,
            cancellationToken: token
        );

        var attributeTypesRepository = _unitOfWork.GetRepository<AttributeTypeModel>();

        var aggregateRoot = AttributeTypeAggregateRoot.CreateOne(
            command: command,
            data: fetchResponse
        );

        var attributeTypeModel = _mapper.Map<AttributeTypeModel>(source: aggregateRoot);

        await attributeTypesRepository.CreateOneAsync(
            entity: attributeTypeModel,
            cancellationToken: token
        );

        await _unitOfWork.CompleteAsync(cancellationToken: token);

        return attributeTypeModel;
    }
}
