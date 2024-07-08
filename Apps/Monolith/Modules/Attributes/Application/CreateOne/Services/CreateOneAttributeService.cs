using Bigpods.Monolith.Modules.Attributes.Domain.CreateOne.Commands;
using Bigpods.Monolith.Modules.Attributes.Domain.CreateOne.Services;
using Bigpods.Monolith.Modules.Shared.Domain.Database;
using Bigpods.Monolith.Modules.Shared.Domain.Models;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;

namespace Bigpods.Monolith.Modules.Attributes.Application.CreateOne.Services;

public sealed class CreateOneAttributeService(
    [Service] IUnitOfWork unitOfWork
) : ICreateOneAttributeService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ICreateOneAttributeServiceResponse> ExecuteAsync(
        ICreateOneAttributeCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var attributesRepository = _unitOfWork.GetRepository<AttributeModel>();
        var attributesClassesRepository = _unitOfWork.GetRepository<AttributeTypeModel>();

        var attributeFoundById = await attributesRepository.FindOneAsync(
            filter: x => x.Id == command.AttributeDto.Id,
            cancellationToken: cancellationToken
        );
        var attributeFoundByValueMeasuringUnitAttributeTypeId = await attributesRepository.FindOneAsync(
            filter: x => x.Value == command.AttributeDto.Value && x.MeasuringUnit == command.AttributeDto.MeasuringUnit && x.AttributeTypeId == command.AttributeDto.AttributeTypeId,
            cancellationToken: cancellationToken
        );
        var attributeTypeFoundById = await attributesClassesRepository.FindOneAsync(
            filter: x => x.Id == command.AttributeDto.AttributeTypeId,
            cancellationToken: cancellationToken
        );

        return new CreateOneAttributeServiceResponse(
            AttributeFoundById: attributeFoundById,
            AttributeFoundByValueMeasuringUnitAttributeTypeId: attributeFoundByValueMeasuringUnitAttributeTypeId,
            AttributeTypeFoundById: attributeTypeFoundById
        );
    }
}

public sealed record CreateOneAttributeServiceResponse(
    IAttributeModel? AttributeFoundById,
    IAttributeModel? AttributeFoundByValueMeasuringUnitAttributeTypeId,
    IAttributeTypeModel? AttributeTypeFoundById
) : ICreateOneAttributeServiceResponse;