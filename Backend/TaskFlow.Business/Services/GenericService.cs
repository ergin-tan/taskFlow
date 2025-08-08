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

        public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _unitOfWork.GetRepository<T>().FindAsync(predicate);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _unitOfWork.GetRepository<T>().GetAllAsync();
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await _unitOfWork.GetRepository<T>().GetByIdAsync(id);
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
