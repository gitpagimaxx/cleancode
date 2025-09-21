using CleanCode.Domain.Entities;
using CleanCode.Domain.Interfaces;
using CleanCode.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CleanCode.Infra.Data.Repositories;

public class ProductRepository(ApplicationDbContext context) : IProductRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Product> AddAsync(Product product, CancellationToken token)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync(token);
        return product;
    }

    public async Task<Product> DeleteAsync(int id, CancellationToken token)
    {
        var entity = await _context.Products.FindAsync(id, token);
        _context.Products.Remove(entity!);
        await _context.SaveChangesAsync(token);

        return entity!;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _context.Products.Include(c => c.Category).SingleOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Product> UpdateAsync(Product product, CancellationToken token)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync(token);
        return product;
    }
}
