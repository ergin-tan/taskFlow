using TaskFlow.Core.Models;
using TaskFlow.Core.Repositories;

namespace TaskFlow.Core.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> GetRepository<T>() where T : BaseEntity;
        Task SaveAsync();
    }
}