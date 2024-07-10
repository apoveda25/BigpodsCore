using AutoMapper;
using Bigpods.Monolith.Modules.Attributes.Domain.Common.Aggregates;
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
    private readonly IDeleteOneAttributeService _deleteOneAttributeService =
        deleteOneAttributeService;

    public async Task<AttributeModel> Handle(
        DeleteOneAttributeCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var fetchResponse = await _deleteOneAttributeService.ExecuteAsync(
            command: command,
            cancellationToken: cancellationToken
        );

        var attributesRepository = _unitOfWork.GetRepository<AttributeModel>();

        var aggregateRoot = AttributeTypeAggregateRoot.DeleteOneAttribute(
            command: command,
            data: fetchResponse
        );

        var attributeModel = _mapper.Map<AttributeModel>(
            source: aggregateRoot.Attributes.FirstOrDefault(x => x.Id == command.AttributeDto.Id)
        );

        attributesRepository.DeleteOne(entity: attributeModel);

        await _unitOfWork.CompleteAsync(cancellationToken: cancellationToken);

        return attributeModel;
    }
}
