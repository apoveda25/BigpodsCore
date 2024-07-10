using Bigpods.Monolith.Modules.Shared.Domain.Database;
using Bigpods.Monolith.Modules.Shared.Domain.Models;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;
using Bigpods.Monolith.Modules.VariantsOnAttributes.Domain.AttachMany.Commands;
using Bigpods.Monolith.Modules.VariantsOnAttributes.Domain.AttachMany.Services;

namespace Bigpods.Monolith.Modules.VariantsOnAttributes.Application.AttachMany.Services;

public sealed class AttachManyVariantOnAttributeService([Service] IUnitOfWork unitOfWork)
    : IAttachManyVariantOnAttributeService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<IAttachManyVariantOnAttributeServiceResponse> ExecuteAsync(
        IAttachManyVariantOnAttributeCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var productsRepository = _unitOfWork.GetRepository<ProductModel>();
        var variantsRepository = _unitOfWork.GetRepository<VariantModel>();
        var variantsOnAttributesRepository = _unitOfWork.GetRepository<VariantOnAttributeModel>();
        var attributesRepository = _unitOfWork.GetRepository<AttributeModel>();

        var productFoundById = await productsRepository.FindOneAsync(
            filter: x => x.Id == command.ProductDto.ProductId,
            cancellationToken: cancellationToken
        );
        var variantsFoundById = await variantsRepository.FindManyAsync(
            filter: x =>
                command
                    .VariantOnAttributeDtos.Select(dto => dto.VariantId)
                    .ToArray()
                    .Contains(x.Id),
            cancellationToken: cancellationToken
        );
        var variantsOnAttributesFoundById = await variantsOnAttributesRepository.FindManyAsync(
            filter: x => command.VariantOnAttributeDtos.Select(dto => dto.Id).Contains(x.Id),
            cancellationToken: cancellationToken
        );
        var variantsOnAttributesFoundByVariantIdAttributeId =
            await variantsOnAttributesRepository.FindManyAsync(
                filter: x =>
                    command
                        .VariantOnAttributeDtos.Select(dto => dto.VariantId)
                        .ToArray()
                        .Contains(x.VariantId)
                    && command
                        .VariantOnAttributeDtos.Select(dto => dto.AttributeId)
                        .ToArray()
                        .Contains(x.AttributeId),
                cancellationToken: cancellationToken
            );
        var attributesFoundById = await attributesRepository.FindManyAsync(
            filter: x =>
                command.VariantOnAttributeDtos.Select(dto => dto.AttributeId).Contains(x.Id),
            cancellationToken: cancellationToken
        );

        return new AttachManyVariantOnAttributeServiceResponse(
            ProductFoundById: productFoundById,
            VariantsFoundById: variantsFoundById.ToArray(),
            VariantsOnAttributesFoundById: variantsOnAttributesFoundById.ToArray(),
            VariantsOnAttributesFoundByVariantIdAttributeId: variantsOnAttributesFoundByVariantIdAttributeId.ToArray(),
            AttributesFoundById: attributesFoundById.ToArray()
        );
    }
}

public record AttachManyVariantOnAttributeServiceResponse(
    IProductModel? ProductFoundById,
    IVariantModel[] VariantsFoundById,
    IVariantOnAttributeModel[] VariantsOnAttributesFoundById,
    IVariantOnAttributeModel[] VariantsOnAttributesFoundByVariantIdAttributeId,
    IAttributeModel[] AttributesFoundById
) : IAttachManyVariantOnAttributeServiceResponse;
