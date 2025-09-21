using AutoMapper;
using CleanCode.Application.Categories.Commands;
using CleanCode.Application.Products.Commands;
using CleanCode.Application.Dtos;

namespace CleanCode.Application.Mappings;

public class DtoToCommandMappingProfile : Profile
{
    public DtoToCommandMappingProfile()
    {
        CreateMap<CategoryDto, CategoryCreateCommand>();
        CreateMap<CategoryDto, CategoryUpdateCommand>();

        CreateMap<ProductDto, ProductCreateCommand>();
        CreateMap<ProductDto, ProductUpdateCommand>();
    }
}
