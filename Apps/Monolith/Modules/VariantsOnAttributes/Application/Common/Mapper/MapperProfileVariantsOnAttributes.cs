using AutoMapper;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;
using Bigpods.Monolith.Modules.VariantsOnAttributes.Application.AttachMany.Dtos;
using Bigpods.Monolith.Modules.VariantsOnAttributes.Application.AttachMany.Inputs;
using Bigpods.Monolith.Modules.VariantsOnAttributes.Application.DettachMany.Dtos;
using Bigpods.Monolith.Modules.VariantsOnAttributes.Application.DettachMany.Inputs;
using Bigpods.Monolith.Modules.VariantsOnAttributes.Domain.Common.Entities;

namespace Bigpods.Monolith.Modules.VariantsOnAttributes.Application.Common.Mapper;

public class MapperProfileVariantsOnAttributes : Profile
{
    public MapperProfileVariantsOnAttributes()
    {
        CreateMap<AttachManyVariantOnAttributeInput, AttachManyVariantOnAttributeDto>()
            .ReverseMap();

        CreateMap<CreateOneVariantOnAttributeInput, CreateOneVariantOnAttributeDto>().ReverseMap();

        CreateMap<DettachManyVariantOnAttributeInput, DettachManyVariantOnAttributeDto>()
            .ReverseMap();

        CreateMap<DeleteOneVariantOnAttributeInput, DeleteOneVariantOnAttributeDto>().ReverseMap();

        CreateMap<VariantOnAttributeEntity, VariantOnAttributeModel>()
            .ForMember(
                destinationMember: dest => dest.Attribute,
                memberOptions: opt => opt.Ignore()
            )
            .ForMember(destinationMember: dest => dest.Variant, memberOptions: opt => opt.Ignore());
    }
}
