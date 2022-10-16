using System.Linq.Expressions;

namespace CatalogAPI.Repositories.Abstractions
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAsync();
        Task<T> GetByIdAsync(Expression<Func<T, bool>> predicate);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
