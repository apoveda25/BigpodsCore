using AutoMapper;
using Bigpods.Monolith.Modules.Attributes.Domain.Common.Aggregates;
using Bigpods.Monolith.Modules.Attributes.Domain.CreateOne.Services;
using Bigpods.Monolith.Modules.Shared.Domain.Database;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;
using MediatR;

namespace Bigpods.Monolith.Modules.Attributes.Application.CreateOne.Commands;

public sealed class CreateOneAttributeHandler(
    [Service] ICreateOneAttributeService createOneAttributeService,
    [Service] IUnitOfWork unitOfWork,
    [Service] IMapper mapper
) : IRequestHandler<CreateOneAttributeCommand, AttributeModel>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly ICreateOneAttributeService _createOneAttributeService =
        createOneAttributeService;

    public async Task<AttributeModel> Handle(
        CreateOneAttributeCommand command,
        CancellationToken cancellationToken
    )
    {
        var fetchResponse = await _createOneAttributeService.ExecuteAsync(
            command: command,
            cancellationToken: cancellationToken
        );

        var attributesRepository = _unitOfWork.GetRepository<AttributeModel>();

        var aggregateRoot = AttributeTypeAggregateRoot.CreateOneAttribute(
            command: command,
            data: fetchResponse
        );

        var attributeModel = _mapper.Map<AttributeModel>(
            source: aggregateRoot.Attributes.Find(x => x.Id == command.AttributeDto.Id)
        );

        await attributesRepository.CreateOneAsync(
            entity: attributeModel,
            cancellationToken: cancellationToken
        );

        await _unitOfWork.CompleteAsync(cancellationToken: cancellationToken);

        return attributeModel;
    }
}
