using Bigpods.Monolith.Modules.AttributeTypes.Domain.CreateOne.Commands;
using Bigpods.Monolith.Modules.AttributeTypes.Domain.CreateOne.Services;
using Bigpods.Monolith.Modules.Shared.Domain.Database;
using Bigpods.Monolith.Modules.Shared.Domain.Models;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;

namespace Bigpods.Monolith.Modules.AttributeTypes.Application.CreateOne.Services;

public sealed class CreateOneAttributeTypeService([Service] IUnitOfWork unitOfWork)
    : ICreateOneAttributeTypeService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ICreateOneAttributeTypeServiceResponse> ExecuteAsync(
        ICreateOneAttributeTypeCommand command,
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

        return new CreateOneAttributeTypeServiceResponse(
            AttributeTypeFoundById: attributeTypeFoundById,
            AttributeTypeFoundByName: attributeTypeFoundByName
        );
    }
}

public sealed record CreateOneAttributeTypeServiceResponse(
    IAttributeTypeModel? AttributeTypeFoundById,
    IAttributeTypeModel? AttributeTypeFoundByName
) : ICreateOneAttributeTypeServiceResponse;
