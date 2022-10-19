using CatalogAPI.Data;
using CatalogAPI.Models;
using CatalogAPI.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Category>> GetCategoriesProductsAsync(int skipAmount, int takeAmount)
        {
            return await _context.Categories.Include(p => p.Products)
                .AsNoTracking()
                .Skip(skipAmount)
                .Take(takeAmount)
                .ToListAsync();
        }

        public async Task<int> CountCategoriesProductsAsync()
        {
            return await _context.Categories.Include(p => p.Products).CountAsync();
        }
    }
}
