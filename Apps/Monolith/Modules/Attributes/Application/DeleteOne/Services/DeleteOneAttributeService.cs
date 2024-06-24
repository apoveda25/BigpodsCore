using Bigpods.Monolith.Modules.Attributes.Domain.DeleteOne.Commands;
using Bigpods.Monolith.Modules.Attributes.Domain.DeleteOne.Services;
using Bigpods.Monolith.Modules.Shared.Domain.Database;
using Bigpods.Monolith.Modules.Shared.Domain.Models;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;

namespace Bigpods.Monolith.Modules.Attributes.Application.DeleteOne.Services;

public sealed class DeleteOneAttributeService(
    [Service] IUnitOfWork unitOfWork
) : IDeleteOneAttributeService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<IDeleteOneAttributeServiceResponse> ExecuteAsync(
        IDeleteOneAttributeCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var attributesRepository = _unitOfWork.GetRepository<AttributeModel>();
        var attributesClasses = _unitOfWork.GetRepository<AttributeTypeModel>();

        var attributeFoundById = await attributesRepository.FindOneAsync(
            filter: x => x.Id == command.AttributeDto.Id && x.IsDeleted == false,
            cancellationToken: cancellationToken
        );
        var attributeTypeFoundById = attributeFoundById is null ? null : await attributesClasses.FindOneAsync(
            filter: x => x.Id == attributeFoundById.AttributeTypeId && x.IsDeleted == false,
            cancellationToken: cancellationToken
        );

        return new DeleteOneAttributeServiceResponse(
            AttributeFoundById: attributeFoundById,
            AttributeTypeFoundById: attributeTypeFoundById
        );
    }
}

public sealed record DeleteOneAttributeServiceResponse(
    IAttributeModel? AttributeFoundById,
    IAttributeTypeModel? AttributeTypeFoundById
) : IDeleteOneAttributeServiceResponse;