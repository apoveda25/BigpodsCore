using AutoMapper;

using Bigpods.Monolith.Modules.AttributeTypes.Application.CreateOne.Dtos;

using Bigpods.Monolith.Modules.AttributeTypes.Application.CreateOne.Inputs;
using Bigpods.Monolith.Modules.AttributeTypes.Application.UpdateOne.Dtos;
using Bigpods.Monolith.Modules.AttributeTypes.Application.UpdateOne.Inputs;
using Bigpods.Monolith.Modules.AttributeTypes.Domain.Common.Aggregates;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;

namespace Bigpods.Monolith.Modules.AttributeTypes.Application.Common.Mapper;

public class MapperProfileAttributeTypes : Profile
{
    public MapperProfileAttributeTypes()
    {
        CreateMap<CreateOneAttributeTypeInput, CreateOneAttributeTypeDto>().ReverseMap();

        CreateMap<UpdateOneAttributeTypeInput, UpdateOneAttributeTypeDto>().ReverseMap();

        CreateMap<AttributeTypeAggregateRoot, AttributeTypeModel>()
            .ForMember(destinationMember: dest => dest.Name, memberOptions: opt => opt.MapFrom(src => src.Name.Value))
            .ForMember(destinationMember: dest => dest.Description, memberOptions: opt => opt.MapFrom(src => src.Description.Value))
            .ForMember(destinationMember: dest => dest.Attributes, memberOptions: opt => opt.Ignore());
    }
}
