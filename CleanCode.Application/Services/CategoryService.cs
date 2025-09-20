using AutoMapper;
using CleanCode.Application.Dtos;
using CleanCode.Application.Interfaces;
using CleanCode.Domain.Entities;
using CleanCode.Domain.Interfaces;

namespace CleanCode.Application.Services;

public class CategoryService(
    ICategoryRepository categoryRepository,
    IMapper mapper) : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<CategoryDto> AddAsync(CategoryDto category)
    {
        var categoryEntity = _mapper.Map<Category>(category);
        var resultEntity = await _categoryRepository.AddAsync(categoryEntity);
        return _mapper.Map<CategoryDto>(resultEntity);
    }

    public async Task DeleteAsync(int id)
    {
        await _categoryRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<CategoryDto>> GetAllAsync()
    {
        var categories = await _categoryRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<CategoryDto>>(categories);
    }

    public async Task<CategoryDto?> GetByIdAsync(int id)
    {
        var categories = await _categoryRepository.GetByIdAsync(id);
        return _mapper.Map<CategoryDto>(categories);
    }

    public async Task<CategoryDto> UpdateAsync(CategoryDto category)
    {
        var categoryEntity = _mapper.Map<Category>(category);
        var resultEntity = await _categoryRepository.UpdateAsync(categoryEntity);
        return _mapper.Map<CategoryDto>(resultEntity);
    }
}
