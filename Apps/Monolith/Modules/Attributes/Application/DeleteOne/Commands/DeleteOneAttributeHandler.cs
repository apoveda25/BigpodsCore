using AutoMapper;

using Bigpods.Monolith.Modules.Attributes.Domain.Common.Aggregates;
using Bigpods.Monolith.Modules.Attributes.Domain.Common.Entities;

using Bigpods.Monolith.Modules.Attributes.Domain.DeleteOne.Services;
using Bigpods.Monolith.Modules.Shared.Domain.Database;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;

using MediatR;

namespace Bigpods.Monolith.Modules.Attributes.Application.DeleteOne.Commands;

public sealed class DeleteOneAttributeHandler(
    [Service] IDeleteOneAttributeService deleteOneAttributeService,
    [Service] IUnitOfWork unitOfWork,
    [Service] IMapper mapper
) : IRequestHandler<DeleteOneAttributeCommand, AttributeModel>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IDeleteOneAttributeService _deleteOneAttributeService = deleteOneAttributeService;

    public async Task<AttributeModel> Handle(
        DeleteOneAttributeCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var fetchResponse = await _deleteOneAttributeService.ExecuteAsync(command: command, cancellationToken: cancellationToken);

        var attributesRepository = _unitOfWork.GetRepository<AttributeModel>();

        var attributeTypeEntity = AttributeTypeEntity.Build(
            attributeType: fetchResponse.AttributeTypeFoundById
        );

        var aggregateRoot = AttributeAggregateRoot.DeleteOne(
            attribute: command.AttributeDto,
            attributeType: attributeTypeEntity,
            attributeFoundById: fetchResponse.AttributeFoundById
        );

        var attributeModel = _mapper.Map<AttributeModel>(source: aggregateRoot);

        attributesRepository.DeleteOne(entity: attributeModel);

        await _unitOfWork.CompleteAsync(cancellationToken: cancellationToken);

        return attributeModel;
    }
}
