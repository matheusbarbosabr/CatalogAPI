using CatalogAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CatalogAPI.Repositories.Abstractions
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<IEnumerable<Category>> GetCategoriesProductsAsync(int skipAmount, int takeAmount);
        Task<int> CountCategoriesProductsAsync();
    }
}
