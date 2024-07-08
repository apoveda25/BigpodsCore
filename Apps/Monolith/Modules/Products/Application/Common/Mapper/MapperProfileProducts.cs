using AutoMapper;

using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;

using Bigpods.Monolith.Modules.Products.Application.CreateOne.Dtos;

using Bigpods.Monolith.Modules.Products.Application.CreateOne.Inputs;
using Bigpods.Monolith.Modules.Products.Application.UpdateOne.Dtos;
using Bigpods.Monolith.Modules.Products.Application.UpdateOne.Inputs;
using Bigpods.Monolith.Modules.Products.Domain.Common.Aggregates;
using Bigpods.Monolith.Modules.Products.Domain.Common.Entities;

namespace Bigpods.Monolith.Modules.Products.Application.Common.Mapper;

public class MapperProfileProducts : Profile
{
    public MapperProfileProducts()
    {
        CreateMap<CreateOneProductInput, CreateOneProductDto>().ReverseMap();
        CreateMap<CreateOneVariantInput, CreateOneVariantDto>().ReverseMap();
        CreateMap<CreateOneVariantOnAttributeInput, CreateOneVariantOnAttributeDto>().ReverseMap();

        CreateMap<UpdateOneProductInput, UpdateOneProductDto>().ReverseMap();

        CreateMap<ProductAggregateRoot, ProductModel>()
            .ForMember(destinationMember: dest => dest.Name, memberOptions: opt => opt.MapFrom(src => src.Name.Value))
            .ForMember(destinationMember: dest => dest.Description, memberOptions: opt => opt.MapFrom(src => src.Description.Value))
            .ForMember(destinationMember: dest => dest.Brand, memberOptions: opt => opt.MapFrom(src => src.Brand.Value))
            .ForMember(destinationMember: dest => dest.Model, memberOptions: opt => opt.MapFrom(src => src.Model.Value))
            .ForMember(destinationMember: dest => dest.Variants, memberOptions: opt => opt.Ignore());

        CreateMap<VariantEntity, VariantModel>()
            .ForMember(destinationMember: dest => dest.Name, memberOptions: opt => opt.MapFrom(src => src.Name.Value))
            .ForMember(destinationMember: dest => dest.Price, memberOptions: opt => opt.MapFrom(src => src.Price.Value))
            .ForMember(destinationMember: dest => dest.Cost, memberOptions: opt => opt.MapFrom(src => src.Cost.Value))
            .ForMember(destinationMember: dest => dest.Product, memberOptions: opt => opt.Ignore())
            .ForMember(destinationMember: dest => dest.VariantsOnAttributes, memberOptions: opt => opt.Ignore());

        CreateMap<VariantOnAttributeEntity, VariantOnAttributeModel>()
            .ForMember(destinationMember: dest => dest.Attribute, memberOptions: opt => opt.Ignore())
            .ForMember(destinationMember: dest => dest.Variant, memberOptions: opt => opt.Ignore());
    }
}
