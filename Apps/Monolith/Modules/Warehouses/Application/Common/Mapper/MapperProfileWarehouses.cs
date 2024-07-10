using AutoMapper;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;
using Bigpods.Monolith.Modules.Warehouses.Application.CreateOne.Dtos;
using Bigpods.Monolith.Modules.Warehouses.Application.CreateOne.Inputs;
using Bigpods.Monolith.Modules.Warehouses.Application.DeleteOne.Dtos;
using Bigpods.Monolith.Modules.Warehouses.Application.DeleteOne.Inputs;
using Bigpods.Monolith.Modules.Warehouses.Application.UpdateOne.Dtos;
using Bigpods.Monolith.Modules.Warehouses.Application.UpdateOne.Inputs;
using Bigpods.Monolith.Modules.Warehouses.Domain.Common.Aggregates;

namespace Bigpods.Monolith.Modules.Warehouses.Application.Common.Mapper;

public class MapperProfileWarehouses : Profile
{
    public MapperProfileWarehouses()
    {
        CreateMap<CreateOneWarehouseInput, CreateOneWarehouseDto>().ReverseMap();

        CreateMap<UpdateOneWarehouseInput, UpdateOneWarehouseDto>().ReverseMap();

        CreateMap<DeleteOneWarehouseInput, DeleteOneWarehouseDto>().ReverseMap();

        CreateMap<WarehouseAggregateRoot, WarehouseModel>()
            .ForMember(
                destinationMember: dest => dest.Name,
                memberOptions: opt => opt.MapFrom(src => src.Name.Value)
            )
            .ForMember(
                destinationMember: dest => dest.Description,
                memberOptions: opt => opt.MapFrom(src => src.Description.Value)
            );
    }
}
