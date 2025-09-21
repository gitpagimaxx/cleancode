using CleanCode.Domain.Entities;
using CleanCode.Domain.Interfaces;
using CleanCode.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CleanCode.Infra.Data.Repositories;

public class CategoryRepository(ApplicationDbContext context) : ICategoryRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Category> AddAsync(Category category, CancellationToken token)
    {
        _context.Categories.Add(category);
        await _context.SaveChangesAsync(token);
        return category;
    }

    public async Task<Category> DeleteAsync(int id, CancellationToken token)
    {
        var category = await _context.Categories.FindAsync(id, token);
        _context.Categories.Remove(category!);
        await _context.SaveChangesAsync(token);

        return category!;
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _context.Categories.ToListAsync();
    }

    public async Task<Category?> GetByIdAsync(int id)
    {
        return await _context.Categories.FindAsync(id);
    }

    public async Task<Category> UpdateAsync(Category category, CancellationToken token)
    {
        _context.Categories.Update(category);
        await _context.SaveChangesAsync(token);
        return category;
    }
}
