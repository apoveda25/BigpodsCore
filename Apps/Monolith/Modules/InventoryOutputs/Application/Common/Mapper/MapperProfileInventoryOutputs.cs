using AutoMapper;

using Bigpods.Monolith.Modules.InventoryOutputs.Application.CreateOne.Dtos;

using Bigpods.Monolith.Modules.InventoryOutputs.Application.CreateOne.Inputs;
using Bigpods.Monolith.Modules.InventoryOutputs.Domain.Common.Entities;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;

namespace Bigpods.Monolith.Modules.InventoryOutputs.Application.Common.Mapper;

public class MapperProfileInventoryOutputs : Profile
{
    public MapperProfileInventoryOutputs()
    {
        CreateMap<CreateOneInventoryOutputInput, CreateOneInventoryOutputDto>();

        CreateMap<InventoryOutputEntity, InventoryOutputModel>()
            .ForMember(destinationMember: dest => dest.Stock, memberOptions: opt => opt.MapFrom(src => src.Stock.Value))
            .ForMember(destinationMember: dest => dest.Comment, memberOptions: opt => opt.MapFrom(src => src.Comment.Value));

        CreateMap<InventoryEntity, InventoryModel>()
            .ForMember(destinationMember: dest => dest.Stock, memberOptions: opt => opt.MapFrom(src => src.Stock.Value));
    }
}
