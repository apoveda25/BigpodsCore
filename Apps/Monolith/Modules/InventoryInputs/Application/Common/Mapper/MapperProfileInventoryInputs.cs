using AutoMapper;
using Bigpods.Monolith.Modules.InventoryInputs.Application.CreateOne.Dtos;
using Bigpods.Monolith.Modules.InventoryInputs.Application.CreateOne.Inputs;
using Bigpods.Monolith.Modules.InventoryInputs.Domain.Common.Entities;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;

namespace Bigpods.Monolith.Modules.InventoryInputs.Application.Common.Mapper;

public class MapperProfileInventoryInputs : Profile
{
    public MapperProfileInventoryInputs()
    {
        CreateMap<CreateOneInventoryInputInput, CreateOneInventoryInputDto>();

        CreateMap<InventoryInputEntity, InventoryInputModel>()
            .ForMember(
                destinationMember: dest => dest.Stock,
                memberOptions: opt => opt.MapFrom(src => src.Stock.Value)
            )
            .ForMember(
                destinationMember: dest => dest.Comment,
                memberOptions: opt => opt.MapFrom(src => src.Comment.Value)
            );

        CreateMap<InventoryEntity, InventoryModel>()
            .ForMember(
                destinationMember: dest => dest.Stock,
                memberOptions: opt => opt.MapFrom(src => src.Stock.Value)
            );
    }
}
