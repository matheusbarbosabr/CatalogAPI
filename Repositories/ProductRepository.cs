using CatalogAPI.Data;
using CatalogAPI.Models;
using CatalogAPI.Repositories.Abstractions;

namespace CatalogAPI.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }
    }
}
