using System;
using System.Collections.Generic;
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
