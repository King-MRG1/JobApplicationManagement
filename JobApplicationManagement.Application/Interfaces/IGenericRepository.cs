using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

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
    }
}
