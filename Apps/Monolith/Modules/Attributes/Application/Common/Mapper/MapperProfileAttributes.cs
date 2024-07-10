using AutoMapper;
using Bigpods.Monolith.Modules.Attributes.Application.CreateOne.Dtos;
using Bigpods.Monolith.Modules.Attributes.Application.CreateOne.Inputs;
using Bigpods.Monolith.Modules.Attributes.Application.DeleteOne.Dtos;
using Bigpods.Monolith.Modules.Attributes.Application.DeleteOne.Inputs;
using Bigpods.Monolith.Modules.Attributes.Domain.Common.Entities;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;

namespace Bigpods.Monolith.Modules.Attributes.Application.Common.Mapper;

public class MapperProfileAttributes : Profile
{
    public MapperProfileAttributes()
    {
        CreateMap<CreateOneAttributeInput, CreateOneAttributeDto>().ReverseMap();

        CreateMap<DeleteOneAttributeInput, DeleteOneAttributeDto>().ReverseMap();

        CreateMap<AttributeEntity, AttributeModel>()
            .ForMember(
                destinationMember: dest => dest.Value,
                memberOptions: opt => opt.MapFrom(src => src.Value.Value)
            )
            .ForMember(
                destinationMember: dest => dest.MeasuringUnit,
                memberOptions: opt => opt.MapFrom(src => src.MeasuringUnit.Value)
            );
    }
}
