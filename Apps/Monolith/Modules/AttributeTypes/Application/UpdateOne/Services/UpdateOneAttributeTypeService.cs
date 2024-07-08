using Bigpods.Monolith.Modules.AttributeTypes.Domain.UpdateOne.Commands;
using Bigpods.Monolith.Modules.AttributeTypes.Domain.UpdateOne.Services;
using Bigpods.Monolith.Modules.Shared.Domain.Database;
using Bigpods.Monolith.Modules.Shared.Domain.Models;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;

namespace Bigpods.Monolith.Modules.AttributeTypes.Application.UpdateOne.Services;

public sealed class UpdateOneAttributeTypeService(
    [Service] IUnitOfWork unitOfWork
) : IUpdateOneAttributeTypeService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<IUpdateOneAttributeTypeServiceResponse> ExecuteAsync(
        IUpdateOneAttributeTypeCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var attributeTypesRepository = _unitOfWork.GetRepository<AttributeTypeModel>();

        var attributeTypeFoundById = await attributeTypesRepository.FindOneAsync(
            filter: x => x.Id == command.AttributeTypeDto.Id,
            cancellationToken: cancellationToken
        );
        var attributeTypeFoundByName = await attributeTypesRepository.FindOneAsync(
            filter: x => x.Name == command.AttributeTypeDto.Name,
            cancellationToken: cancellationToken
        );

        return new UpdateOneAttributeTypeServiceResponse(
            AttributeTypeFoundById: attributeTypeFoundById,
            AttributeTypeFoundByName: attributeTypeFoundByName
        );
    }
}

public sealed record UpdateOneAttributeTypeServiceResponse(
    IAttributeTypeModel? AttributeTypeFoundById,
    IAttributeTypeModel? AttributeTypeFoundByName
) : IUpdateOneAttributeTypeServiceResponse;