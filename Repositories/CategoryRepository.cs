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

        public async Task<IEnumerable<Category>> GetCategoriesProductsAsync()
        {
            return await _context.Categories.Include(p => p.Products).AsNoTracking().ToListAsync();
        }
    }
}
