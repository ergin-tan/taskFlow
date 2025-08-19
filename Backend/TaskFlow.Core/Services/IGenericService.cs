using System.Linq.Expressions;
using TaskFlow.Core.Models;

namespace TaskFlow.Core.Services
{
    public interface IGenericService<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
        Task<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes);
        Task<T?> GetByIdAsNoTrackingAsync(int id, params Expression<Func<T, object>>[] includes);
        Task AddAsync(T entity);
        Task Update(T entity);
        Task Remove(T entity);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
    }
}