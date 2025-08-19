using System.Linq.Expressions;
using TaskFlow.Core.Models;
using TaskFlow.Core.Services;
using TaskFlow.Core.UnitOfWorks;

namespace TaskFlow.Business.Services
{
    public class GenericService<T> : IGenericService<T> where T : BaseEntity
    {
        protected readonly IUnitOfWork _unitOfWork;

        public GenericService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public virtual async Task AddAsync(T entity)
        {
            await _unitOfWork.GetRepository<T>().AddAsync(entity);
            await _unitOfWork.SaveAsync();
        }

        public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            return await _unitOfWork.GetRepository<T>().FindAsync(predicate, includes);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await GetAllAsync(Array.Empty<Expression<Func<T, object>>>());
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            return await _unitOfWork.GetRepository<T>().GetAllAsync(includes);
        }

        public virtual async Task<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
        {
            return await _unitOfWork.GetRepository<T>().GetByIdAsync(id, includes);
        }

        public virtual async Task<T?> GetByIdAsNoTrackingAsync(int id, params Expression<Func<T, object>>[] includes)
        {
            return await _unitOfWork.GetRepository<T>().GetByIdAsNoTrackingAsync(id, includes);
        }

        public virtual async Task Remove(T entity)
        {
            _unitOfWork.GetRepository<T>().Remove(entity);
            await _unitOfWork.SaveAsync();
        }

        public virtual async Task Update(T entity)
        {
            _unitOfWork.GetRepository<T>().Update(entity);
            await _unitOfWork.SaveAsync();
        }
    }
}