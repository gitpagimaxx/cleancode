using AutoMapper;
using CleanCode.Application.Dtos;
using CleanCode.Domain.Entities;

namespace CleanCode.Application.Mappings;

public class DomainToDTOMappingProfiler : Profile
{
    public DomainToDTOMappingProfiler()
    {
        CreateMap<Category, CategoryDto>().ReverseMap();
        CreateMap<Product, ProductDto>().ReverseMap();
    }
}
