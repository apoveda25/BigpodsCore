using AutoMapper;
using Bigpods.Monolith.Modules.Inventories.Application.CreateOne.Dtos;
using Bigpods.Monolith.Modules.Inventories.Application.CreateOne.Inputs;
using Bigpods.Monolith.Modules.Inventories.Application.DeleteOne.Dtos;
using Bigpods.Monolith.Modules.Inventories.Application.DeleteOne.Inputs;
using Bigpods.Monolith.Modules.Inventories.Domain.Common.Entities;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;

namespace Bigpods.Monolith.Modules.Inventories.Application.Common.Mapper;

public class MapperProfileInventories : Profile
{
    public MapperProfileInventories()
    {
        CreateMap<CreateOneInventoryInput, CreateOneInventoryDto>().ReverseMap();

        CreateMap<DeleteOneInventoryInput, DeleteOneInventoryDto>().ReverseMap();

        CreateMap<InventoryEntity, InventoryModel>()
            .ForMember(
                destinationMember: dest => dest.Stock,
                memberOptions: opt => opt.MapFrom(src => src.Stock.Value)
            );
    }
}
