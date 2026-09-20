using System.Linq.Expressions;

namespace JobApplicationManagement.Application.Interfaces
{
public interface IGenericRepository<T> where T : class
    {
        Task AddAsync(T entity);
        void Update(T entity);
        IEnumerable<T> Get();
        Task<T> GetByIdAsync(int id);
        void Remove(T entity);
        Task SaveChangesAsync();
        Task<T?> FindFirstAsync(Expression<Func<T, bool>> predicate);
        IQueryable<T> Query();
    }
}
